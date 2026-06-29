using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class AroundTheCorner() : TheFacelessCard(1,
		CardType.Attack, CardRarity.Uncommon,
		TargetType.Self)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		new DamageVar(10, ValueProp.Move),
		new PowerVar<Paranoia>(1)
	];

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
	[
		CardKeyword.Innate,
		CardKeyword.Exhaust,
		CardKeyword.Ethereal
	];
	
	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
		HoverTipFactory.FromKeyword(CardKeyword.Innate),
		HoverTipFactory.FromKeyword(CardKeyword.Ethereal)
	];



	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		AroundTheCorner card = this;
		await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);
		await PowerCmd.Apply<Paranoia>(choiceContext, card.CurrentTarget, DynamicVars["Paranoia"].BaseValue, Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Damage.UpgradeValueBy(4m);
		DynamicVars.Power<Paranoia>().UpgradeValueBy(1m);
	}
}
