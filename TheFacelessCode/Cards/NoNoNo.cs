using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class NoNoNo() : TheFacelessCard(2, (CardType)1, (CardRarity)3, (TargetType)3)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3m, (ValueProp)8), new RepeatVar(3)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Corruption>()];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		NoNoNo noNoNo = this;
		if (noNoNo.CombatState != null)
		{
			ArgumentNullException.ThrowIfNull(noNoNo.CombatState.HittableEnemies);
			foreach (Creature hittableEnemy in noNoNo.CombatState.HittableEnemies)
			{
					await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy,
						(await DamageCmd.Attack(noNoNo.DynamicVars.Damage.BaseValue).WithHitCount(noNoNo.DynamicVars.Repeat.IntValue)
							.FromCard(noNoNo, play).Targeting(hittableEnemy).WithHitFx("vfx/vfx_attack_slash")
							.Execute(choiceContext)).Results
						.SelectMany(
							r => r)
						.Sum(r => r.TotalDamage), noNoNo.Owner.Creature,
						noNoNo);
			}
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Damage.UpgradeValueBy(1m);
	}
}
