using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Extensions;
using StringExtensions = BaseLib.Extensions.StringExtensions;

namespace TheFaceless.TheFacelessCode.Powers;

public sealed class Corruption : TheFacelessPower
{
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();

	public override PowerType Type => (PowerType)2;

	public override PowerStackType StackType => (PowerStackType)1;
}
