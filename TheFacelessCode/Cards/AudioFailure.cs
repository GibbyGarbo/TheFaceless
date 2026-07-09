using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class AudioFailure : TheFacelessCard
{
	protected override bool HasEnergyCostX => true;

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		(DynamicVar)new BlockVar(5m, (ValueProp)8),
		(DynamicVar)new PowerVar<Corruption>(6m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Corruption>()];

	public AudioFailure()
		: base(0, (CardType)2, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		AudioFailure card = this;
		decimal block = card.ResolveEnergyXValue() * DynamicVars.Block.BaseValue;
		decimal corruption = card.ResolveEnergyXValue() * DynamicVars.Power<Corruption>().BaseValue;
		if (block > 0m)
		{
			await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<Corruption>(choiceContext, card.Owner.Creature, corruption, Owner.Creature, this);
			await CreatureCmd.GainBlock(card.Owner.Creature, block, (ValueProp)8, play);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Block).UpgradeValueBy(2m);
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(2m);
	}
}
