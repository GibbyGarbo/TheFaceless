using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public abstract class TheFacelessPower : CustomPowerModel
{
	public override string CustomPackedIconPath => (this.Id.Entry.RemovePrefix().ToLowerInvariant() + ".png").PowerImagePath();

	public override string CustomBigIconPath => (this.Id.Entry.RemovePrefix().ToLowerInvariant() + ".png").BigPowerImagePath();

	public abstract override PowerType Type { get; }

	public abstract override PowerStackType StackType { get; }
}
