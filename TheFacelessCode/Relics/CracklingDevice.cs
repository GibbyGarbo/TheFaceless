using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Relics;


public class CracklingDevice : TheFacelessRelic
{
    private bool _dexterityApplied;
    
    public override RelicRarity Rarity =>
        RelicRarity.Rare;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<DexterityPower>(3),
        new DynamicVar("CorruptionThreshold", 14)
    ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<Corruption>(),
        HoverTipFactory.FromPower<DexterityPower>()
    ];

    private bool DexterityApplied
    {
        get => _dexterityApplied;
        set
        {
            AssertMutable();
            _dexterityApplied = value;
        }
    }
    
    
    public override Task AfterCombatEnd(CombatRoom _)
    {
        DexterityApplied = false;
        Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, Decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (!CombatManager.Instance.IsInProgress || power is not Corruption || power.Owner != applier)
            return;
        await ModifyDexterityIfNecessary(choiceContext);
    }

    private async Task ModifyDexterityIfNecessary(PlayerChoiceContext choiceContext)
    {
        Creature creature = Owner.Creature;
       // bool flag = creature.GetPowerAmount<Corruption>() > DynamicVars["CorruptionThreshold"].BaseValue;
       // Status = flag ? RelicStatus.Normal : RelicStatus.Active;
        Decimal baseValue = DynamicVars.Dexterity.BaseValue;
        if (creature.GetPowerAmount<Corruption>() < 15 && DexterityApplied)
        {
            Flash();
            await PowerCmd.Apply<DexterityPower>(choiceContext, creature, -baseValue, creature, null);
            DexterityApplied = false;
        }
        else if (creature.GetPowerAmount<Corruption>() >= 15 && !DexterityApplied)
        {
            Flash();
            await PowerCmd.Apply<DexterityPower>(choiceContext, creature, baseValue, creature, null);
            DexterityApplied = true;
        }
        else
        {
            DynamicVars.Dexterity.UpgradeValueBy(0);
        }
    }
}