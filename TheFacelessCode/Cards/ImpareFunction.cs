using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Powers;

namespace TheFaceless.TheFacelessCode.Cards;


public class ImpareFunction() : TheFacelessCard(3,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new PowerVar<Paranoia>(5),
    new PowerVar<WeakPower>(3),
    new PowerVar<VulnerablePower>(3)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<Corruption>(),
        HoverTipFactory.FromPower<WeakPower>(),
        HoverTipFactory.FromPower<VulnerablePower>()
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ImpareFunction card = this;
        if (card.CurrentTarget != null) 
            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<Paranoia>(choiceContext, card.CurrentTarget, DynamicVars.Power<Paranoia>().BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<WeakPower>(choiceContext, card.CurrentTarget, DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this); 
        await PowerCmd.Apply<VulnerablePower>(choiceContext, card.CurrentTarget, DynamicVars.Power<VulnerablePower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    DynamicVars.Power<Paranoia>().UpgradeValueBy(2);
    DynamicVars.Power<WeakPower>().UpgradeValueBy(1);
    DynamicVars.Power<VulnerablePower>().UpgradeValueBy(1);
    }
}