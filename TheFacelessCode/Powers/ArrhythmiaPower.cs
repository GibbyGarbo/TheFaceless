using System.Linq;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.Powers;

public class ArrhythmiaPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (ValuePropExtensions.IsPoweredAttack(props) && cardSource != null && (dealer == ((PowerModel)this).Owner || ((PowerModel)this).Owner.Pets.Contains(dealer)) && target != null && target.HasPower<Paranoia>())
		{
			return 1m + (decimal)((PowerModel)this).Amount / 100m;
		}
		return 1m;
	}
}
