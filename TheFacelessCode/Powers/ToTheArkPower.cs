using System.Linq;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class ToTheArkPower : TheFacelessPower
{
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
	
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
	{
		
		ToTheArkPower toTheArkPower = this;
		ModelDb.Enchantment<DejaVu>();
		if (player != ((PowerModel)toTheArkPower).Owner.Player)
		{
			return;
		}
		int i = 0;
		while (i < ((PowerModel)toTheArkPower).Amount)
		{
			CardModel card = CardFactory.GetDistinctForCombat(player, player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint), 1, player.RunState.Rng.CombatCardGeneration).FirstOrDefault();
			if (card != null)
			{
				CardCmd.Enchant<DejaVu>(card, 1m);
				await CardPileCmd.AddGeneratedCardToCombat(card, (PileType)2, ((PowerModel)toTheArkPower).Owner.Player, (CardPilePosition)1);
			}
			int num = i + 1;
			i = num;
		}
	}
}
