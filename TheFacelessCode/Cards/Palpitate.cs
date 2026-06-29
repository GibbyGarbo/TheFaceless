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

public class Palpitate : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(9m, (ValueProp)8),
		(DynamicVar)new EnergyVar(1)
	];

	public Palpitate()
		: base(1, (CardType)1, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Palpitate card = this;
		await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
		if (((CardModel)this).CurrentTarget.GetPowerAmount<Corruption>() > 4)
		{
			await PlayerCmd.GainEnergy(((DynamicVar)((CardModel)this).DynamicVars.Energy).BaseValue, ((CardModel)card).Owner);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(2m);
	}
}
