using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

[Pool(typeof(TokenCardPool))]
public class TentacleStrike() : TheFacelessCard(0, (CardType)1, (CardRarity)7, (TargetType)2)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		new DamageVar(6, ValueProp.Move),
		new PowerVar<Corruption>(6)
	];

	protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Ethereal];

	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		HoverTipFactory.FromPower<Corruption>(),
		HoverTipFactory.FromKeyword(CardKeyword.Ethereal)
	];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		TentacleStrike card = this;
		if (play.Target != null)
			await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash")
				.Execute(choiceContext);
		if (card.CurrentTarget != null)
			await PowerCmd.Apply<Corruption>(choiceContext, card.CurrentTarget,
				DynamicVars["Corruption"].BaseValue, Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Power<Corruption>().UpgradeValueBy(2);
		DynamicVars.Damage.UpgradeValueBy(2);
	}
}
