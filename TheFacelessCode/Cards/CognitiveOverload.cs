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

public class CognitiveOverload : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		new BlockVar(7m, (ValueProp)8),
		new CardsVar(1),
		new CalculationBaseVar(0m),
		new CalculationExtraVar(1m),
		new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((card, target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0)))),
		new DynamicVar("sickeningNeed", 5m),
		new DynamicVar("Sickening", 0m).WithTooltip<DynamicVar>("THEFACELESS-SICKENING")
	];

	public CognitiveOverload()
		: base(1, (CardType)2, (CardRarity)2, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		CognitiveOverload card1 = this;
		if (card1.Owner.Creature.GetPowerAmount<Corruption>() >= card1.DynamicVars["sickeningNeed"].BaseValue - 1m)
		{
			await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(card1.Owner.Creature, card1.DynamicVars.Block, play);
			await CardPileCmd.Draw(choiceContext, card1.DynamicVars.Cards.BaseValue, card1.Owner);
		}
		else
		{
			await CreatureCmd.GainBlock(card1.Owner.Creature, card1.DynamicVars.Block, play);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Block.UpgradeValueBy(4m);
	}
}
