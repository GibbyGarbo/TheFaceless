using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class CorruptedFootage : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<Corruption>(8m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Corruption>()];

	public CorruptedFootage()
		: base(2, (CardType)2, (CardRarity)3, (TargetType)3)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		CorruptedFootage card = this;
		await PowerCmd.Apply<Corruption>(choiceContext, card.Owner.Creature, this.DynamicVars["Corruption"].BaseValue, this.Owner.Creature, this);
		foreach (Creature hittableEnemy in card.CombatState.HittableEnemies)
		{
			await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy, this.DynamicVars["Corruption"].BaseValue, this.Owner.Creature, this);
		}
	}

	protected override void OnUpgrade()
	{
		this.DynamicVars.Power<Corruption>().UpgradeValueBy(2m);
	}
}
