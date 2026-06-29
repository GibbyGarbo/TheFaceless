using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

public class Arrhythmia : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ArrhythmiaPower>(50m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Paranoia>()];

	public Arrhythmia()
		: base(1, (CardType)3, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Arrhythmia cardSource = this;
		await PowerCmd.Apply<ArrhythmiaPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars["ArrhythmiaPower"].BaseValue, cardSource.Owner.Creature, cardSource);
	}

	protected override void OnUpgrade()
	{
		DynamicVars["ArrhythmiaPower"].UpgradeValueBy(25m);
	}
}
