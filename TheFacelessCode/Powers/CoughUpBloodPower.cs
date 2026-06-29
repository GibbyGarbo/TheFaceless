using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.Powers;

public class CoughUpBloodPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		CoughUpBloodPower coughUpBloodPower = this;
		if (!((amount > 0m) & (applier != ((PowerModel)coughUpBloodPower).Owner)) && power is Corruption)
		{
			((PowerModel)coughUpBloodPower).Flash();
			VfxCmd.PlayOnCreatureCenters((IEnumerable<Creature>)((PowerModel)coughUpBloodPower).CombatState.HittableEnemies, "vfx/vfx_attack_slash");
			SfxCmd.Play("slash_attack.mp3", 1f);
			await CreatureCmd.Damage(choiceContext, (IEnumerable<Creature>)((PowerModel)coughUpBloodPower).CombatState.HittableEnemies, (decimal)((PowerModel)coughUpBloodPower).Amount, (ValueProp)4, ((PowerModel)coughUpBloodPower).Owner, (CardModel)null);
		}
	}
}
