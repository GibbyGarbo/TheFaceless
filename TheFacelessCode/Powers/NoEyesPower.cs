using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace TheFaceless.TheFacelessCode.Powers;

public class NoEyesPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		NoEyesPower noEyesPower = this;
		if (applier == ((PowerModel)noEyesPower).Owner && power is Paranoia)
		{
			((PowerModel)noEyesPower).Flash();
			await CardPileCmd.Draw(choiceContext, (decimal)((PowerModel)noEyesPower).Amount, ((PowerModel)noEyesPower).Owner.Player, false);
		}
	}
}
