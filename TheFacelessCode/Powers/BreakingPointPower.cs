using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Cards;

namespace TheFaceless.TheFacelessCode.Powers;

public class BreakingPointPower : CustomTemporaryPowerModelWrapper<BreakingPoint, StrengthPower>
{
	protected bool IsPositive => false;
}
