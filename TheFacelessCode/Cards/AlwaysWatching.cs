using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class AlwaysWatching() : TheFacelessCard(1,
	CardType.Skill, CardRarity.Basic,
	CustomTargetType.Anyone),
	ITranscendenceCard
{
	public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<Paralyze>();
	
	public override bool GainsBlock => true;
	
	
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		new CalculationBaseVar(0m),
		new ExtraDamageVar(1m),
		new CalculatedDamageVar((ValueProp)12).WithMultiplier((Func<CardModel, Creature, decimal>)((_, target) => (target != null) ? target.GetPowerAmount<Corruption>() : 0)),
		new PowerVar<Corruption>(5m),
		new DynamicVar("CorruptionInt", 5m),
		new CalculationExtraVar(1m),
		new CalculatedVar("CalculatedCorruption").WithMultiplier((Func<CardModel, Creature, decimal>)((card, target) => Math.Floor(((target != null) ? target.GetPowerAmount<Corruption>() : 0) + card.DynamicVars["CorruptionInt"].BaseValue)))
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Corruption>()];





	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		AlwaysWatching card = this;
		if (card.CurrentTarget != null && card.CurrentTarget.IsMonster)
		{
			await PowerCmd.Apply<Corruption>(choiceContext, card.CurrentTarget, this.DynamicVars["Corruption"].BaseValue, this.Owner.Creature, this);
			await CreatureCmd.Damage(choiceContext, card.CurrentTarget, new DamageVar(this.DynamicVars.CalculatedDamage.Calculate(card.CurrentTarget), (ValueProp)4), card);
		}
		else if (card.CurrentTarget != null && card.CurrentTarget.IsPlayer)
		{
			await PowerCmd.Apply<Corruption>(choiceContext, card.CurrentTarget, this.DynamicVars["Corruption"].BaseValue, this.Owner.Creature, this);
			int blockGain = card.Owner.Creature.GetPowerAmount<Corruption>();
			await CreatureCmd.GainBlock(card.Owner.Creature, blockGain, (ValueProp)8, play);
		}
		else
		{
			await PowerCmd.Apply<Corruption>(choiceContext, card.Owner.Creature, this.DynamicVars["Corruption"].BaseValue, this.Owner.Creature, this);
			int blockGain2 = card.Owner.Creature.GetPowerAmount<Corruption>();
			await CreatureCmd.GainBlock(card.Owner.Creature, blockGain2, (ValueProp)8, play);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Power<Corruption>().UpgradeValueBy(2m);
		DynamicVars["CorruptionInt"].UpgradeValueBy(2m);
	}
	

	
}
