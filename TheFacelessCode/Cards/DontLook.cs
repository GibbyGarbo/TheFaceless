using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class DontLook : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DontLookPower>(3m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	public DontLook()
		: base(0, (CardType)2, (CardRarity)4, CustomTargetType.Anyone)
	{
	}//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		DontLook cardSource = this;
		ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
		await CreatureCmd.TriggerAnim(((CardModel)cardSource).Owner.Creature, "Cast", ((CardModel)cardSource).Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<DontLookPower>(choiceContext, play.Target, DynamicVarSetExtensions.Power<DontLookPower>(((CardModel)cardSource).DynamicVars).BaseValue, ((CardModel)cardSource).Owner.Creature, (CardModel)(object)cardSource, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<DontLookPower>(((CardModel)this).DynamicVars).UpgradeValueBy(2m);
	}
}
