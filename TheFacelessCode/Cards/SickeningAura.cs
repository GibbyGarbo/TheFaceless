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
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class SickeningAura : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new BlockVar(8m, (ValueProp)8),
		(DynamicVar)new PowerVar<Corruption>(5m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public SickeningAura()
		: base(1, (CardType)2, (CardRarity)2, CustomTargetType.Anyone)
	{
	}//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		SickeningAura card = this;
		await CreatureCmd.GainBlock(((CardModel)this).Owner.Creature, ((CardModel)this).DynamicVars.Block, play, false);
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(3m);
		((DynamicVar)((CardModel)this).DynamicVars.Block).UpgradeValueBy(3m);
	}
}
