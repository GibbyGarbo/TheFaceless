using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Relics;

public class TheSource : TheFacelessRelic
{
	public bool _usedThisCombat;

	public override RelicRarity Rarity => (RelicRarity)1;

	public override string FlashSfx => "event:/sfx/ui/relic_activate_draw";

	protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

	public bool UsedThisCombat
	{
		get
		{
			return _usedThisCombat;
		}
		set
		{
			if (_usedThisCombat != value)
			{
				AssertMutable();
				_usedThisCombat = value;
			}
		}
	}

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		TheSource theSource = this;
		if (CombatManager.Instance.IsInProgress && applier == theSource.Owner.Creature && power is Corruption && !theSource.UsedThisCombat)
		{
			theSource.UsedThisCombat = true;
			theSource.Flash();
			await CardPileCmd.Draw(choiceContext, theSource.DynamicVars.Cards.BaseValue, theSource.Owner);
		}
	}

	public override Task AfterCombatEnd(CombatRoom _)
	{
		UsedThisCombat = false;
		return Task.CompletedTask;
	}

	public override RelicModel GetUpgradeReplacement()
	{
		return (RelicModel)(object)ModelDb.Relic<TheArk>();
	}
}
