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

public class Mortify : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(10m, (ValueProp)8),
		(DynamicVar)new PowerVar<Corruption>(5m),
		new DynamicVar("sickeningNeed", 10m),
		DynamicVarExtensions.WithTooltip<DynamicVar>(new DynamicVar("Sickening", 0m), "THEFACELESS-SICKENING", "static_hover_tips")
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public Mortify()
		: base(1, (CardType)1, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		decimal sickCorruption = 10m;
		Mortify card = this;
		if ((decimal)((CardModel)card).Owner.Creature.GetPowerAmount<Corruption>() >= ((CardModel)card).DynamicVars["sickeningNeed"].BaseValue)
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this, play).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
			await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, sickCorruption, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
		else
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this, play).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
			await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(2m);
		((CardModel)this).DynamicVars["sickeningNeed"].UpgradeValueBy(-2m);
	}
}
