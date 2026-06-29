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

public class BreakingPoint : TheFacelessCard
{
	public override bool GainsBlock => true;

	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0))))
	];

	public BreakingPoint()
		: base(1, (CardType)2, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		BreakingPoint breakingPoint = this;
		int strengthGain = ((CardModel)breakingPoint).CurrentTarget.GetPowerAmount<Corruption>();
		await PowerCmd.Apply<BreakingPointPower>(choiceContext, ((CardModel)breakingPoint).CurrentTarget, (decimal)strengthGain, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		int blockGain = ((CardModel)breakingPoint).Owner.Creature.GetPowerAmount<Corruption>();
		await CreatureCmd.GainBlock(((CardModel)breakingPoint).Owner.Creature, (decimal)blockGain, (ValueProp)8, play, false);
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).EnergyCost.UpgradeBy(-1);
	}
}
