using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Extensions;
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

public class CutDeep : TheFacelessCard
{
	protected override bool ShouldGlowGoldInternal
	{
		get
		{
			if (((CardModel)this).CombatState != null)
			{
				return ((CardModel)this).CombatState.HittableEnemies.Any(delegate(Creature e)
				{
					MonsterModel monster = e.Monster;
					return monster != null && !monster.IntendsToAttack;
				});
			}
			return false;
		}
	}

	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(15m, (ValueProp)8),
		(DynamicVar)new PowerVar<Paranoia>(2m)
	];

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips
	{
		get
		{
			List<IHoverTip> list = new List<IHoverTip>();
			list.Add(HoverTipFactory.FromKeyword((CardKeyword)1));
			list.AddRange(HoverTipFactory.FromPowerWithPowerHoverTips<Paranoia>((int?)null));
			return new List<IHoverTip>(list);
		}
	}

	public CutDeep()
		: base(2, (CardType)1, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		CutDeep card = this;
		await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
		await PowerCmd.Apply<Paranoia>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Paranoia"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Damage).UpgradeValueBy(5m);
		DynamicVarSetExtensions.Power<Paranoia>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
