using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class ManInTheSuit : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("EveryNum", 15m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[3]
	{
		HoverTipFactory.FromPower<Corruption>((int?)null),
		HoverTipFactory.FromPower<StrengthPower>((int?)null),
		HoverTipFactory.FromKeyword((CardKeyword)1)
	};

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	public ManInTheSuit()
		: base(0, (CardType)2, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		ManInTheSuit card = this;
		decimal calculatedStrength = (decimal)((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>() / ((CardModel)this).DynamicVars["EveryNum"].BaseValue;
		if ((decimal)((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>() >= ((CardModel)this).DynamicVars["EveryNum"].BaseValue)
		{
			await PowerCmd.Apply<StrengthPower>(choiceContext, ((CardModel)card).Owner.Creature, calculatedStrength, ((CardModel)card).Owner.Creature, (CardModel)(object)card, false);
		}
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).DynamicVars["EveryNum"].UpgradeValueBy(-5m);
	}
}
