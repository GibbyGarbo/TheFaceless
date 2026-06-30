using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;


public class Psychogenesis() : TheFacelessCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<PsychogenesisPower>(9)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<Paranoia>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Psychogenesis card = this;
        await PowerCmd.Apply<PsychogenesisPower>(choiceContext, card.Owner.Creature, DynamicVars.Power<PsychogenesisPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    DynamicVars.Power<PsychogenesisPower>().UpgradeValueBy(3);
    }
}