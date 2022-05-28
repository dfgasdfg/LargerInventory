using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

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
        public override void UpdateUI(GameTime gameTime)
        {
            if (Config.EnableScrollPage)
            {
                var player = Manager.Activing;
                var Player = player.player;
                int next = -Math.Sign(PlayerInput.ScrollWheelDelta);
                float invScale = 0.85f;
                if (next != 0 && Player.mouseInterface)
                {
                    if (Main.mouseX > 20 && Main.mouseX < (int)(20f + 560f * invScale * Main.UIScale)
                        && Main.mouseY > 20 && Main.mouseY < (int)(20f + 280f * invScale * Main.UIScale))
                    {
                        player.SaveInventory();
                        player.NowInventoryIndex = Hooks.Wrap(0, player.pages.Count, player.NowInventoryIndex + next);
                        player.SendInventory();
                    }

                    int OnePrivateValue = (int)typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
                    int MagicalNumber = 174 + OnePrivateValue;
                    int EquipCount = Player.GetAmountOfExtraAccessorySlotsToShow() + 8;
                    if (Main.screenHeight < 950 && EquipCount >= 10)
                    {
                        MagicalNumber -= (int)(56f * invScale * (float)(EquipCount - 9));
                    }
                    float factor = 3f / EquipCount;
                    float boundary = (1 - factor) * MagicalNumber + factor * (int)((float)MagicalNumber + 448f * invScale);
                    if (Main.mouseX > Main.screenWidth - 64 - 28 && Main.mouseX < (int)((float)(Main.screenWidth - 64 - 28) + 56f * invScale))
                    {
                        if (Main.EquipPage == 0)
                        {
                            if (Main.mouseY > MagicalNumber && Main.mouseY < (int)((float)MagicalNumber + 448f * invScale))
                            {
                                if (Main.mouseY < boundary)
                                {
                                    player.SaveArmor();
                                    player.NowArmorIndex = Hooks.Wrap(0, player.pages.Count, player.NowArmorIndex + next);
                                    player.SendArmor();
                                }
                                else
                                {
                                    player.SaveAccessory();
                                    player.NowAccessoryIndex = Hooks.Wrap(0, player.pages.Count, player.NowAccessoryIndex + next);
                                    player.SendAccessory();
                                }
                            }
                        }
                        else if (Main.EquipPage == 2)
                        {
                            factor = 5f / EquipCount;
                            float bottom = (1 - factor) * MagicalNumber + factor * (int)((float)MagicalNumber + 448f * invScale);
                            if (Main.mouseY > MagicalNumber && Main.mouseY < bottom)
                            {
                                player.SaveMisc();
                                player.NowMiscIndex = Hooks.Wrap(0, player.pages.Count, player.NowMiscIndex + next);
                                player.SendMisc();
                            }
                        }
                    }
                }
            }

        }
    }
    internal class GlobalNpcSet : GlobalNPC
    {
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            if (nextSlot < 40)
            {
                if (Main.rand.NextDouble() < 0.15)
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
