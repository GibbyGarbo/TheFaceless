using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace TheFaceless.TheFacelessCode.Powers;

public class DontLookPower : TheFacelessPower
{
	public class Data
	{
		public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
	}

	public override PowerType Type => (PowerType)2;

	public override PowerStackType StackType => (PowerStackType)1;

	public override PowerInstanceType InstanceType => (PowerInstanceType)2;

	protected override object InitInternalData()
	{
		return new Data();
	}

	public override Task BeforeCardPlayed(CardPlay cardPlay)
	{
		Creature applier = ((PowerModel)this).Applier;
		if (((applier != null) ? applier.Player : null) == null || cardPlay.Card.Owner != ((PowerModel)this).Applier.Player)
		{
			return Task.CompletedTask;
		}
		((PowerModel)this).GetInternalData<Data>().amountsForPlayedCards.Add(cardPlay.Card, ((PowerModel)this).Amount);
		return Task.CompletedTask;
	}

	public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		DontLookPower dontLookPower = this;
		if (((PowerModel)dontLookPower).GetInternalData<Data>().amountsForPlayedCards.Remove(cardPlay.Card, out var amount))
		{
			((PowerModel)dontLookPower).Flash();
			await PowerCmd.Apply<Corruption>(choiceContext, ((PowerModel)dontLookPower).Owner, (decimal)amount, ((PowerModel)dontLookPower).Applier, (CardModel)null, false);
		}
	}

	public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		DontLookPower power = this;
		if ((int)side == 1)
		{
			await PowerCmd.Remove((PowerModel)(object)power);
		}
	}
}
