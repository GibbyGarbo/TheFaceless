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
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class CheckTheWindows : TheFacelessCard
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

	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<Paranoia>(2m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Paranoia>((int?)null) };

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	public CheckTheWindows()
		: base(1, (CardType)2, (CardRarity)2, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		CheckTheWindows card = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<Paranoia>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Paranoia"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Paranoia>(((CardModel)this).DynamicVars).UpgradeValueBy(1m);
	}
}
