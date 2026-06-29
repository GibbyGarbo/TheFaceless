using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class TooLate : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [
	
		new DynamicVar("EveryNum", 5m),
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedHits").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? card.Owner.Creature.GetPowerAmount<Corruption>() : 0) / card.DynamicVars["EveryNum"].BaseValue) + 1m)),
		(DynamicVar)new DamageVar(4m, (ValueProp)8)
	];

	public TooLate()
		: base(2, (CardType)1, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		TooLate card = this;
		await DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).WithHitCount((int)((CalculatedVar)((CardModel)card).DynamicVars["CalculatedHits"]).Calculate(play.Target)).FromCard((CardModel)(object)card)
			.Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).DynamicVars["EveryNum"].UpgradeValueBy(-2m);
	}
}
