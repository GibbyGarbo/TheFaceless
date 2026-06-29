using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Terrorize : TheFacelessCard
{
	protected override bool HasEnergyCostX => true;

	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		(DynamicVar)new DamageVar(5m, (ValueProp)8),
		(DynamicVar)new PowerVar<Corruption>(3m),
		(DynamicVar)new PowerVar<Paranoia>(1m)
			];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => 
	[
		HoverTipFactory.FromPower<Corruption>((int?)null),
		HoverTipFactory.FromPower<Paranoia>((int?)null)
	];

	public Terrorize()
		: base(0, (CardType)1, (CardRarity)4, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Terrorize card = this;
		decimal corruption = (decimal)((CardModel)card).ResolveEnergyXValue() * DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).BaseValue;
		decimal paranoia = (decimal)((CardModel)card).ResolveEnergyXValue() * DynamicVarSetExtensions.Power<Paranoia>(((CardModel)this).DynamicVars).BaseValue;
		if (corruption > 0m)
		{
			if (((CardModel)card).CurrentTarget != null)
			{
				await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, corruption, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
				await PowerCmd.Apply<Paranoia>(choiceContext, ((CardModel)card).CurrentTarget, paranoia, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			}
			if (play.Target != null)
			{
				await DamageCmd.Attack(((DynamicVar)((CardModel)card).DynamicVars.Damage).BaseValue).WithHitCount(((CardModel)card).ResolveEnergyXValue()).FromCard((CardModel)(object)card)
					.Targeting(play.Target)
					.Execute(choiceContext);
			}
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(2m);
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(3m);
		DynamicVarSetExtensions.Power<Paranoia>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
