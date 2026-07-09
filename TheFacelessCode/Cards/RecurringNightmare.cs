using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class RecurringNightmare : TheFacelessCard
{
	public override bool GainsBlock => true;

	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0)))),
		new DynamicVar("sickeningNeed", 5m),
		(DynamicVar)new BlockVar(5m, (ValueProp)8),
		DynamicVarExtensions.WithTooltip<DynamicVar>(new DynamicVar("Sickening", 0m), "THEFACELESS-SICKENING", "static_hover_tips")
	];

	public RecurringNightmare()
		: base(1, (CardType)2, (CardRarity)2, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		RecurringNightmare card1 = this;
		if ((decimal)((CardModel)card1).Owner.Creature.GetPowerAmount<Corruption>() >= ((CardModel)card1).DynamicVars["sickeningNeed"].BaseValue)
		{
			await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(((CardModel)card1).Owner.Creature, ((CardModel)card1).DynamicVars.Block, play, false);
			CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1);
			CardModel card2 = (await CardSelectCmd.FromCombatPile(choiceContext, PileTypeExtensions.GetPile((PileType)3, ((CardModel)card1).Owner), ((CardModel)card1).Owner, prefs)).FirstOrDefault();
			if (card2 != null)
			{
				await CardPileCmd.Add(card2, (PileType)2, (CardPilePosition)1, (AbstractModel)null, false);
			}
		}
		else
		{
			await CreatureCmd.GainBlock(((CardModel)card1).Owner.Creature, ((CardModel)card1).DynamicVars.Block, play, false);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Block).UpgradeValueBy(3m);
	}
}
