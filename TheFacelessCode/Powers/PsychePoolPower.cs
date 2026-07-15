using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class PsychePoolPower : TheFacelessPower
{
    public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

    public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
    
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;


 
    
  public override async Task AfterSideTurnStart(
      CombatSide side,
      IReadOnlyList<Creature> participants,
      ICombatState combatState)
    {
        PsychePoolPower psychePoolPower = this;
        if (Owner.Player != null)
        {
            if (!participants.Contains(psychePoolPower.Owner))
                return;
            int blockPerTurn = PileType.Exhaust.GetPile(Owner.Player).Cards.Count;
            await CreatureCmd.GainBlock(psychePoolPower.Owner, blockPerTurn, ValueProp.Unpowered,  null);
        }
    }
}