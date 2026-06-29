using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class ItTakesYou : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => Array.Empty<DynamicVar>();

	public ItTakesYou()
		: base(3, (CardType)2, (CardRarity)4, (TargetType)3)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		ItTakesYou card = this;
		foreach (Creature hittableEnemy in ((CardModel)card).CombatState.HittableEnemies)
		{
			int CorruptionAmount = hittableEnemy.GetPowerAmount<Corruption>();
			await CreatureCmd.Damage(choiceContext, hittableEnemy, (decimal)CorruptionAmount, (ValueProp)6, ((CardModel)this).Owner.Creature, (CardModel)(object)this);
		}
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).AddKeyword((CardKeyword)5);
	}
}
