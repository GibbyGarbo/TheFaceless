using BaseLib.Abstracts;
using Godot;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Character;

public class TheFacelessCardPool : CustomCardPoolModel
{
	public override string Title => "TheFaceless";

	public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();

	public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();

	public override float H => 0f;

	public override float S => 0f;

	public override float V => 0.3f;

	public override Color DeckEntryCardColor => new Color("363636");

	public override bool IsColorless => false;
}
