using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Powers;


public class OperatorFormPower() : TheFacelessPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;



    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        OperatorFormPower operatorFormPower = this;
        
        if (!participants.Contains(operatorFormPower.Owner))
            return;
        foreach (CardModel card in PileType.Exhaust.GetPile(operatorFormPower.Owner.Player).Cards
                     .Where(c => c.Enchantment is DejaVu).ToList()
                     .UnstableShuffle(operatorFormPower.Owner.Player.RunState.Rng.CombatCardSelection)
                     .Take(operatorFormPower.Amount))
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
        }

    }
}