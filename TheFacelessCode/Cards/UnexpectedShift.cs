using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

public class UnexpectedShift : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WeakPower>(1m)];

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

	public UnexpectedShift()
		: base(1, (CardType)2, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		UnexpectedShift source = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<WeakPower>(choiceContext, ((CardModel)source).CurrentTarget, ((CardModel)this).DynamicVars["WeakPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		EnchantmentModel cantRun = (EnchantmentModel)(object)ModelDb.Enchantment<DejaVu>();
		CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
		PileTypeExtensions.GetPile((PileType)6, ((CardModel)source).Owner).Cards.Where((CardModel c) => cantRun.CanEnchant(c)).ToList();
		CardModel card = (await CardSelectCmd.FromHand(choiceContext, ((CardModel)source).Owner, prefs, (Func<CardModel, bool>)((CardModel c) => cantRun.CanEnchant(c)), (AbstractModel)(object)source)).FirstOrDefault();
		if (card != null)
		{
			CardCmd.Enchant<DejaVu>(card, 1m);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<WeakPower>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
