using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheFaceless.TheFacelessCode.Cards;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;


public class OperatorForm() : TheFacelessCard(2,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<OperatorFormPower>(1)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            List<IHoverTip> list = new List<IHoverTip>();
            list.Add(HoverTipFactory.FromKeyword((CardKeyword.Exhaust)));
            list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>());
            return new List<IHoverTip>(list);
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        OperatorForm card = this;
        await PowerCmd.Apply<OperatorFormPower>(choiceContext, card.Owner.Creature, DynamicVars.Power<OperatorFormPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    EnergyCost.UpgradeBy(-1);
    }
}