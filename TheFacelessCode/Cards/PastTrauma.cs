using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class PastTrauma : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		(DynamicVar)new DamageVar(5m, (ValueProp)8),
		(DynamicVar)new PowerVar<Corruption>(5m)
	];

	public PastTrauma()
		: base(1, (CardType)1, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		PastTrauma card = this;
		if (((EnchantmentModel)ModelDb.Enchantment<DejaVu>()).CanEnchant((CardModel)(object)card))
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
			await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
			CardCmd.Enchant<DejaVu>((CardModel)(object)card, 1m);
		}
		else
		{
			await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this).Targeting(play.Target)
				.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
				.Execute(choiceContext);
			await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(2m);
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(2m);
	}
}
