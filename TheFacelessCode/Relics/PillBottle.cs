using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Powers;
using TheFaceless.TheFacelessCode.Relics;

namespace TheFaceless.TheFacelessCode.Relics;


public class PillBottle() : TheFacelessRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Common;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<Corruption>(3)
    ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<Corruption>()
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
                await PowerCmd.Apply<Corruption>(choiceContext, Owner.Creature,
                    DynamicVars["Corruption"].BaseValue, Owner.Creature, null);
            }
        }
    }
}