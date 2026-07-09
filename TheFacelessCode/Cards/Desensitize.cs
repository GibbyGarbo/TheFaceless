using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Desensitize : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(7m, (ValueProp)8),
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0)))),
		new DynamicVar("sickeningNeed", 10m),
		(DynamicVar)new PowerVar<VulnerablePower>(1m)
	];

	public Desensitize()
		: base(1, (CardType)1, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Desensitize card = this;
		if ((decimal)((CardModel)card).CurrentTarget.GetPowerAmount<Corruption>() >= ((CardModel)card).DynamicVars["sickeningNeed"].BaseValue - 1m)
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).FromCard(card, play).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
			await PowerCmd.Apply<VulnerablePower>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["VulnerablePower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
		else
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).FromCard(card, play).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<VulnerablePower>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
