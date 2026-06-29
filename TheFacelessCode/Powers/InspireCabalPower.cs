using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace TheFaceless.TheFacelessCode.Powers;

public class InspireCabalPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
	{
		InspireCabalPower inspireCabalPower = this;
		if (participants.Contains(((PowerModel)inspireCabalPower).Owner))
		{
			((PowerModel)inspireCabalPower).Flash();
			await PowerCmd.Apply<Corruption>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(), ((PowerModel)inspireCabalPower).Owner, (decimal)((PowerModel)inspireCabalPower).Amount, ((PowerModel)inspireCabalPower).Owner, (CardModel)null, false);
			await PowerCmd.Apply<Corruption>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(), (IEnumerable<Creature>)((PowerModel)inspireCabalPower).CombatState.HittableEnemies, (decimal)((PowerModel)inspireCabalPower).Amount, ((PowerModel)inspireCabalPower).Owner, (CardModel)null, false);
			await PowerCmd.Apply<RitualPower>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(), (IEnumerable<Creature>)((PowerModel)inspireCabalPower).CombatState.HittableEnemies, 1m, ((PowerModel)inspireCabalPower).Owner, (CardModel)null, false);
		}
	}
}
