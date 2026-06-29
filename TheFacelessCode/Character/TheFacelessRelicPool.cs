using BaseLib.Abstracts;
using Godot;
using TheFaceless.TheFacelessCode.Extensions;

namespace TheFaceless.TheFacelessCode.Character;

public class TheFacelessRelicPool : CustomRelicPoolModel
{
	public override Color LabOutlineColor => TheFaceless.Color;

	public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();

	public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}
