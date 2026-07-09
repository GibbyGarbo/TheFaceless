using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.Cards;

public class DefendFaceless() : TheFacelessCard(1, (CardType)2, (CardRarity)1, (TargetType)1)
{
	public override bool GainsBlock => true;

	protected override HashSet<CardTag> CanonicalTags => [(CardTag)2];

	protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5m, (ValueProp)8)];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		DefendFaceless defendFaceless = this;
		await CreatureCmd.GainBlock(defendFaceless.Owner.Creature, defendFaceless.DynamicVars.Block, play);
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Block.UpgradeValueBy(3m);
	}
}
