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

public class TemptingPath : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("EveryNum", 8m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public TemptingPath()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		TemptingPath card = this;
		decimal calculatedCards = (decimal)((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>() / ((CardModel)this).DynamicVars["EveryNum"].BaseValue;
		if ((decimal)((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>() >= ((CardModel)this).DynamicVars["EveryNum"].BaseValue)
		{
			await CardPileCmd.Draw(choiceContext, calculatedCards, ((CardModel)card).Owner, false);
		}
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).DynamicVars["EveryNum"].UpgradeValueBy(-3m);
	}
}
