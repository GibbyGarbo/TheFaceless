using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Relics;

namespace TheFaceless.TheFacelessCode.Relics;


public class BurntTape() : TheFacelessRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Shop;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            List<IHoverTip> list = new List<IHoverTip>();
            list.AddRange(HoverTipFactory.FromEnchantment<DejaVu>(1));
            list.Add(HoverTipFactory.FromKeyword((CardKeyword)1));
            return new List<IHoverTip>(list);
        }
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (Owner.PlayerCombatState != null && (player != Owner || Owner.PlayerCombatState.TurnNumber != 1))
            return;
        Flash();
        CardModel card = CardFactory.GetDistinctForCombat(Owner, Owner.Character.CardPool.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint).Where(c => c.Type == CardType.Power), 1, Owner.RunState.Rng.CombatCardGeneration).First();
        CardCmd.Enchant<DejaVu>(card, 1m);
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, player);
    }
}