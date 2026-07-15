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
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Inescapable : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new CalculationBaseVar(0m),
		(DynamicVar)new CalculationExtraVar(1m),
		(DynamicVar)new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((CardModel card, Creature target) => Math.Floor((decimal)((target != null) ? target.GetPowerAmount<Corruption>() : 0)))),
		new DynamicVar("sickeningNeed", 5m),
		DynamicVarExtensions.WithTooltip<DynamicVar>(new DynamicVar("Sickening", 0m), "THEFACELESS-SICKENING", "static_hover_tips")
	];

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	public Inescapable()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Inescapable card1 = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		EnchantmentModel inescapable = (EnchantmentModel)(object)ModelDb.Enchantment<DejaVu>();
		int sickening = ((CardModel)card1).Owner.Creature.GetPowerAmount<Corruption>();
		if (((CardModel)card1).IsUpgraded)
		{
			CardSelectorPrefs prefs =  new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			CardModel card2 = (await CardSelectCmd.FromCombatPile(choiceContext, PileTypeExtensions.GetPile((PileType)3, ((CardModel)card1).Owner), ((CardModel)card1).Owner, prefs)).FirstOrDefault();
			if (card2 != null)
			{
				await CardPileCmd.Add(card2, (PileType)2, (CardPilePosition)1, (AbstractModel)null, false);
				if (inescapable.CanEnchant(card2))
				{
					CardCmd.Enchant<DejaVu>(card2, 1m);
				}
			}
		}
		else
		{
			if (!((decimal)sickening >= ((CardModel)card1).DynamicVars["sickeningNeed"].BaseValue))
			{
				return;
			}
			CardSelectorPrefs prefs2 = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			CardModel card2 = (await CardSelectCmd.FromCombatPile(choiceContext, PileTypeExtensions.GetPile((PileType)3, ((CardModel)card1).Owner), ((CardModel)card1).Owner, prefs2)).FirstOrDefault();
			if (card2 != null)
			{
				await CardPileCmd.Add(card2, (PileType)2, (CardPilePosition)1, (AbstractModel)null, false);
				if (inescapable.CanEnchant(card2))
				{
					CardCmd.Enchant<DejaVu>(card2, 1m);
				}
			}
		}
	}
}
