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
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class MassHysteria : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(9m, (ValueProp)8),
		(DynamicVar)new PowerVar<Corruption>(5m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public MassHysteria()
		: base(2, (CardType)1, (CardRarity)2, (TargetType)3)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		MassHysteria card = this;
		await CreatureCmd.TriggerAnim(((CardModel)card).Owner.Creature, "Attack", ((CardModel)card).Owner.Character.AttackAnimDelay);
		DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)card, play).TargetingAllOpponents(((CardModel)card).CombatState)
			.Execute(choiceContext);
		foreach (Creature hittableEnemy in ((CardModel)card).CombatState.HittableEnemies)
		{
			await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(1m);
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(3m);
	}
}
