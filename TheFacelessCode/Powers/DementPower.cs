using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheFaceless.TheFacelessCode.Enchantments;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Powers;

public class DementPower : TheFacelessPower
{
	public override string CustomPackedIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Small.png").PowerImagePath();

	public override string CustomBigIconPath => (Id.Entry.RemovePrefix().ToLowerInvariant() + "_Big.png").BigPowerImagePath();
	
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		DementPower dementPower = this;
		if (!((cardPlay.Card.Owner == dementPower.Owner.Player) & !(cardPlay.Card.Enchantment is DejaVu)))
		{
			await CreatureCmd.GainBlock(dementPower.Owner, dementPower.Amount, (ValueProp)4, null, true);
		}
	}
}
