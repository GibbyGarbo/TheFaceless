using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class EverPresent : TheFacelessCard
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
		(DynamicVar)new EnergyVar(2),
		(DynamicVar)new PowerVar<Paranoia>(1m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Paranoia>((int?)null) };

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	public EverPresent()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		EverPresent card = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<Paranoia>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Paranoia"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
		await PlayerCmd.GainEnergy((decimal)((DynamicVar)((CardModel)card).DynamicVars.Energy).IntValue, ((CardModel)card).Owner);
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Energy).UpgradeValueBy(1m);
	}
}
