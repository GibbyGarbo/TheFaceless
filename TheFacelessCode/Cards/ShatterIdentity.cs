using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;


public class ShatterIdentity() : TheFacelessCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<ShatterIdentityPower>(2)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ShatterIdentity card = this;
        await PowerCmd.Apply<OneWithNonsensePower>(choiceContext, card.Owner.Creature, DynamicVars.Power<ShatterIdentityPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    AddKeyword(CardKeyword.Innate);
    }
}