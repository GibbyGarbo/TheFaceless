using System.Linq;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.Powers;

public class ArrhythmiaPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override Decimal ModifyDamageMultiplicative(
		Creature? target, 
		Decimal amount, 
		ValueProp props, 
		Creature? dealer, 
		CardModel? cardSource,
		CardPlay? cardPlay)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//return !props.IsPoweredAttack() || cardSource == null || dealer != Owner && !Owner.Pets.Contains<Creature>(dealer) || target == null || !target.HasPower<Paranoia>() ? 1M : 1M + Amount / 100M;
		if (props.IsPoweredAttack() && cardSource != null && (dealer == Owner || Owner.Pets.Contains(dealer)) && target != null && target.HasPower<Paranoia>())
		{
			return 1m + Amount / 100m;
		}
		return 1m;
	}
}
