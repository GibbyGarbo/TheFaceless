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
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Attenuate() : TheFacelessCard(1, (CardType)2, (CardRarity)3, (TargetType)3)
{
	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		new PowerVar<Corruption>(5m),
		new PowerVar<WeakPower>(1m)
	];

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		HoverTipFactory.FromPower<Corruption>(),
		HoverTipFactory.FromPower<WeakPower>(),
		HoverTipFactory.FromKeyword((CardKeyword)1)
	];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Attenuate card = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		foreach (Creature hittableEnemy in card.CombatState.HittableEnemies)
		{
			await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy, DynamicVars["Corruption"].BaseValue, Owner.Creature, this);
			await PowerCmd.Apply<WeakPower>(choiceContext, hittableEnemy, DynamicVars["WeakPower"].BaseValue, Owner.Creature, this);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Power<Corruption>().UpgradeValueBy(3m);
		DynamicVars.Power<WeakPower>().UpgradeValueBy(1m);
	}
}
