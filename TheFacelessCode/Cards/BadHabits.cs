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

public class BadHabits : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BadHabitsPower>(4m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Corruption>()];

	public BadHabits()
		: base(2, (CardType)3, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		BadHabits card = this;
		await PowerCmd.Apply<BadHabitsPower>(choiceContext, ((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars["BadHabitsPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).EnergyCost.UpgradeBy(-1);
	}
}
