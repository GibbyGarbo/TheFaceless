using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Patches.Features;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using TheFaceless.TheFacelessCode.Character;
using TheFaceless.TheFacelessCode.Extensions;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Potions;

[Pool(typeof(TheFacelessPotionPool))]
public class PanicPotion : CustomPotionModel
{
    public override string CustomPackedImagePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();
    public override string CustomPackedOutlinePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".PotionImagePath();
    public override PotionRarity Rarity => PotionRarity.Uncommon;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyEnemy;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<Paranoia>(3)
    ];   
    

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<Paranoia>()
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("404040"));
        await PowerCmd.Apply<Paranoia>(choiceContext, target, DynamicVars.Power<Paranoia>().BaseValue, Owner.Creature, null);
    }
}