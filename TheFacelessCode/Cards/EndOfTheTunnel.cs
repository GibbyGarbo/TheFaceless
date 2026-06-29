using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.Cards;

public class EndOfTheTunnel : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new ExtraDamageVar(3m),
		(DynamicVar)((CalculatedVar)new CalculatedDamageVar((ValueProp)8)).WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature _) => CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => ((CombatHistoryEntry)e).HappenedThisTurn(card.CombatState) && e.CardPlay.Card.Owner == card.Owner)))
	];

	public EndOfTheTunnel()
		: base(0, (CardType)1, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		EndOfTheTunnel card = this;
		await DamageCmd.Attack(((CalculatedVar)((CardModel)this).DynamicVars.CalculatedDamage).Calculate(((CardModel)card).CurrentTarget)).FromCard((CardModel)(object)this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.ExtraDamage).UpgradeValueBy(1m);
	}
}
