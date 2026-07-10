using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Relics;


public class BloodyRock : TheFacelessRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3M, ValueProp.Unpowered)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..HoverTipFactory.FromEnchantment<DejaVu>()
    ];
    

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!((cardPlay.Card.Owner == Owner) & !(cardPlay.Card.Enchantment is DejaVu)))
        {
            Flash();
            if (Owner.Creature.CombatState != null)
            {
                Creature? target = Owner.RunState.Rng.CombatTargets.NextItem(Owner.Creature.CombatState.HittableEnemies);
                if (target == null)
                    return;
                VfxCmd.PlayOnCreatureCenter(target, "vfx/vfx_attack_blunt");
                await CreatureCmd.Damage(choiceContext, target, DynamicVars.Damage, Owner.Creature);
            }
        }
    }
}