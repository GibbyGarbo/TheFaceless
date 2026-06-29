using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

public class ArkTrap : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => Array.Empty<DynamicVar>();

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

	public ArkTrap()
		: base(3, (CardType)2, (CardRarity)4, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		ArkTrap arkTrap = this;
		new List<CardModel>();
		await CreatureCmd.TriggerAnim(((CardModel)arkTrap).Owner.Creature, "Cast", ((CardModel)arkTrap).Owner.Character.CastAnimDelay);
		IEnumerable<CardModel> list = PileTypeExtensions.GetPile((PileType)4, ((CardModel)arkTrap).Owner).Cards.Where((CardModel c) => c.Enchantment is DejaVu).ToList();
		foreach (CardModel item in list)
		{
			await CardPileCmd.Add(item, (PileType)1, (CardPilePosition)3, (AbstractModel)null, false);
		}
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).EnergyCost.UpgradeBy(-1);
	}
}
