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
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

public class EphemeralStrike : TheFacelessCard
{
	protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { (CardTag)1 };

	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6m, (ValueProp)8)];

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

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

	public EphemeralStrike()
		: base(1, (CardType)1, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		EphemeralStrike card = this;
		await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
		EnchantmentModel ephemeralStrike = (EnchantmentModel)(object)ModelDb.Enchantment<DejaVu>();
		CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
		PileTypeExtensions.GetPile((PileType)6, ((CardModel)card).Owner).Cards.Where((CardModel c) => ephemeralStrike.CanEnchant(c)).ToList();
		CardModel card2 = (await CardSelectCmd.FromHand(choiceContext, ((CardModel)card).Owner, prefs, (Func<CardModel, bool>)((CardModel c) => ephemeralStrike.CanEnchant(c)), (AbstractModel)(object)card)).FirstOrDefault();
		if (card2 != null)
		{
			CardCmd.Enchant<DejaVu>(card2, 1m);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(3m);
	}
}
