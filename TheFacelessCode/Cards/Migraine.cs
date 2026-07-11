using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.CalculatedVars;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Migraine() : TheFacelessCard(2, (CardType)1, (CardRarity)4, (TargetType)2)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
		[
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new ExtraDamageVar(1m),
		(DynamicVar)new PowerVar<Corruption>(3m),
		new DynamicVar("CorruptionInt", 3m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)((CalculatedVar)new CalculatedCorruptionDamVar((ValueProp)8)).WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0) + card.DynamicVars["CorruptionInt"].BaseValue) * 2m)),
		(DynamicVar)((CalculatedVar)new CalculatedDamageVar((ValueProp)8)).WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0) * 2m)))
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Migraine card = this;
		await PowerCmd.Apply<Corruption>(choiceContext, card.CurrentTarget, this.DynamicVars["Corruption"].BaseValue, this.Owner.Creature, this);
		await DamageCmd.Attack(this.DynamicVars.CalculatedDamage.Calculate(card.CurrentTarget)).FromCard((CardModel)this, play).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
		((CardModel)this).DynamicVars["CorruptionInt"].UpgradeValueBy(1m);
	}
}
