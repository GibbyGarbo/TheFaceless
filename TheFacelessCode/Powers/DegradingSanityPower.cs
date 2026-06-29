using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace TheFaceless.TheFacelessCode.Powers;

public class DegradingSanityPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
	{
		DegradingSanityPower degradingSanityPower = this;
		if (participants.Contains(((PowerModel)degradingSanityPower).Owner))
		{
			((PowerModel)degradingSanityPower).Flash();
			await PowerCmd.Apply<Paranoia>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(), (IEnumerable<Creature>)((PowerModel)degradingSanityPower).CombatState.HittableEnemies, (decimal)((PowerModel)degradingSanityPower).Amount, ((PowerModel)degradingSanityPower).Owner, (CardModel)null, false);
		}
	}
}
