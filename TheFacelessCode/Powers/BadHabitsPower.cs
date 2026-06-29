using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace TheFaceless.TheFacelessCode.Powers;

public class BadHabitsPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		BadHabitsPower badHabitsPower = this;
		if (amount > 0m && applier == ((PowerModel)badHabitsPower).Owner && power is Corruption && power.Owner.IsEnemy)
		{
			((PowerModel)badHabitsPower).Flash();
			await PowerCmd.Apply<Corruption>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(), ((PowerModel)badHabitsPower).Owner, (decimal)((PowerModel)badHabitsPower).Amount, ((PowerModel)badHabitsPower).Owner, (CardModel)null, false);
		}
	}
}
