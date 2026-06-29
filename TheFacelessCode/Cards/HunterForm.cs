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

public class HunterForm : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new PowerVar<HunterFormPower>(1m),
		(DynamicVar)new PowerVar<HunterFormUpgradedPower>(1m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[2]
	{
		HoverTipFactory.FromPower<Corruption>((int?)null),
		HoverTipFactory.FromCard<TentacleStrike>(((CardModel)this).IsUpgraded)
	};

	public HunterForm()
		: base(3, (CardType)3, (CardRarity)4, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		HunterForm card = this;
		if (((CardModel)card).IsUpgraded)
		{
			await PowerCmd.Apply<HunterFormUpgradedPower>(choiceContext, ((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars["HunterFormUpgradedPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
		else
		{
			await PowerCmd.Apply<HunterFormPower>(choiceContext, ((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars["HunterFormPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
	}
}
