using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class TrepidationWave : TheFacelessCard
{
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

	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(11m, (ValueProp)8),
		(DynamicVar)new PowerVar<Paranoia>(1m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Paranoia>((int?)null) };

	public TrepidationWave()
		: base(1, (CardType)1, (CardRarity)4, (TargetType)3)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		TrepidationWave card = this;
		await CreatureCmd.TriggerAnim(((CardModel)card).Owner.Creature, "Attack", ((CardModel)card).Owner.Character.AttackAnimDelay);
		DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)card, play).TargetingAllOpponents(((CardModel)card).CombatState)
			.Execute(choiceContext);
		foreach (Creature hittableEnemy in ((CardModel)card).CombatState.HittableEnemies)
		{
			await PowerCmd.Apply<Paranoia>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["Paranoia"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(4m);
	}
}
