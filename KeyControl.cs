using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LargerInventory
{
    internal class KeyControl
    {
        internal static Dictionary<string, ModKeybind> keybinds = new();
        internal static string PageInventory = "PageInventory";
        internal static string PageArmor = "PageArmor";
        internal static string PageAccessory = "PageAccessory";
        internal static string PageMisc = "PageMisc";
        internal static string ReverseDirection = "ReverseDirection";
        internal static string Favorite = "Favorite";
        internal static string SortAll = "SortAll";
        internal static string[] Favorites = new string[10]
        {
            "Favorite0",
            "Favorite1",
            "Favorite2",
            "Favorite3",
            "Favorite4",
            "Favorite5",
            "Favorite6",
            "Favorite7",
            "Favorite8",
            "Favorite9"
        };
        internal static int PageDirection = 1;
        internal static void Load()
        {
            Unload();
            Mod mod = LargerInventory.Instance;
            keybinds.Add(ReverseDirection, KeybindLoader.RegisterKeybind(mod, ReverseDirection, Microsoft.Xna.Framework.Input.Keys.Y));
            keybinds.Add(PageInventory, KeybindLoader.RegisterKeybind(mod, PageInventory, Microsoft.Xna.Framework.Input.Keys.U));
            keybinds.Add(PageArmor, KeybindLoader.RegisterKeybind(mod, PageArmor, Microsoft.Xna.Framework.Input.Keys.I));
            keybinds.Add(PageAccessory, KeybindLoader.RegisterKeybind(mod, PageAccessory, Microsoft.Xna.Framework.Input.Keys.O));
            keybinds.Add(PageMisc, KeybindLoader.RegisterKeybind(mod, PageMisc, Microsoft.Xna.Framework.Input.Keys.P));
            keybinds.Add(Favorite, KeybindLoader.RegisterKeybind(mod, Favorite, Microsoft.Xna.Framework.Input.Keys.LeftControl));
            keybinds.Add(SortAll, KeybindLoader.RegisterKeybind(mod, SortAll, Microsoft.Xna.Framework.Input.Keys.RightControl));
            keybinds.Add(Favorites[0], KeybindLoader.RegisterKeybind(mod, Favorites[0], Microsoft.Xna.Framework.Input.Keys.NumPad0));
            keybinds.Add(Favorites[1], KeybindLoader.RegisterKeybind(mod, Favorites[1], Microsoft.Xna.Framework.Input.Keys.NumPad1));
            keybinds.Add(Favorites[2], KeybindLoader.RegisterKeybind(mod, Favorites[2], Microsoft.Xna.Framework.Input.Keys.NumPad2));
            keybinds.Add(Favorites[3], KeybindLoader.RegisterKeybind(mod, Favorites[3], Microsoft.Xna.Framework.Input.Keys.NumPad3));
            keybinds.Add(Favorites[4], KeybindLoader.RegisterKeybind(mod, Favorites[4], Microsoft.Xna.Framework.Input.Keys.NumPad4));
            keybinds.Add(Favorites[5], KeybindLoader.RegisterKeybind(mod, Favorites[5], Microsoft.Xna.Framework.Input.Keys.NumPad5));
            keybinds.Add(Favorites[6], KeybindLoader.RegisterKeybind(mod, Favorites[6], Microsoft.Xna.Framework.Input.Keys.NumPad6));
            keybinds.Add(Favorites[7], KeybindLoader.RegisterKeybind(mod, Favorites[7], Microsoft.Xna.Framework.Input.Keys.NumPad7));
            keybinds.Add(Favorites[8], KeybindLoader.RegisterKeybind(mod, Favorites[8], Microsoft.Xna.Framework.Input.Keys.NumPad8));
            keybinds.Add(Favorites[9], KeybindLoader.RegisterKeybind(mod, Favorites[9], Microsoft.Xna.Framework.Input.Keys.NumPad9));
            
        }
        internal static void Unload()
        {
            keybinds.Clear();
        }
    }
}