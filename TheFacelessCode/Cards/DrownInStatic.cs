using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheFaceless.TheFacelessCode.Cards;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;


public class DrownInStatic() : TheFacelessCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<DrownInStaticPower>(4)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Corruption>()];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        DrownInStatic card = this;
        await PowerCmd.Apply<DrownInStaticPower>(choiceContext, card.Owner.Creature, DynamicVars.Power<DrownInStaticPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    DynamicVars.Power<DrownInStaticPower>().UpgradeValueBy(2);
    }
}