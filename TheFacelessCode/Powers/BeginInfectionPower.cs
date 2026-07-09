using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace TheFaceless.TheFacelessCode.Powers;


public class BeginInfectionPower : TheFacelessPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override object InitInternalData() => new Data();

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player || cardPlay.Card.Type != CardType.Power)
            return Task.CompletedTask;
        GetInternalData<Data>().amountsForPlayedCards.Add(cardPlay.Card, Amount);
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BeginInfectionPower beginInfectionPower = this;
        int corruption;
        if (cardPlay.Card.Owner != beginInfectionPower.Owner.Player || !beginInfectionPower.GetInternalData<Data>()
                .amountsForPlayedCards.Remove(cardPlay.Card, out corruption) || corruption <= 0)
            return;
        beginInfectionPower.Flash();
            await PowerCmd.Apply<Corruption>(new ThrowingPlayerChoiceContext(), beginInfectionPower.Owner,
                corruption, beginInfectionPower.Owner, null);

    }

    public class Data
    {
        public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
    }
}