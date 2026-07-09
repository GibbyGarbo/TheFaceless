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

public class ArkTrap() : TheFacelessCard(3, (CardType)2, (CardRarity)4, (TargetType)1)
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

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		ArkTrap arkTrap = this;
		new List<CardModel>();
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		await CreatureCmd.TriggerAnim(arkTrap.Owner.Creature, "Cast", arkTrap.Owner.Character.CastAnimDelay);
		IEnumerable<CardModel> list = ((PileType)4).GetPile(arkTrap.Owner).Cards.Where(c => c.Enchantment is DejaVu).ToList();
		foreach (CardModel item in list)
		{
			await CardPileCmd.Add(item, (PileType)1, (CardPilePosition)3);
		}
	}

	protected override void OnUpgrade()
	{
		EnergyCost.UpgradeBy(-1);
	}
}
