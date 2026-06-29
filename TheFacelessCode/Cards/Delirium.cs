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

public class Delirium : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<Corruption>(30m)];

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public Delirium()
		: base(3, (CardType)2, (CardRarity)4, CustomTargetType.Anyone)
	{
	}//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Delirium card = this;
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(10m);
	}
}
