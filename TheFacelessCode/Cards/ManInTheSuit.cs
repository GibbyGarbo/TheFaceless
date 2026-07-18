using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class ManInTheSuit() : TheFacelessCard(0, (CardType)2, (CardRarity)3, (TargetType)1)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new("EveryNum", 12m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		HoverTipFactory.FromPower<Corruption>(),
		HoverTipFactory.FromPower<StrengthPower>(),
		HoverTipFactory.FromKeyword((CardKeyword)1)
	];

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		ManInTheSuit card = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		decimal calculatedStrength = card.Owner.Creature.GetPowerAmount<Corruption>() / DynamicVars["EveryNum"].BaseValue;
		if (card.Owner.Creature.GetPowerAmount<Corruption>() >= DynamicVars["EveryNum"].BaseValue)
		{
			await PowerCmd.Apply<StrengthPower>(choiceContext, card.Owner.Creature, calculatedStrength, card.Owner.Creature, card);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVars["EveryNum"].UpgradeValueBy(-4m);
	}
}
