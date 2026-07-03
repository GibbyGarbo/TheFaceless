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
		new DamageVar(5m, (ValueProp)8),
		new PowerVar<Corruption>(3m),
		new PowerVar<Paranoia>(1m)
			];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => 
	[
		HoverTipFactory.FromPower<Corruption>(),
		HoverTipFactory.FromPower<Paranoia>(),
		HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
	];

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
	
	public Terrorize()
		: base(0, (CardType)1, (CardRarity)4, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Terrorize card = this;
		decimal corruption = card.ResolveEnergyXValue() * DynamicVars.Power<Corruption>().BaseValue;
		decimal paranoia = card.ResolveEnergyXValue() * DynamicVars.Power<Paranoia>().BaseValue;
		if (corruption > 0m)
		{
			if (card.CurrentTarget != null)
			{
				await PowerCmd.Apply<Corruption>(choiceContext, card.CurrentTarget, corruption, Owner.Creature, this);
				await PowerCmd.Apply<Paranoia>(choiceContext, card.CurrentTarget, paranoia, Owner.Creature, this);
			}
			if (play.Target != null)
			{
				await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(card.ResolveEnergyXValue()).FromCard(card)
					.Targeting(play.Target)
					.Execute(choiceContext);
			}
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Damage.UpgradeValueBy(2m);
		DynamicVars.Power<Corruption>().UpgradeValueBy(2m);
		DynamicVars.Power<Paranoia>().UpgradeValueBy(1m);
	}
}
