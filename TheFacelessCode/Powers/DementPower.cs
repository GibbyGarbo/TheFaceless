using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;

namespace TheFaceless.TheFacelessCode.Powers;

public class DementPower : TheFacelessPower
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		DementPower dementPower = this;
		if (!((cardPlay.Card.Owner == ((PowerModel)dementPower).Owner.Player) & !(cardPlay.Card.Enchantment is DejaVu)))
		{
			await CreatureCmd.GainBlock(((PowerModel)dementPower).Owner, (decimal)((PowerModel)dementPower).Amount, (ValueProp)4, (CardPlay)null, true);
		}
	}
}
