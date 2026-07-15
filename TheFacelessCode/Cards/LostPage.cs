using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.CardTags;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;


public class LostPage() : TheFacelessCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    
    protected override HashSet<CardTag> CanonicalTags => [CustomCardTags.Page];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new DamageVar(3, ValueProp.Move),
    new RepeatVar(8),
    new CardsVar(8)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            List<IHoverTip> list = new List<IHoverTip>();
            list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>());
            list.Add(HoverTipFactory.FromKeyword((CardKeyword)1));
            return new List<IHoverTip>(list);
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        LostPage card = this;
        EnchantmentModel lostPage = ModelDb.Enchantment<DejaVu>();
        if (CardPile.GetCards(Owner, PileType.Exhaust).Count(c =>  c.Tags.Contains(CustomCardTags.Page)) >= DynamicVars.Cards.IntValue)
        {
            ArgumentNullException.ThrowIfNull(play.Target);
            await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(card.DynamicVars.Repeat.IntValue).FromCard(card, play).Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
            CardModel clone = card.CreateClone();
                 if (lostPage.CanEnchant(clone))
                 {
                     CardCmd.Enchant<DejaVu>(clone, 1);
                 }

                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Discard, card.Owner), 1f);
                
        }
        else
        {
                ArgumentNullException.ThrowIfNull(play.Target);
            await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card, play).Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
            CardModel clone = card.CreateClone();
            if (lostPage.CanEnchant(clone))
            {
                CardCmd.Enchant<DejaVu>(clone, 1);
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Discard, card.Owner), 1f);
           
        }
    }

    protected override void OnUpgrade()
    {
    EnergyCost.UpgradeBy(-1);
    }
}