using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Cards;

public class ImpossibleOutcome : TheFacelessCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars => 
	[
		(DynamicVar)new CardsVar(2),
		(DynamicVar)new BlockVar(8m, (ValueProp)8)
	];

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
		[CardKeyword.Exhaust];

	protected override IEnumerable<IHoverTip> ExtraHoverTips
	{
		get
		{
			List<IHoverTip> list = new List<IHoverTip>();
			list.Add(HoverTipFactory.FromKeyword((CardKeyword)1));
			list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>(1));
			return new List<IHoverTip>(list);
		}
	}

	public ImpossibleOutcome()
		: base(1, (CardType)2, (CardRarity)3, (TargetType)1)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		ImpossibleOutcome card1 = this;
		EnchantmentModel impossibleOutcome = (EnchantmentModel)(object)ModelDb.Enchantment<DejaVu>();
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		await CreatureCmd.GainBlock(((CardModel)card1).Owner.Creature, ((CardModel)card1).DynamicVars.Block, play, false);
		foreach (CardModel item in IEnumerableExtensions.TakeRandom<CardModel>(PileTypeExtensions.GetPile((PileType)3, ((CardModel)card1).Owner).Cards.Where((CardModel c) => impossibleOutcome.CanEnchant(c)), ((DynamicVar)((CardModel)card1).DynamicVars.Cards).IntValue, ((CardModel)card1).Owner.RunState.Rng.CombatCardSelection))
		{
			CardCmd.Enchant<DejaVu>(item, 1m);
			CardCmd.Preview(item, 1.2f, (CardPreviewStyle)1);
		}
	}

	protected override void OnUpgrade()
	{
		((DynamicVar)((CardModel)this).DynamicVars.Cards).UpgradeValueBy(1m);
		((DynamicVar)((CardModel)this).DynamicVars.Block).UpgradeValueBy(2m);
	}
}
