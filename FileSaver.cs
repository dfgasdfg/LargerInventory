using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LargerInventory
{
    internal class FileSaver
    {
        internal static void Save(InventoryPlayer player)
        {
            if (Flag.SavingFile is null)
            {
                Flag.CatchException(Flag.ExceptionFlag.NullReferenceException, new NullReferenceException("存档抓捕脱靶"));
                return;
            }
            BackupIO.Player.ArchivePlayer(Flag.SavingFile.Path);
            string path = Path.ChangeExtension(Flag.SavingFile.Path, ".lis");
            using (FileStream fs = new(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (BinaryWriter writer = new(fs))
                {
                    player.SaveAll();
                    writer.Write(LargerInventory.Instance.Version.ToString());
                    writer.Write(player.NowInventoryIndex);
                    writer.Write(player.NowArmorIndex);
                    writer.Write(player.NowAccessoryIndex);
                    writer.Write(player.NowMiscIndex);
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            writer.Write(player.favorite[i][j]);
                        }
                    }
                    writer.Write(player.pages.Count);
                    for (int i = 0; i < player.pages.Count; i++)
                    {
                        for (int j = 0; j < 59; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].Inventory[j]), writer);
                        }
                        for (int j = 0; j < 10; j++)
                        {
                            writer.Write(player.pages[i].HideAccessory[j]);
                        }
                        for (int j = 0; j < 20; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].Armor[j]), writer);
                        }
                        for (int j = 0; j < 10; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].Dye[j]), writer);
                        }
                        for (int j = 0; j < 5; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].MiscEquips[j]), writer);
                        }
                        for (int j = 0; j < 5; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].MiscDyes[j]), writer);
                        }
                        writer.Write(player.pages[i].BlackList.Count);
                        for (int j = 0; j < player.pages[i].BlackList.Count; j++)
                        {
                            writer.Write(player.pages[i].BlackList[j].DisplayName);
                        }
                        writer.Write(player.pages[i].WhiteList.Count);
                        for (int j = 0; j < player.pages[i].WhiteList.Count; j++)
                        {
                            writer.Write(player.pages[i].WhiteList[j].DisplayName);
                        }
                    }
                }
            }
            path = Path.ChangeExtension(Flag.SavingFile.Path, ".lisb");
            using (FileStream fs = new(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (BinaryWriter writer = new(fs))
                {
                    player.SaveAll();
                    writer.Write(LargerInventory.Instance.Version.ToString());
                    writer.Write(player.NowInventoryIndex);
                    writer.Write(player.NowArmorIndex);
                    writer.Write(player.NowAccessoryIndex);
                    writer.Write(player.NowMiscIndex);
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            writer.Write(player.favorite[i][j]);
                        }
                    }
                    writer.Write(player.pages.Count);
                    for (int i = 0; i < player.pages.Count; i++)
                    {
                        for (int j = 0; j < 59; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].Inventory[j]), writer);
                        }
                        for (int j = 0; j < 10; j++)
                        {
                            writer.Write(player.pages[i].HideAccessory[j]);
                        }
                        for (int j = 0; j < 20; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].Armor[j]), writer);
                        }
                        for (int j = 0; j < 10; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].Dye[j]), writer);
                        }
                        for (int j = 0; j < 5; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].MiscEquips[j]), writer);
                        }
                        for (int j = 0; j < 5; j++)
                        {
                            TagIO.Write(ItemIO.Save(player.pages[i].MiscDyes[j]), writer);
                        }
                        writer.Write(player.pages[i].BlackList.Count);
                        for (int j = 0; j < player.pages[i].BlackList.Count; j++)
                        {
                            writer.Write(player.pages[i].BlackList[j].DisplayName);
                        }
                        writer.Write(player.pages[i].WhiteList.Count);
                        for (int j = 0; j < player.pages[i].WhiteList.Count; j++)
                        {
                            writer.Write(player.pages[i].WhiteList[j].DisplayName);
                        }
                    }
                }
            }
            Flag.SavingFile = null;
        }
        //private static void SaveItem(Item item, BinaryWriter writer, TagCompound tag)
        //{
        //    if (item is null)
        //    {
        //        item = new Item(0);
        //    }
        //    if(item.type != 0)
        //    {
        //        ;
        //    }
        //    if (item.ModItem is null)
        //    {
        //        writer.Write(true);
        //        writer.Write(item.type);
        //    }
        //    else
        //    {
        //        writer.Write(false);
        //        writer.Write(ItemLoader.GetItem(item.type).GetType().FullName);
        //    }
        //    if (PrefixLoader.GetPrefix(item.prefix) is null)
        //    {
        //        writer.Write(true);
        //        writer.Write(item.prefix);
        //    }
        //    else
        //    {
        //        writer.Write(false);
        //        writer.Write(PrefixLoader.GetPrefix(item.prefix).GetType().FullName);
        //    }
        //    writer.Write(item.stack);
        //    writer.Write(item.favorited);
        //    item.ModItem?.SaveData(tag);
        //}
        public static void SaveItem(Terraria.Item item, BinaryWriter writer)
        {
            if (item is null || item.netID == 0)
            {
                writer.Write(0);
                return;
            }
            if (item.ModItem is null)
            {
                writer.Write(item.netID);
            }
            else
            {
                writer.Write(-114514);
                writer.Write(item.ModItem.FullName);
                TagCompound tag = new();
                item.ModItem.SaveData(tag);
                if (tag.Count != 0)
                {
                    writer.Write(true);
                    TagIO.Write(tag, writer);
                }
                else writer.Write(false);
            }
            writer.Write(item.prefix);
            if (item.prefix >= 85)
            {
                ModPrefix modPrefix = PrefixLoader.GetPrefix(item.prefix);
                writer.Write(modPrefix.FullName);
            }
            writer.Write(item.stack);
        }
    }
}
