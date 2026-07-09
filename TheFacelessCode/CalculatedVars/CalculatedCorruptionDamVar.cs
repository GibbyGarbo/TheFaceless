using System;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheFaceless.TheFacelessCode.CalculatedVars;


using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;

#nullable enable

public class CalculatedCorruptionDamVar : CalculatedVar
{
  public const string defaultName = "CalculatedCorruptionDam";

  public ValueProp Props { get; }

  public bool IsFromOsty { get; set; }

  /// <summary>
  /// Create a new <see cref="T:MegaCrit.Sts2.Core.Localization.DynamicVars.CalculatedDamageVar" />.
  /// This will only work if the owner is a <see cref="T:MegaCrit.Sts2.Core.Models.CardModel" /> whose <see cref="T:MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVarSet" /> also has a
  /// <see cref="T:MegaCrit.Sts2.Core.Localization.DynamicVars.CalculationBaseVar" /> and a <see cref="T:MegaCrit.Sts2.Core.Localization.DynamicVars.ExtraDamageVar" />.
  /// Note: For cards whose values are entirely dynamic and have no base value (like <see cref="T:MegaCrit.Sts2.Core.Models.Cards.BodySlam" />), you
  /// should use a <see cref="T:MegaCrit.Sts2.Core.Localization.DynamicVars.CalculationBaseVar" /> of 0 and a <see cref="T:MegaCrit.Sts2.Core.Localization.DynamicVars.ExtraDamageVar" /> of 1.
  /// </summary>
  public CalculatedCorruptionDamVar(ValueProp props)
    : base("CalculatedCorruptionDamage")
  {
    this.Props = props;
  }

  /// <summary>Set this damage to come from Osty.</summary>
  public CalculatedCorruptionDamVar FromOsty()
  {
    this.IsFromOsty = true;
    return this;
  }

  public override void UpdateCardPreview(
    CardModel card,
    CardPreviewMode previewMode,
    Creature? target,
    bool runGlobalHooks)
  {
    EnchantmentModel enchantment = card.Enchantment;
    if (enchantment != null)
    {
      Decimal baseValue = this.GetBaseVar().BaseValue;
      Decimal originalDamage = baseValue + enchantment.EnchantDamageAdditive(baseValue, this.Props);
      Decimal num = Math.Max(originalDamage * enchantment.EnchantDamageMultiplicative(originalDamage, this.Props), 0M);
      if (card.IsEnchantmentPreview)
        this.PreviewValue = num;
      else
        this.EnchantedValue = num;
    }
    Decimal num1 = this.Calculate(target);
    if (runGlobalHooks)
    {
      ICombatState combatState = card.CombatState ?? card.Owner.Creature.CombatState;
      this.PreviewValue = Hook.ModifyDamage(card.Owner.RunState, combatState, target, this.IsFromOsty ? card.Owner.Osty : card.Owner.Creature, num1, this.Props, card, (CardPlay) null, ModifyDamageHookType.All, previewMode, out IEnumerable<AbstractModel> _);
    }
    else if (!card.IsEnchantmentPreview)
    {
      if (enchantment != null)
      {
        Decimal originalDamage = num1 + enchantment.EnchantDamageAdditive(num1, this.Props);
        num1 = originalDamage * enchantment.EnchantDamageMultiplicative(originalDamage, this.Props);
      }
      this.PreviewValue = num1;
    }
    this.PreviewValue = Math.Max(this.PreviewValue, 0M);
  }

  protected override DynamicVar GetExtraVar()
  {
    return (DynamicVar) ((CardModel) this._owner).DynamicVars.ExtraDamage;
  }
}
