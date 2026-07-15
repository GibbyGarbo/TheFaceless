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
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		int playerCorruption = card.Owner.Creature.GetPowerAmount<Corruption>();
		var currentTarget = card.CurrentTarget;
		if (currentTarget != null)
		{
			int enemyCorruption = currentTarget.GetPowerAmount<Corruption>();
			await PowerCmd.Apply<Corruption>(choiceContext, currentTarget, -enemyCorruption, Owner.Creature, this);
			await PowerCmd.Apply<Corruption>(choiceContext, currentTarget, playerCorruption, Owner.Creature, this);
			await PowerCmd.Apply<Corruption>(choiceContext, card.Owner.Creature, -playerCorruption, Owner.Creature, this);
			await PowerCmd.Apply<Corruption>(choiceContext, card.Owner.Creature, enemyCorruption, Owner.Creature, this);
		}
	}

	protected override void OnUpgrade()
	{
		RemoveKeyword(CardKeyword.Exhaust);
	}
}
