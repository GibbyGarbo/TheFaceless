using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using TheFaceless.TheFacelessCode.Powers;
using TheFaceless.TheFacelessCode.Relics;

namespace TheFaceless.TheFacelessCode.Powers;

public class FacelessMaskPower() : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Relic<FacelessMask>();
}
