using BaseLib.Extensions;
using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

 
public class Paralyze() : TheFacelessCard(1,
    CardType.Attack, CardRarity.Ancient,
    CustomTargetType.Everyone)

{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar((ValueProp)12).WithMultiplier((Func<CardModel, Creature, decimal>)((_, target) => target != null ? target.GetPowerAmount<Corruption>() : 0)),
        new PowerVar<Corruption>(8m),
        new DynamicVar("CorruptionInt", 8m),
        new CalculationExtraVar(1m),
        new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((card, target) => Math.Floor((target != null ? target.GetPowerAmount<Corruption>() : 0) + card.DynamicVars["CorruptionInt"].BaseValue)))
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Corruption>()];

    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Paralyze card = this;
        
        await PowerCmd.Apply<Corruption>(choiceContext, card.Owner.Creature, DynamicVars.Power<Corruption>().BaseValue, Owner.Creature, this);
        int blockGain = card.Owner.Creature.GetPowerAmount<Corruption>();
        await CreatureCmd.GainBlock(card.Owner.Creature, blockGain, (ValueProp)8, play);
        if (card.CombatState != null)
            foreach (Creature hittableEnemy in card.CombatState.HittableEnemies)
            {
                await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy,
                    DynamicVars.Power<Corruption>().BaseValue, Owner.Creature, this);
                await CreatureCmd.Damage(choiceContext, hittableEnemy,
                    DynamicVars.CalculatedDamage.Calculate(hittableEnemy), (ValueProp)12, this.Owner.Creature, this);
            }
    }

    protected override void OnUpgrade()
    {
    DynamicVars.Power<Corruption>().UpgradeValueBy(4);
    DynamicVars["CorruptionInt"].UpgradeValueBy(4);
    }


    
}