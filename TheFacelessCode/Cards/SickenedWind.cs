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

public class SickenedWind : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0)))),
		new DynamicVar("sickeningNeed", 10m),
		(DynamicVar)new BlockVar(10m, (ValueProp)8),
		(DynamicVar)new PowerVar<WeakPower>(1m),
		DynamicVarExtensions.WithTooltip<DynamicVar>(new DynamicVar("Sickening", 0m), "THEFACELESS-SICKENING", "static_hover_tips")
	];

	public SickenedWind()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		SickenedWind card = this;
		if ((decimal)((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>() >= ((CardModel)card).DynamicVars["sickeningNeed"].BaseValue)
		{
			await CreatureCmd.GainBlock(((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars.Block, play, false);
			await PowerCmd.Apply<WeakPower>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["WeakPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
		else
		{
			await CreatureCmd.GainBlock(((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars.Block, play, false);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Block).UpgradeValueBy(2m);
		DynamicVarSetExtensions.Power<WeakPower>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
