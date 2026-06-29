using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class FateDetermined : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(7m, (ValueProp)8),
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0)))),
		new DynamicVar("sickeningNeed", 10m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public FateDetermined()
		: base(1, (CardType)1, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		FateDetermined card = this;
		if ((decimal)((CardModel)card).CurrentTarget.GetPowerAmount<Corruption>() >= ((CardModel)card).DynamicVars["sickeningNeed"].BaseValue - 1m)
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).WithHitCount(2).FromCard((CardModel)(object)card)
				.Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
		}
		else
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)card).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(2m);
		((CardModel)this).DynamicVars["sickeningNeed"].UpgradeValueBy(-2m);
	}
}
