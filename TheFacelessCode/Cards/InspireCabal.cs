using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class InspireCabal : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new PowerVar<InspireCabalPower>(5m),
		(DynamicVar)new PowerVar<Corruption>(5m),
		(DynamicVar)new PowerVar<RitualPower>(1m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => 
	[
		HoverTipFactory.FromPower<Corruption>((int?)null),
		HoverTipFactory.FromPower<RitualPower>((int?)null)
	];

	public InspireCabal()
		: base(0, (CardType)3, (CardRarity)4, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		InspireCabal card = this;
		PowerCmd.Apply<InspireCabalPower>(choiceContext, ((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars["InspireCabalPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).Owner.Creature, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		foreach (Creature hittableEnemy in ((CardModel)card).CombatState.HittableEnemies)
		{
			await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<Corruption>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			await PowerCmd.Apply<RitualPower>(choiceContext, hittableEnemy, ((CardModel)this).DynamicVars["RitualPower"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<InspireCabalPower>(((CardModel)this).DynamicVars).UpgradeValueBy(3m);
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(3m);
	}
}
