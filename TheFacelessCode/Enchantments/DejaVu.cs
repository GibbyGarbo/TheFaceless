using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Enchantments;

public sealed class DejaVu : EnchantmentModel
{
	public const string _timesKey = "Times";

	public bool _usedThisCombat;

	protected override IEnumerable<DynamicVar> CanonicalVars => [(new("Times", 1))];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => 
	[
		HoverTipFactory.FromKeyword((CardKeyword)1),
		HoverTipFactory.Static((StaticHoverTip)14, DynamicVars["Times"])
	];

	public bool UsedThisCombat
	{
		get
		{
			return _usedThisCombat;
		}
		set
		{
			((AbstractModel)this).AssertMutable();
			_usedThisCombat = value;
		}
	}

	public override int EnchantPlayCount(int originalPlayCount)
	{
		if (!UsedThisCombat)
		{
			return originalPlayCount + ((EnchantmentModel)this).DynamicVars["Times"].IntValue + base._card.Owner.Creature.GetPowerAmount<IterationCyclePower>();
		}
		return originalPlayCount;
	}

	protected override void OnEnchant()
	{
		CardCmd.ApplyKeyword(((EnchantmentModel)this).Card, (CardKeyword[])(object)new CardKeyword[1] { (CardKeyword)1 });
	}
}
