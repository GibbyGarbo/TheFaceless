using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Compromised : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		new DynamicVar("EveryNum", 5m),
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedVulnerable").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0) / card.DynamicVars["EveryNum"].BaseValue)))
	];

	public Compromised()
		: base(1, (CardType)2, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Compromised card = this;
		await PowerCmd.Apply<VulnerablePower>(choiceContext, play.Target, ((CalculatedVar)((CardModel)card).DynamicVars["CalculatedVulnerable"]).Calculate(play.Target), ((CardModel)card).Owner.Creature, (CardModel)(object)card, false);
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).DynamicVars["EveryNum"].UpgradeValueBy(-2m);
	}
}
