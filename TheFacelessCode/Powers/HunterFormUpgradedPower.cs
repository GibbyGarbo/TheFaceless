using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using TheFaceless.TheFacelessCode.Cards;

namespace TheFaceless.TheFacelessCode.Powers;

public class HunterFormUpgradedPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
	{
		HunterFormUpgradedPower hunterFormPower = this;
		if (player == ((PowerModel)hunterFormPower).Owner.Player)
		{
			int i = 0;
			while (i < ((PowerModel)hunterFormPower).Amount)
			{
				TentacleStrike tentacleStrike = ((PowerModel)hunterFormPower).CombatState.CreateCard<TentacleStrike>(((PowerModel)hunterFormPower).Owner.Player);
				CardCmd.Upgrade((CardModel)(object)tentacleStrike, (CardPreviewStyle)1);
				await CardPileCmd.AddGeneratedCardToCombat((CardModel)(object)tentacleStrike, (PileType)2, ((PowerModel)hunterFormPower).Owner.Player, (CardPilePosition)1);
				int num = i + 1;
				i = num;
			}
		}
	}
}
