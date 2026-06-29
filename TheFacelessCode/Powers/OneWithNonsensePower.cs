using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace TheFaceless.TheFacelessCode.Powers;

public class OneWithNonsensePower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	protected override IEnumerable<DynamicVar> CanonicalVars =>
		[new PowerVar<Corruption>((decimal)((PowerModel)this).Amount)];

	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
	{
		OneWithNonsensePower oneWithNonsensePower = this;
		if (participants.Contains(((PowerModel)oneWithNonsensePower).Owner))
		{
			((PowerModel)oneWithNonsensePower).Flash();
			await PowerCmd.Apply<Corruption>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(), ((PowerModel)oneWithNonsensePower).Owner, (decimal)((PowerModel)oneWithNonsensePower).Amount, ((PowerModel)oneWithNonsensePower).Owner, (CardModel)null, false);
		}
	}
}
