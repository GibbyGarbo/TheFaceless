using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class TooClose() : TheFacelessCard(2, (CardType)1, (CardRarity)4, (TargetType)2)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(12m, (ValueProp)8)];

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>() };

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		TooClose card = this;
		int corruptionToApply = card.Owner.Creature.GetPowerAmount<Corruption>();
		await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);
		await PowerCmd.Apply<Corruption>(choiceContext, card.CurrentTarget, corruptionToApply, Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(4m);
	}
}
