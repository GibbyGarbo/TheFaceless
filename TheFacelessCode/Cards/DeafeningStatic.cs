using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class DeafeningStatic : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		new BlockVar(10m, (ValueProp)8),
		new PowerVar<Corruption>(10m)
	];

	public DeafeningStatic()
		: base(2, (CardType)2, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		DeafeningStatic card = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		await CreatureCmd.GainBlock(card.Owner.Creature, card.DynamicVars.Block, play);
		await PowerCmd.Apply<Corruption>(choiceContext, card.Owner.Creature, this.DynamicVars["Corruption"].BaseValue, this.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		EnergyCost.UpgradeBy(-1);
	}
}
