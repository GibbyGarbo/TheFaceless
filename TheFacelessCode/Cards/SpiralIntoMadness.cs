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

public class SpiralIntoMadness : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => Array.Empty<DynamicVar>();

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips
	{
		get
		{
			List<IHoverTip> list = new List<IHoverTip>();
			list.Add(HoverTipFactory.FromKeyword((CardKeyword)1));
			list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>(1));
			return new List<IHoverTip>(list);
		}
	}

	public SpiralIntoMadness()
		: base(2, (CardType)2, (CardRarity)4, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		SpiralIntoMadness source = this;
		EnchantmentModel spiralIntoMadness = (EnchantmentModel)(object)ModelDb.Enchantment<DejaVu>();
		CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, 999999999);
		PileTypeExtensions.GetPile((PileType)6, ((CardModel)source).Owner).Cards.Where((CardModel c) => spiralIntoMadness.CanEnchant(c)).ToList();
		foreach (CardModel item in await CardSelectCmd.FromHand(choiceContext, ((CardModel)source).Owner, prefs, (Func<CardModel, bool>)null, (AbstractModel)(object)source))
		{
			CardCmd.Enchant<DejaVu>(item, 1m);
		}
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).AddKeyword((CardKeyword)5);
	}
}
