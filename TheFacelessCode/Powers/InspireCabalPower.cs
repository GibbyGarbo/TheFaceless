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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class InspireCabalPower : TheFacelessPower
{
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
	
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
	{
		InspireCabalPower inspireCabalPower = this;
		if (participants.Contains(inspireCabalPower.Owner))
		{
			inspireCabalPower.Flash();
			foreach (Creature creature in (IEnumerable<Creature>)CombatState.PlayerCreatures
				         .Where(c => c.IsAlive).ToList())
			{
				await PowerCmd.Apply<Corruption>(new ThrowingPlayerChoiceContext(),
					creature, inspireCabalPower.Amount,
					inspireCabalPower.Owner, null);
			}

			await PowerCmd.Apply<Corruption>(new ThrowingPlayerChoiceContext(), inspireCabalPower.CombatState.HittableEnemies, inspireCabalPower.Amount, inspireCabalPower.Owner, null);
			await PowerCmd.Apply<RitualPower>(new ThrowingPlayerChoiceContext(), inspireCabalPower.CombatState.HittableEnemies, 1m, inspireCabalPower.Owner, null);
		}
	}
}
