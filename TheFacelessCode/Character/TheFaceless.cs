using System.Collections.Generic;
using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using TheFaceless.TheFacelessCode.Cards;
using TheFaceless.TheFacelessCode.Extensions;
using TheFaceless.TheFacelessCode.Relics;

namespace TheFaceless.TheFacelessCode.Character;

public class TheFaceless : PlaceholderCharacterModel
{
	public const string CharacterId = "TheFaceless";

	public static readonly Color Color = new Color("404040");

	public override Color NameColor => Color;

	public override CharacterGender Gender => (CharacterGender)2;

	public override int StartingHp => 68;

	public override IEnumerable<CardModel> StartingDeck =>
	[
		ModelDb.Card<StrikeFaceless>(),
		ModelDb.Card<StrikeFaceless>(),
		ModelDb.Card<StrikeFaceless>(),
		ModelDb.Card<StrikeFaceless>(),
		ModelDb.Card<DefendFaceless>(),
		ModelDb.Card<DefendFaceless>(),
		ModelDb.Card<DefendFaceless>(),
		ModelDb.Card<DefendFaceless>(),
		ModelDb.Card<CantRun>(),
		ModelDb.Card<AlwaysWatching>()
	];

	public override IReadOnlyList<RelicModel> StartingRelics =>

	[
		ModelDb.Relic<TheSource>()
	];

	public override CardPoolModel CardPool => ModelDb.CardPool<TheFacelessCardPool>();

	public override RelicPoolModel RelicPool => ModelDb.RelicPool<TheFacelessRelicPool>();

	public override PotionPoolModel PotionPool => ModelDb.PotionPool<TheFacelessPotionPool>();

	public override Control CustomIcon
	{
		get
		{
			var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
			icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
			return icon;
		}
	}

	public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();

	public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();

	public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();

	public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();

	public override NCreatureVisuals CreateCustomVisuals()
	{
		return NodeFactory<NCreatureVisuals>.CreateFromScene("res://TheFacelessCode/control.tscn");
	}

	public override string CustomMerchantAnimPath => "res://TheFacelessCode/Merchent.tscn";

	public override string CustomCharacterSelectBg => "res://TheFaceless/images/CharSelect/CharSelectBackground.tscn";

	public override string CustomEnergyCounterPath => "res://TheFaceless/images/EnergyCounter/energy_control.tscn";

	public override Color EnergyLabelOutlineColor => new Color("404040");
}
