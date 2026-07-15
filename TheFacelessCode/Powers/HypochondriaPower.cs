using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class HypochondriaPower : TheFacelessPower
{
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
	
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		HypochondriaPower hypochondriaPower = this;
		if (((amount > 0m) & (applier == ((PowerModel)hypochondriaPower).Owner)) && power is Paranoia && power.Owner.IsEnemy && power.Owner.Monster != null && power.Owner.Monster.IntendsToAttack)
		{
			((PowerModel)hypochondriaPower).Flash();
			await PowerCmd.Apply<WeakPower>(choiceContext, power.Owner, (decimal)((PowerModel)hypochondriaPower).Amount, ((PowerModel)hypochondriaPower).Owner, (CardModel)null, false);
		}
	}
}
