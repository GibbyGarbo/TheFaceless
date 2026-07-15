using System.IO;
using Godot;

namespace TheFaceless.TheFacelessCode.Extensions;

public static class StringExtensions
{
	public static string ImagePath(this string path)
	{
		return Path.Join("res://TheFaceless", "images", path);
	}

	public static string CardImagePath(this string path)
	{
		path = Path.Join("res://TheFaceless", "images", "card_portraits", path);
		if (ResourceLoader.Exists(path, ""))
		{
			return path;
		}
		MainFile.Logger.Info("Could not find card image path: " + path, 1);
		return Path.Join("res://TheFaceless", "images", "card_portraits", "card.png");
	}

	public static string BigCardImagePath(this string path)
	{
		path = Path.Join(new string[5] { "res://TheFaceless", "images", "card_portraits", "big", path });
		if (ResourceLoader.Exists(path, ""))
		{
			return path;
		}
		MainFile.Logger.Info("Could not find big card image path: " + path, 1);
		return Path.Join(new string[5] { "res://TheFaceless", "images", "card_portraits", "big", "card.png" });
	}

	public static string PowerImagePath(this string path)
	{
		path = Path.Join("res://TheFaceless", "images", "powers", path);
		if (ResourceLoader.Exists(path, ""))
		{
			return path;
		}
		MainFile.Logger.Info("Could not find power image path: " + path, 1);
		return Path.Join("res://TheFaceless", "images", "powers", "power.png");
	}

	public static string BigPowerImagePath(this string path)
	{
		path = Path.Join(new string[5] { "res://TheFaceless", "images", "powers", "big", path });
		if (ResourceLoader.Exists(path, ""))
		{
			return path;
		}
		MainFile.Logger.Info("Could not find big power image path: " + path, 1);
		return Path.Join(new string[5] { "res://TheFaceless", "images", "powers", "big", "power.png" });
	}

	public static string RelicImagePath(this string path)
	{
		path = Path.Join("res://TheFaceless", "images", "relics", path);
		if (ResourceLoader.Exists(path, ""))
		{
			return path;
		}
		MainFile.Logger.Info("Could not find relic image path: " + path, 1);
		return Path.Join("res://TheFaceless", "images", "relics", "relic.png");
	}

	public static string BigRelicImagePath(this string path)
	{
		path = Path.Join(new string[5] { "res://TheFaceless", "images", "relics", "big", path });
		if (ResourceLoader.Exists(path, ""))
		{
			return path;
		}
		MainFile.Logger.Info("Could not find big relic image path: " + path, 1);
		return Path.Join(new string[5] { "res://TheFaceless", "images", "relics", "big", "relic.png" });
	}

	public static string CharacterUiPath(this string path)
	{
		return Path.Join("res://TheFaceless", "images", "charui", path);
	}
	public static string PotionImagePath(this string path)
	{
		path = Path.Join(MainFile.ResPath, "images", "potions", path);
		if (ResourceLoader.Exists(path))
		{
			return path;
		}

		MainFile.Logger.Info("Could not find potion image path: " + path);
		return Path.Join(MainFile.ResPath, "images", "potions", "potion.png");
	}
}
