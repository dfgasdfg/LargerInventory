using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.IO;
using static LargerInventory.Flag;

namespace LargerInventory
{
    internal class FileLoader
    {
        internal static void Load(InventoryPlayer player, string path)
        {
            try
            {
                player.pages.Clear();
                path = Path.ChangeExtension(path, ".lis");
                using (FileStream fs = File.OpenRead(path))
                {
                    using (BinaryReader reader = new(fs))
                    {
                        bool currentVersion = reader.ReadString() == LargerInventory.Instance.Version.ToString();
                        int nowinv = reader.ReadInt32();
                        int nowarm = reader.ReadInt32();
                        int nowacc = reader.ReadInt32();
                        int nowmis = reader.ReadInt32();
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                player.favorite[i][j] = reader.ReadInt32();
                            }
                        }
                        player.CreatePage(reader.ReadInt32());
                        for (int i = 0; i < player.pages.Count; i++)
                        {
                            for (int j = 0; j < 59; j++)
                            {
                                ItemIO.Load(player.pages[i].Inventory[j], TagIO.Read(reader));
                            }
                            for (int j = 0; j < 10; j++)
                            {
                                player.pages[i].HideAccessory[j] = reader.ReadBoolean();
                            }
                            for (int j = 0; j < 20; j++)
                            {
                                ItemIO.Load(player.pages[i].Armor[j], TagIO.Read(reader));
                            }
                            for (int j = 0; j < 10; j++)
                            {
                                ItemIO.Load(player.pages[i].Dye[j], TagIO.Read(reader));
                            }
                            for (int j = 0; j < 5; j++)
                            {
                                ItemIO.Load(player.pages[i].MiscEquips[j], TagIO.Read(reader));
                            }
                            for (int j = 0; j < 5; j++)
                            {
                                ItemIO.Load(player.pages[i].MiscDyes[j], TagIO.Read(reader));
                            }
                            int b = reader.ReadInt32();
                            for (int j = 0; j < b; j++)
                            {
                                if (GameDictionary.Mods.TryGetValue(reader.ReadString(), out Mod mod))
                                {
                                    player.pages[i].BlackList.Add(mod);
                                }
                            }
                            int w = reader.ReadInt32();
                            for (int j = 0; j < w; j++)
                            {
                                if (GameDictionary.Mods.TryGetValue(reader.ReadString(), out Mod mod))
                                {
                                    player.pages[i].WhiteList.Add(mod);
                                }
                            }
                        }
                        player.NowInventoryIndex = nowinv;
                        player.NowArmorIndex = nowarm;
                        player.NowAccessoryIndex = nowacc;
                        player.NowMiscIndex = nowmis;
                        player.SendAll();
                    }
                }
                player.Loaded = true;
            }
            catch (Exception e)
            {
                CatchExptionForPlayer(player.player, ExceptionFlag.Loading, e);
                LargerInventory.Instance.Logger.Warn(e);
                try
                {
                    player.pages.Clear();
                    path = Path.ChangeExtension(path, ".lisb");
                    using (FileStream fs = File.OpenRead(path))
                    {
                        using (BinaryReader reader = new(fs))
                        {
                            bool currentVersion = reader.ReadString() == LargerInventory.Instance.Version.ToString();
                            int nowinv = reader.ReadInt32();
                            int nowarm = reader.ReadInt32();
                            int nowacc = reader.ReadInt32();
                            int nowmis = reader.ReadInt32();
                            for (int i = 0; i < 10; i++)
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    player.favorite[i][j] = reader.ReadInt32();
                                }
                            }
                            player.CreatePage(reader.ReadInt32());
                            for (int i = 0; i < player.pages.Count; i++)
                            {
                                for (int j = 0; j < 59; j++)
                                {
                                    ItemIO.Load(player.pages[i].Inventory[j], TagIO.Read(reader));
                                }
                                for (int j = 0; j < 10; j++)
                                {
                                    player.pages[i].HideAccessory[j] = reader.ReadBoolean();
                                }
                                for (int j = 0; j < 20; j++)
                                {
                                    ItemIO.Load(player.pages[i].Armor[j], TagIO.Read(reader));
                                }
                                for (int j = 0; j < 10; j++)
                                {
                                    ItemIO.Load(player.pages[i].Dye[j], TagIO.Read(reader));
                                }
                                for (int j = 0; j < 5; j++)
                                {
                                    ItemIO.Load(player.pages[i].MiscEquips[j], TagIO.Read(reader));
                                }
                                for (int j = 0; j < 5; j++)
                                {
                                    ItemIO.Load(player.pages[i].MiscDyes[j], TagIO.Read(reader));
                                }
                                int b = reader.ReadInt32();
                                for (int j = 0; j < b; j++)
                                {
                                    if (GameDictionary.Mods.TryGetValue(reader.ReadString(), out Mod mod))
                                    {
                                        player.pages[i].BlackList.Add(mod);
                                    }
                                }
                                int w = reader.ReadInt32();
                                for (int j = 0; j < w; j++)
                                {
                                    if (GameDictionary.Mods.TryGetValue(reader.ReadString(), out Mod mod))
                                    {
                                        player.pages[i].WhiteList.Add(mod);
                                    }
                                }
                            }
                            player.NowInventoryIndex = nowinv;
                            player.NowArmorIndex = nowarm;
                            player.NowAccessoryIndex = nowacc;
                            player.NowMiscIndex = nowmis;
                            player.SendAll();
                        }
                    }
                    player.Loaded = true;
                }
                catch (Exception ex)
                {
                    player.pages.Clear();
                    player.CreatePage(Config.DefaultNumberOfBackpacks);
                    player.SaveAll();
                    player.SendAll();
                    CatchExptionForPlayer(player.player, ExceptionFlag.Loading, ex);
                    LargerInventory.Instance.Logger.Warn(ex);
                }
            }
        }
        public static void LoadItem(ref Item item, BinaryReader reader)
        {
            int id = reader.ReadInt32();
            if (id == -114514)
            {
                if (ModContent.TryFind(reader.ReadString(), out ModItem modItem))
                {
                    item = new Item(modItem.Type);
                    if (reader.ReadBoolean()) item.ModItem.LoadData(TagIO.Read(reader));
                }
                else
                {
                    item = new Item(ModContent.ItemType<UnloadedItem>());
                }
            }
            else if (id != 0)
            {
                item = new Item(id);
            }
            else
            {
                item = new Item(0);
                return;
            }
            int prefix = reader.ReadInt32();
            if (prefix >= 85)
            {
                if (ModContent.TryFind(reader.ReadString(), out ModPrefix modPrefix))
                {
                    item.prefix = modPrefix.Type;
                }
            }
            item.stack = reader.ReadInt32();
            if (item.stack > item.maxStack)
            {
                item.stack = item.maxStack;
            }
            if (item.IsAir)
            {
                item.TurnToAir();
            }
        }
        internal static void OldLoad(BinaryReader reader,InventoryPlayer player)
        {
            int nowinv = reader.ReadInt32();
            int nowarm = reader.ReadInt32();
            int nowacc = reader.ReadInt32();
            int nowmis = reader.ReadInt32();
            player.CreatePage(reader.ReadInt32());
            for (int i = 0; i < player.pages.Count; i++)
            {
                for (int j = 0; j < 59; j++)
                {
                    ItemIO.Load(player.pages[i].Inventory[j], TagIO.Read(reader));
                }
                for (int j = 0; j < 10; j++)
                {
                    player.pages[i].HideAccessory[j] = reader.ReadBoolean();
                }
                for (int j = 0; j < 20; j++)
                {
                    ItemIO.Load(player.pages[i].Armor[j], TagIO.Read(reader));
                }
                for (int j = 0; j < 10; j++)
                {
                    ItemIO.Load(player.pages[i].Dye[j], TagIO.Read(reader));
                }
                for (int j = 0; j < 5; j++)
                {
                    ItemIO.Load(player.pages[i].MiscEquips[j], TagIO.Read(reader));
                }
                for (int j = 0; j < 5; j++)
                {
                    ItemIO.Load(player.pages[i].MiscDyes[j], TagIO.Read(reader));
                }
                int b = reader.ReadInt32();
                for (int j = 0; j < b; j++)
                {
                    if (GameDictionary.Mods.TryGetValue(reader.ReadString(), out Mod mod))
                    {
                        player.pages[i].BlackList.Add(mod);
                    }
                }
                int w = reader.ReadInt32();
                for (int j = 0; j < w; j++)
                {
                    if (GameDictionary.Mods.TryGetValue(reader.ReadString(), out Mod mod))
                    {
                        player.pages[i].WhiteList.Add(mod);
                    }
                }
            }
            player.NowInventoryIndex = nowinv;
            player.NowArmorIndex = nowarm;
            player.NowAccessoryIndex = nowacc;
            player.NowMiscIndex = nowmis;
            player.SendAll();
        }
    }
}