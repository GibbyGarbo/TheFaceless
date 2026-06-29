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

public class Attenuate : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[2]
	{
		new PowerVar<Corruption>(5m),
		new PowerVar<WeakPower>(1m)
	};

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[3]
	{
		HoverTipFactory.FromPower<Corruption>(),
		HoverTipFactory.FromPower<WeakPower>(),
		HoverTipFactory.FromKeyword((CardKeyword)1)
	};

	public Attenuate()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)3)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Attenuate card = this;
		foreach (Creature hittableEnemy in ((CardModel)card).CombatState.HittableEnemies)
		{
			await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			await PowerCmd.Apply<WeakPower>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["WeakPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(3m);
		DynamicVarSetExtensions.Power<WeakPower>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
