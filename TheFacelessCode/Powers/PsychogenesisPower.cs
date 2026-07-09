using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.Powers;


public class PsychogenesisPower : TheFacelessPower
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
        PsychogenesisPower psychogenesisPower = this;
        if (amount == 0M || power is not Paranoia || !power.Owner.IsEnemy || applier != psychogenesisPower.Owner || power is ITemporaryPower)
            //(amount <= 0M && power is Paranoia && power.Owner.IsEnemy && applier == psychogenesisPower.Owner)
            return;
        psychogenesisPower.Flash();
        await CreatureCmd.Damage(choiceContext, power.Owner, psychogenesisPower.Amount, ValueProp.Unpowered, psychogenesisPower.Owner);
    }
}