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

public class Seizure : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		new DynamicVar("EveryNum", 5m),
		(DynamicVar)new CalculationBaseVar(5m),
		(DynamicVar)new ExtraDamageVar(3m),
		(DynamicVar)((CalculatedVar)new CalculatedDamageVar((ValueProp)8)).WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0) / card.DynamicVars["EveryNum"].BaseValue)))
	];

	public Seizure()
		: base(1, (CardType)1, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Seizure card = this;
		await DamageCmd.Attack(((CalculatedVar)((CardModel)this).DynamicVars.CalculatedDamage).Calculate(((CardModel)card).CurrentTarget)).FromCard((CardModel)(object)this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).DynamicVars["EveryNum"].UpgradeValueBy(-2m);
	}
}
