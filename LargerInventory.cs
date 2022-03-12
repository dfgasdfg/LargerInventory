using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Terraria.Localization;

namespace LargerInventory
{
    public class LargerInventory : Mod
    {
        internal static LargerInventory Instance;
        internal static Dictionary<string, Mod> mods = new();
        public LargerInventory()
        {
            Instance = this;
            Flag.PlayerException.Clear();
            Flag.Exceptions.Clear();
        }
        public override void Load()
        {
            Hooks.Load();
            KeyControl.Load();
            base.Load();
        }
        public override void Unload()
        {
            Hooks.Unload();
            KeyControl.Unload();
            base.Unload();
        }
    }
}