using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Models;
using TheFaceless.TheFacelessCode.Character;
using TheFaceless.TheFacelessCode.Extensions;
using StringExtensions = TheFaceless.TheFacelessCode.Extensions.StringExtensions;


namespace TheFaceless.TheFacelessCode.Potions;

[Pool(typeof(TheFacelessPotionPool))]
public abstract class TheFacelessPotion : CustomPotionModel
{
}

