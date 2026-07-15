using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class IterationCyclePower : TheFacelessPower
{
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
	
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;
}
