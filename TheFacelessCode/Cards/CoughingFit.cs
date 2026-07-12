using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class CoughingFit : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<Corruption>(7m)];

	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[1] { HoverTipFactory.FromPower<Corruption>((int?)null) };

	public CoughingFit()
		: base(0, (CardType)2, (CardRarity)2, CustomTargetType.Anyone)
	{
	}//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		CoughingFit card = this;
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		if (card.CurrentTarget != null)
			await PowerCmd.Apply<Corruption>(choiceContext, card.CurrentTarget, DynamicVars["Corruption"].BaseValue,
				Owner.Creature, this);
		else
		{
			await PowerCmd.Apply<Corruption>(choiceContext, Owner.Creature, DynamicVars["Corruption"].BaseValue,
				Owner.Creature, this);
		}
	}

	protected override void OnUpgrade()
	{
		RemoveKeyword((CardKeyword)1);
	}
}
