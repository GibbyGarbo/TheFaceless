using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class StaringBack : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => Array.Empty<DynamicVar>();

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	public StaringBack()
		: base(0, (CardType)2, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		StaringBack card = this;
		int playerCorruption = ((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>();
		int enemyCorruption = ((CardModel)card).CurrentTarget.GetPowerAmount<Corruption>();
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, (decimal)(-enemyCorruption), ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, (decimal)playerCorruption, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).Owner.Creature, (decimal)(-playerCorruption), ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).Owner.Creature, (decimal)enemyCorruption, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		RemoveKeyword(CardKeyword.Exhaust);
	}
}
