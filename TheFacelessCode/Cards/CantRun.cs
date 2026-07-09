using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

public class CantRun : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => Array.Empty<DynamicVar>();

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips
	{
		get
		{
			List<IHoverTip> list = new List<IHoverTip>();
			list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>(1));
			list.Add(HoverTipFactory.FromKeyword((CardKeyword)1));
			return new List<IHoverTip>(list);
		}
	}

	public CantRun()
		: base(1, (CardType)2, (CardRarity)1, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		CantRun source = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		EnchantmentModel cantRun = ModelDb.Enchantment<DejaVu>();
		CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
		((PileType)6).GetPile(source.Owner).Cards.Where(c => cantRun.CanEnchant(c)).ToList();
		CardModel card = (await CardSelectCmd.FromHand(choiceContext, source.Owner, prefs, (Func<CardModel, bool>)(c => cantRun.CanEnchant(c)), source)).FirstOrDefault();
		if (card != null)
		{
			CardCmd.Enchant<DejaVu>(card, 1m);
		}
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).EnergyCost.UpgradeBy(-1);
	}
}
