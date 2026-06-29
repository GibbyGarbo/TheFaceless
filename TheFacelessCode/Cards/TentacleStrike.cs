using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;

[Pool(typeof(TokenCardPool))]
public class TentacleStrike : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new DamageVar(6m, (ValueProp)8),
		(DynamicVar)new PowerVar<Corruption>(6m)
	];

	protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { (CardTag)1 };

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => (IEnumerable<IHoverTip>)(object)new IHoverTip[2]
	{
		HoverTipFactory.FromPower<Corruption>((int?)null),
		HoverTipFactory.FromKeyword((CardKeyword)1)
	};

	public TentacleStrike()
		: base(0, (CardType)1, (CardRarity)7, (TargetType)2)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		TentacleStrike card = this;
		await DamageCmd.Attack(((DynamicVar)((CardModel)this).DynamicVars.Damage).BaseValue).FromCard((CardModel)(object)this).Targeting(play.Target)
			.WithHitFx("vfx/vfx_attack_slash", (string)null, (string)null)
			.Execute(choiceContext);
		await PowerCmd.Apply<Corruption>(choiceContext, ((CardModel)card).CurrentTarget, ((CardModel)this).DynamicVars["Corruption"].BaseValue, ((CardModel)this).Owner.Creature, (CardModel)(object)this, false);
	}

	protected override void OnUpgrade()
	{
		DynamicVarSetExtensions.Power<Corruption>(((CardModel)this).DynamicVars).UpgradeValueBy(2m);
		AddKeyword(CardKeyword.Retain);
	}
}
