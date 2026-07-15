using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;


public class ConsequencesPower() : TheFacelessPower
{
    public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

    public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
    
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    
    public override async Task BeforeDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        ConsequencesPower consequencesPower = this;

         int damage = consequencesPower.Owner.GetPowerAmount<ConsequencesPower>() * dealer.Powers.Count(ShouldCountPower);
        if (target != consequencesPower.Owner || dealer == null || !props.IsPoweredAttack() && !(cardSource is Omnislice) && damage > 0)
            return;
        consequencesPower.Flash();
        await CreatureCmd.Damage(choiceContext, dealer, damage, ValueProp.Unpowered | ValueProp.SkipHurtAnim, consequencesPower.Owner, null, null);
    }
    
    public static bool ShouldCountPower(PowerModel power)
    {
        return power.TypeForCurrentAmount == PowerType.Debuff && !(power is ITemporaryPower);
    }
}
