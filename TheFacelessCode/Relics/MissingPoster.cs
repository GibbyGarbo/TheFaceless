using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using TheFaceless.TheFacelessCode.Powers;
using TheFaceless.TheFacelessCode.Relics;

namespace TheFaceless.TheFacelessCode.Relics;


public class MissingPoster() : TheFacelessRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<Paranoia>()
    ];
}