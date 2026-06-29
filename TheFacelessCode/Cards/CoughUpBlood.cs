using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class CoughUpBlood : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<CoughUpBloodPower>(2m)];

	public CoughUpBlood()
		: base(1, (CardType)3, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		CoughUpBlood card = this;
		PowerCmd.Apply<CoughUpBloodPower>(choiceContext, ((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars["CoughUpBloodPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<CoughUpBloodPower>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
