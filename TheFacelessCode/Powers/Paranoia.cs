using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace TheFaceless.TheFacelessCode.Powers;

public class Paranoia : TheFacelessPower
{
	public override PowerType Type => (PowerType)2;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
	{
		Paranoia paranoia = this;
		_ = ((PowerModel)paranoia)._amountOnTurnStart;
		if (!((PowerModel)paranoia).Owner.Monster.IntendsToAttack)
		{
			await PowerCmd.Apply<StrengthPower>(choiceContext, ((PowerModel)paranoia).Owner, (decimal)(-((PowerModel)paranoia).Amount), ((PowerModel)paranoia).Owner, (CardModel)null, false);
			await PowerCmd.Apply<Paranoia>(choiceContext, ((PowerModel)paranoia).Owner, (decimal)(-((PowerModel)paranoia).Amount), ((PowerModel)paranoia).Owner, (CardModel)null, false);
		}
		else
		{
			await PowerCmd.Apply<Paranoia>(choiceContext, ((PowerModel)paranoia).Owner, (decimal)(-((PowerModel)paranoia).Amount), ((PowerModel)paranoia).Owner, (CardModel)null, false);
		}
	}
}
