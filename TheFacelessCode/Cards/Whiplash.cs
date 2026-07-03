using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

 
public class Whiplash() : TheFacelessCard(4,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new DamageVar(20, ValueProp.Move),
    new EnergyVar(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Whiplash card = this;
        await CreatureCmd.TriggerAnim(card.Owner.Creature, "Cast", card.Owner.Character.AttackAnimDelay);
        DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).TargetingAllOpponents(card.CombatState)
            .Execute(choiceContext);
        if (card.CombatState != null)
        {
            foreach (Creature hittableEnemy in card.CombatState.HittableEnemies)
            {
                    NCombatRoom instance = NCombatRoom.Instance;
                    instance.CombatVfxContainer.AddChildSafely(NSpikeSplashVfx.Create(hittableEnemy));
                
            }
        }
    }


    protected override void OnUpgrade()
    {
    DynamicVars.Damage.UpgradeValueBy(7);
    }
    
    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this || IsClone)
            return Task.CompletedTask;
        ReduceCostBy(CombatManager.Instance.History.CardPlaysFinished.Count(e => e.CardPlay.Card.Enchantment is not DejaVu && e.CardPlay.Card.Owner == Owner && e.HappenedThisTurn(CombatState)));
        return Task.CompletedTask;
    }

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != this.Owner || cardPlay.Card.Enchantment is not DejaVu)
            return Task.CompletedTask;
        this.ReduceCostBy(1);
        return Task.CompletedTask;
    }

    public void ReduceCostBy(int amount) => this.EnergyCost.AddThisTurn(-amount);
}

