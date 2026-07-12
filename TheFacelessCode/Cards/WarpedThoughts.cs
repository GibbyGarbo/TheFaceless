using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class WarpedThoughts : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => (IEnumerable<DynamicVar>)(object)new DynamicVar[2]
	{
		(DynamicVar)new PowerVar<Corruption>(7m),
		(DynamicVar)new CardsVar(1)
	};

	public WarpedThoughts()
		: base(1, (CardType)2, (CardRarity)2, CustomTargetType.Anyone)
	{
	}//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		WarpedThoughts cardSource = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		if (CurrentTarget != null)
			await PowerCmd.Apply<Corruption>(choiceContext, CurrentTarget,
				((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature,
				(CardModel)(object)this, false);
		else
		{
			await PowerCmd.Apply<Corruption>(choiceContext, Owner.Creature, DynamicVars["Corruption"].BaseValue,
				Owner.Creature, this);
		}
		await CardPileCmd.Draw(choiceContext, ((DynamicVar)((CardModel)cardSource).DynamicVars.Cards).BaseValue, ((CardModel)cardSource).Owner, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(2m);
		((DynamicVar)((CardModel)this).DynamicVars.Cards).UpgradeValueBy(1m);
	}
}
