using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheFaceless.TheFacelessCode.Relics;

namespace TheFaceless.TheFacelessCode.Powers;

public class Paranoia : TheFacelessPower
{
	public override PowerType Type => (PowerType)2;

	public override PowerStackType StackType => (PowerStackType)1;

	public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
	{
		Paranoia paranoia = this;
		
		if (!paranoia.Owner.Monster.IntendsToAttack && Applier.Player.Relics.OfType<MissingPoster>().Count() > 0)
		{
			await PowerCmd.Apply<StrengthPower>(choiceContext, paranoia.Owner, -paranoia.Amount + -paranoia.Amount, paranoia.Owner, null);
			await PowerCmd.Apply<Paranoia>(choiceContext, paranoia.Owner, -paranoia.Amount, paranoia.Owner, null);
		}
		 else if (!paranoia.Owner.Monster.IntendsToAttack && Applier.Player.Relics.OfType<MissingPoster>().Count() <= 0)
		{
			await PowerCmd.Apply<StrengthPower>(choiceContext, paranoia.Owner, -paranoia.Amount, paranoia.Owner, null);
			await PowerCmd.Apply<Paranoia>(choiceContext, paranoia.Owner, -paranoia.Amount, paranoia.Owner, null);
		}
		else
		{
			await PowerCmd.Apply<Paranoia>(choiceContext, paranoia.Owner, -paranoia.Amount, paranoia.Owner, null);
		}
	}
}
