using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class BeyondReckoning : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new CardsVar(2),
		(DynamicVar)new PowerVar<Paranoia>(2m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips
	{
		get
		{
			List<IHoverTip> list = new List<IHoverTip>();
			list.Add(HoverTipFactory.FromKeyword((CardKeyword)1));
			list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>(1));
			list.Add(HoverTipFactory.FromPower<Paranoia>((int?)null));
			return new List<IHoverTip>(list);
		}
	}

	public BeyondReckoning()
		: base(0, (CardType)2, (CardRarity)4, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		BeyondReckoning card = this;
		if (((EnchantmentModel)ModelDb.Enchantment<DejaVu>()).CanEnchant((CardModel)(object)card))
		{
			await CardPileCmd.Draw(choiceContext, ((DynamicVar)((CardModel)card).DynamicVars.Cards).BaseValue, ((CardModel)card).Owner, false);
			if (((CardModel)card).CurrentTarget != null)
			{
				await PowerCmd.Apply<Paranoia>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Paranoia"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			}
			CardCmd.Enchant<DejaVu>((CardModel)(object)card, 1m);
		}
		else
		{
			await CardPileCmd.Draw(choiceContext, ((DynamicVar)((CardModel)card).DynamicVars.Cards).BaseValue, ((CardModel)card).Owner, false);
			if (((CardModel)card).CurrentTarget != null)
			{
				await PowerCmd.Apply<Paranoia>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Paranoia"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			}
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Cards).UpgradeValueBy(1m);
	}
}
