using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using TheFaceless.TheFacelessCode.Character;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Potions;

[Pool(typeof(TheFacelessPotionPool))]
public class VialPotion : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Common;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AllEnemies;


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<Corruption>(6)
    ];


    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<Corruption>()
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        AssertValidForTargetedPotion(target);

        if (Owner.Creature.CombatState != null)
        {
            foreach (Creature creature in Owner.Creature.CombatState.Creatures.Where(c => !c.IsPet))
                NCombatRoom.Instance?.PlaySplashVfx(creature, new Color("404040"));
            NCombatRoom.Instance?.PlaySplashVfx(Owner.Creature, new Color("404040"));
            NCombatRoom.Instance?.PlaySplashVfx(target, new Color("404040"));
                await PowerCmd.Apply<Corruption>(choiceContext,
                    Owner.Creature.CombatState.Creatures.Where(c => !c.IsPet),
                    DynamicVars.Power<Corruption>().BaseValue, Owner.Creature, null);
        }
    }
}
