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
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class HauntTheWoods : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
		[
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0)))),
		new DynamicVar("sickeningNeed", 5m),
		(DynamicVar)new BlockVar(5m, (ValueProp)8),
		(DynamicVar)new DamageVar(7m, (ValueProp)8),
		DynamicVarExtensions.WithTooltip<DynamicVar>(new DynamicVar("Sickening", 0m), "THEFACELESS-SICKENING", "static_hover_tips")
	];

	public HauntTheWoods()
		: base(1, (CardType)1, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		HauntTheWoods card = this;
		if ((decimal)((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>() >= ((CardModel)card).DynamicVars["sickeningNeed"].BaseValue)
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this, play).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
			await CreatureCmd.GainBlock(((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars.Block, play, false);
		}
		else
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this, play).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Block).UpgradeValueBy(2m);
	}
}
