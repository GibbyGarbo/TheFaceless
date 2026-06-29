using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Powers;


public class ShatterIdentityPower() : TheFacelessPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        ShatterIdentityPower shatterIdentityPower = this;
        if (amount == 0M || power.GetTypeForAmount(amount) != PowerType.Debuff || !power.Owner.IsEnemy || applier != shatterIdentityPower.Owner || power is ITemporaryPower)
            return;
        shatterIdentityPower.Flash();
        await CreatureCmd.GainBlock(shatterIdentityPower.Owner, shatterIdentityPower.Amount, ValueProp.Move, null, true);;
    }
}