using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars; 
using TheFaceless.TheFacelessCode.Powers;
namespace TheFaceless.TheFacelessCode.Cards;

 
public class PsychePool() : TheFacelessCard(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<PsychePoolPower>(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        PsychePool card = this;
        await PowerCmd.Apply<PsychePoolPower>(choiceContext, card.Owner.Creature, DynamicVars.Power<PsychePoolPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    AddKeyword(CardKeyword.Retain);
    }
}