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

public class BadFeeling : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		new PowerVar<Corruption>(8),
		new PowerVar<Paranoia>(1)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		HoverTipFactory.FromPower<Corruption>(),
		HoverTipFactory.FromPower<Paranoia>(),
		HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
	];
	
	protected override bool ShouldGlowGoldInternal
	{
		get
		{
			if (((CardModel)this).CombatState != null)
			{
				return ((CardModel)this).CombatState.HittableEnemies.Any(delegate(Creature e)
				{
					MonsterModel monster = e.Monster;
					return monster != null && !monster.IntendsToAttack;
				});
			}
			return false;
		}
	}
	
	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	public BadFeeling()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)3)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		BadFeeling card = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		foreach (Creature hittableEnemy in ((CardModel)card).CombatState.HittableEnemies)
		{
			await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			await PowerCmd.Apply<Paranoia>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["Paranoia"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(4m);
		DynamicVarSetExtensions.Power<Paranoia>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
