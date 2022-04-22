using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
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
    internal class GlobalNpcSet:GlobalNPC
    {
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            if (nextSlot < 40)
            {
                if(Main.rand.NextDouble() < 0.15)
                {
                    shop[nextSlot] = ModContent.ItemType<BackpackExpansionLicense>();
                }
                else if (Main.rand.NextDouble() < 0.3)
                {
                    shop[nextSlot] = ModContent.ItemType<BurdenReliefCertificate>();
                }
            }
            else
            {
                List<int> canaccpet = shop.ToList().FindAll(new Predicate<int>((t) => ItemLoader.GetItem(t) is null));
                if (canaccpet.Count > 0)
                {
                    int index = 0, find = Main.rand.Next(canaccpet);
                    for (int i = 0; i < shop.Length; i++)
                    {
                        if (shop[i] == find)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (Main.rand.NextDouble() < 0.15)
                    {
                        shop[index] = ModContent.ItemType<BackpackExpansionLicense>();
                    }
                    else if (Main.rand.NextDouble() < 0.3)
                    {
                        shop[index] = ModContent.ItemType<BurdenReliefCertificate>();
                    }
                }
            }
            base.SetupTravelShop(shop, ref nextSlot);
        }
    }
}
