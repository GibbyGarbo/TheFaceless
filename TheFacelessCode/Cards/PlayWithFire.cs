using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

 
public class PlayWithFire() : TheFacelessCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new DamageVar(13, ValueProp.Move)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            List<IHoverTip> list =
            [
                HoverTipFactory.FromKeyword((CardKeyword)1)
            ];
            list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>());
            return new List<IHoverTip>(list);
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        PlayWithFire source = this;
        EnchantmentModel playWithFire = ModelDb.Enchantment<DejaVu>();
        
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(source.DynamicVars.Damage.BaseValue).FromCard(source).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, 2);
        ((PileType)6).GetPile(source.Owner).Cards.Where(playWithFire.CanEnchant).ToList();
        foreach (CardModel item in await CardSelectCmd.FromHand(choiceContext, source.Owner, prefs, null, source))
        {
            CardCmd.Enchant<DejaVu>(item, 1m);
        }
    }

    protected override void OnUpgrade()
    {
    DynamicVars.Damage.UpgradeValueBy(4);
    }
}