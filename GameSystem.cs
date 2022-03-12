using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LargerInventory
{
    internal class GameSystem : ModSystem
    {
        public override void PostAddRecipes()
        {
            foreach (Mod mod in ModLoader.Mods)
            {
                if (!GameDictionary.Mods.ContainsKey(mod.DisplayName))
                {
                    GameDictionary.Mods.Add(mod.DisplayName, mod);
                }
                else
                {
                    GameDictionary.Mods[mod.DisplayName] = mod;
                }
            }
            base.PostAddRecipes();
        }
    }
}
