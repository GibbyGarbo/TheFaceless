using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Hypochondria : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<HypochondriaPower>(1m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[2]
	{
		HoverTipFactory.FromPower<Paranoia>((int?)null),
		HoverTipFactory.FromPower<WeakPower>((int?)null)
	};

	public Hypochondria()
		: base(1, (CardType)3, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Hypochondria card = this;
		await PowerCmd.Apply<HypochondriaPower>(choiceContext, ((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars["HypochondriaPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<HypochondriaPower>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
