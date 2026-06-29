using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class ExacerbatingPresence : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => Array.Empty<DynamicVar>();

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	public ExacerbatingPresence()
		: base(3, (CardType)2, (CardRarity)4, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		ExacerbatingPresence card = this;
		int powerAmount = (play.Target.IsAlive ? play.Target.GetPowerAmount<Corruption>() : 0);
		int upgradedPowerAmmount = (play.Target.IsAlive ? play.Target.GetPowerAmount<Corruption>() : 0) * 2;
		if (powerAmount > 0)
		{
			if (((CardModel)card).IsUpgraded)
			{
				await PowerCmd.Apply<Corruption>(choiceContext, play.Target, (decimal)upgradedPowerAmmount, ((CardModel)card).Owner.Creature, (CardModel)(object)card, false);
			}
			else
			{
				await PowerCmd.Apply<Corruption>(choiceContext, play.Target, (decimal)powerAmount, ((CardModel)card).Owner.Creature, (CardModel)(object)card, false);
			}
		}
	}
}
