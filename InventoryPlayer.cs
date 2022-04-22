using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace LargerInventory
{
    internal class InventoryPlayer
    {
        internal InventoryPlayer(Player p)
        {
            player = p;
            for (int i = 0; i < 10; i++)
            {
                favorite[i] = new int[4] { -1, -1, -1, -1 };
            }
            CreatePage(Config.DefaultNumberOfBackpacks);
        }
        internal bool Loaded = false;
        internal readonly Player player;
        internal readonly List<Page> pages = new();
        private int inventoryindex = 0;
        internal int[][] favorite = new int[10][];
        internal int NowInventoryIndex
        {
            get
            {
                return inventoryindex;
            }
            set
            {
                inventoryindex = Math.Clamp(value, 0, pages.Count - 1);
            }
        }
        internal Item[] NowInventory => pages[NowInventoryIndex].Inventory[0..58];
        private int armorindex = 0;
        internal int NowArmorIndex
        {
            get
            {
                return armorindex;
            }
            set
            {
                armorindex = Math.Clamp(value, 0, pages.Count - 1);
            }
        }
        private int accessoryindex = 0;
        internal int NowAccessoryIndex
        {
            get
            {
                return accessoryindex;
            }
            set
            {
                accessoryindex = Math.Clamp(value, 0, pages.Count - 1);
            }
        }
        private int miscindex = 0;
        internal int NowMiscIndex
        {
            get
            {
                return miscindex;
            }
            set
            {
                miscindex = Math.Clamp(value, 0, pages.Count - 1);
            }
        }
        internal void SendInventory()
        {
            pages[NowInventoryIndex].Inventory[0..58].CopyTo(player.inventory, 0);
        }
        internal void SaveInventory()
        {
            player.inventory[0..58].CopyTo(pages[NowInventoryIndex].Inventory, 0);
        }
        internal void SendArmor()
        {
            pages[NowArmorIndex].Armor[0..3].CopyTo(player.armor, 0);
            pages[NowArmorIndex].Armor[10..13].CopyTo(player.armor, 10);
            pages[NowArmorIndex].Dye[0..3].CopyTo(player.dye, 0);
        }
        internal void SaveArmor()
        {
            player.armor[0..3].CopyTo(pages[NowArmorIndex].Armor, 0);
            player.armor[10..13].CopyTo(pages[NowArmorIndex].Armor, 10);
            player.dye[0..3].CopyTo(pages[NowArmorIndex].Dye, 0);
        }
        internal void SendAccessory()
        {
            pages[NowAccessoryIndex].Armor[3..10].CopyTo(player.armor, 3);
            pages[NowAccessoryIndex].Armor[13..20].CopyTo(player.armor, 13);
            pages[NowAccessoryIndex].Dye[3..10].CopyTo(player.dye, 3);
        }
        internal void SaveAccessory()
        {
            player.armor[3..10].CopyTo(pages[NowAccessoryIndex].Armor, 3);
            player.armor[13..20].CopyTo(pages[NowAccessoryIndex].Armor, 13);
            player.dye[3..10].CopyTo(pages[NowAccessoryIndex].Dye, 3);
        }
        internal void SendMisc()
        {
            pages[NowMiscIndex].MiscEquips.CopyTo(player.miscEquips, 0);
            pages[NowMiscIndex].MiscDyes.CopyTo(player.miscDyes, 0);
        }
        internal void SaveMisc()
        {
            player.miscEquips.CopyTo(pages[NowMiscIndex].MiscEquips, 0);
            player.miscDyes.CopyTo(pages[NowMiscIndex].MiscDyes, 0);
        }
        internal void SendAll()
        {
            SendInventory();
            SendArmor();
            SendAccessory();
            SendMisc();
        }
        internal void SaveAll()
        {
            SaveInventory();
            SaveArmor();
            SaveAccessory();
            SaveMisc();
        }

        internal Page CreatePage(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Page p = new(pages.Count);
                pages.Add(p);
            }
            return pages[^1];
        }
        internal Item[] GetItemForFindRecipes()
        {
            List<Item> list = new();
            SaveInventory();
            foreach (Page p in pages)
            {
                foreach (Item item in p.Inventory)
                {
                    if (Config.IgnoreFavouritesWhenCraftingItems)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        if (!item.favorited)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list.ToArray();
        }
        internal Item[] GetItemForQuickUse()
        {
            List<Item> list = new();
            SaveInventory();
            foreach (Page p in pages)
            {
                foreach (Item item in p.Inventory)
                {
                    if (Config.IgnoreFavouritesWhenQuickUseItems)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        if (!item.favorited)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list.ToArray();
        }
        internal Item[] GetItemForBuySell()
        {
            List<Item> result = new();
            foreach (Page p in pages)
            {
                result.AddRange(p.Inventory[0..54]);
            }
            return result.ToArray();
        }
    }
}
