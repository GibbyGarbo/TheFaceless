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

public class TooClose : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(12m, (ValueProp)8)];

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public TooClose()
		: base(2, (CardType)1, (CardRarity)4, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		TooClose card = this;
		int corruptionToApply = ((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>();
		await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, (decimal)corruptionToApply, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(4m);
	}
}
