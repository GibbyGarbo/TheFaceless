using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class OneWithNonsense : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<OneWithNonsensePower>(4m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public OneWithNonsense()
		: base(1, (CardType)3, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		OneWithNonsense card = this;
		await PowerCmd.Apply<OneWithNonsensePower>(choiceContext, card.Owner.Creature, this.DynamicVars["OneWithNonsensePower"].BaseValue, this.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<OneWithNonsensePower>(((CardModel)this).DynamicVars).UpgradeValueBy(2m);
	}
}
