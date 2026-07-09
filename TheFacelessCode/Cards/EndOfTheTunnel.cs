using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.Cards;

public class EndOfTheTunnel() : TheFacelessCard(0, (CardType)1, (CardRarity)3, (TargetType)2)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		new CalculationBaseVar(0m),
		new ExtraDamageVar(2m),
		new CalculatedDamageVar((ValueProp)8).WithMultiplier((Func<CardModel, Creature, decimal>)((card, _) => CombatManager.Instance.History.CardPlaysFinished.Count(e => e.HappenedThisTurn(card.CombatState) && e.CardPlay.Card.Owner == card.Owner)))
	];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		EndOfTheTunnel card = this;
		await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate(card.CurrentTarget)).FromCard(this, play).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);
	}

	protected override void OnUpgrade()
	{
		DynamicVars.ExtraDamage.UpgradeValueBy(1m);
	}
}
