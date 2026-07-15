using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class CoughUpBloodPower : TheFacelessPower
{
	
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
	
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		CoughUpBloodPower coughUpBloodPower = this;
		if (!((amount > 0m) & (applier != coughUpBloodPower.Owner)) && power is Corruption)
		{
			coughUpBloodPower.Flash();
			VfxCmd.PlayOnCreatureCenters(coughUpBloodPower.CombatState.HittableEnemies, "vfx/vfx_attack_slash");
			SfxCmd.Play("slash_attack.mp3");
			await CreatureCmd.Damage(choiceContext, coughUpBloodPower.CombatState.HittableEnemies, coughUpBloodPower.Amount, (ValueProp)4, coughUpBloodPower.Owner);
		}
	}
}
