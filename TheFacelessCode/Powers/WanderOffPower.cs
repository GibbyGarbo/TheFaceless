using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class WanderOffPower : TheFacelessPower
{
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
	
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
	{
		WanderOffPower wanderOffPower = this;
		if (participants.Contains(((PowerModel)wanderOffPower).Owner))
		{
			((PowerModel)wanderOffPower).Flash();
			Creature target = ((PowerModel)wanderOffPower).Owner.Player.RunState.Rng.CombatTargets.NextItem<Creature>((IEnumerable<Creature>)((PowerModel)wanderOffPower).CombatState.HittableEnemies);
			if (target != null)
			{
				await PowerCmd.Apply<Corruption>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(), target, (decimal)((PowerModel)wanderOffPower).Amount, ((PowerModel)wanderOffPower).Owner, (CardModel)null, false);
			}
		}
	}
}
