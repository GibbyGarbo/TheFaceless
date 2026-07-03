using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Powers;


public class DrownInStaticPower() : TheFacelessPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;


    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        DrownInStaticPower drownInStaticPower = this;
        if (participants.Contains(drownInStaticPower.Owner))
        {
            drownInStaticPower.Flash();
            if (drownInStaticPower.Owner.CombatState != null)
                await PowerCmd.Apply<Corruption>(new ThrowingPlayerChoiceContext(),
                    drownInStaticPower.Owner.CombatState.Creatures.Where(c => !c.IsPet),
                    drownInStaticPower.Amount,
                    drownInStaticPower.Owner,
                    null);
        }
    }
}