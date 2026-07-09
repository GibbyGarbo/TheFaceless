using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;


public class BeginInfection() : TheFacelessCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<BeginInfectionPower>(6)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        BeginInfection card = this;
        await PowerCmd.Apply<BeginInfectionPower>(choiceContext, card.Owner.Creature, DynamicVars.Power<BeginInfectionPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    AddKeyword(CardKeyword.Innate);
    }
}