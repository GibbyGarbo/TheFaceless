using System;
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

public class NoNoNo : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3m, (ValueProp)8)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public NoNoNo()
		: base(3, (CardType)1, (CardRarity)3, (TargetType)3)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		NoNoNo noNoNo = this;
		_ = ((CardModel)noNoNo).CombatState.HittableEnemies.Count;
		ArgumentNullException.ThrowIfNull(((CardModel)noNoNo).CombatState.HittableEnemies, "noNoNo.CombatState.HittableEnemies");
		IEnumerable<Creature> hittableEnemies = ((CardModel)noNoNo).CombatState.HittableEnemies;
		await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemies, (decimal)(await DamageCmd.Attack(((DynamicVar)((CardModel)noNoNo).DynamicVars.Damage).BaseValue).WithHitCount(3).FromCard((CardModel)(object)noNoNo)
			.TargetingAllOpponents(((CardModel)noNoNo).CombatState)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext)).Results.SelectMany((List<DamageResult> r) => r).Sum((DamageResult r) => r.TotalDamage), ((CardModel)noNoNo).Owner.Creature, (CardModel)(object)noNoNo, false);
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(1m);
	}
}
