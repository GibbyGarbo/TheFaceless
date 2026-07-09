using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheFaceless.TheFacelessCode.Cards;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

 

public class Consequences() : TheFacelessCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<ConsequencesPower>(2)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Consequences card = this;
        await PowerCmd.Apply<ConsequencesPower>(choiceContext, card.Owner.Creature, DynamicVars.Power<ConsequencesPower>().BaseValue, Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
    DynamicVars.Power<ConsequencesPower>().UpgradeValueBy(1);
    }
}