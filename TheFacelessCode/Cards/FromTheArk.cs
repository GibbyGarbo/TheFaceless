using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

public class FromTheArk : TheFacelessCard
{
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

	public FromTheArk()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		FromTheArk fromTheArk = this;
		ModelDb.Enchantment<DejaVu>();
		CardModel card = CardFactory.GetDistinctForCombat(((CardModel)fromTheArk).Owner, ((CardModel)fromTheArk).Owner.Character.CardPool.GetUnlockedCards(((CardModel)fromTheArk).Owner.UnlockState, ((CardModel)fromTheArk).Owner.RunState.CardMultiplayerConstraint), 1, ((CardModel)fromTheArk).Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();
		if (card != null)
		{
			card.SetToFreeThisTurn();
			if (((CardModel)fromTheArk).IsUpgraded)
			{
				CardCmd.Enchant<DejaVu>(card, 1m);
				CardCmd.Upgrade(card, (CardPreviewStyle)1);
				await CardPileCmd.AddGeneratedCardToCombat(card, (PileType)2, ((CardModel)fromTheArk).Owner, (CardPilePosition)1);
			}
			else
			{
				CardCmd.Enchant<DejaVu>(card, 1m);
				await CardPileCmd.AddGeneratedCardToCombat(card, (PileType)2, ((CardModel)fromTheArk).Owner, (CardPilePosition)1);
			}
		}
	}
}
