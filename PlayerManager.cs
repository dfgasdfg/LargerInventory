using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LargerInventory
{
    internal class PlayerManager : ModPlayer
    {
        internal bool LoadDataSuccess = false;
        internal InventoryPlayer inventoryPlayer;
        internal string LoadFrom = string.Empty;
        #region
        internal static int Mushroom = 0;
        internal static int Wooden = 0;
        internal static int MetalTier1 = 0;
        internal static int MetalTier2 = 0;
        internal static int Platinum = 0;
        internal static int Pumpkin = 0;
        internal static int Gladiator = 0;
        internal static int Ninja = 0;
        internal static int Fossil = 0;
        internal static int Bone = 0;
        internal static int BeetleAttack = 0;
        internal static int BeetleDefense = 0;
        internal static int Wizard = 0;
        internal static int MagicHat = 0;
        internal static int ShadowScale = 0;
        internal static int Crimson = 0;
        internal static int GhostHeal = 0;
        internal static int GhostHurt = 0;
        internal static int Meteor = 0;
        internal static int Frost = 0;
        internal static int Jungle = 0;
        internal static int Molten = 0;
        internal static int Mining = 0;
        internal static int Chlorophyte = 0;
        internal static int ChlorophyteMelee = 0;
        internal static int Cactus = 0;
        internal static int Turtle = 0;
        internal static int CobaltCaster = 0;
        internal static int CobaltMelee = 0;
        internal static int CobaltRanged = 0;
        internal static int MythrilCaster = 0;
        internal static int MythrilMelee = 0;
        internal static int MythrilRanged = 0;
        internal static int AdamantiteCaster = 0;
        internal static int AdamantiteMelee = 0;
        internal static int AdamantiteRanged = 0;
        internal static int Palladium = 0;
        internal static int Orichalcum = 0;
        internal static int Titanium = 0;
        internal static int Hallowed = 0;
        internal static int HallowedSummoner = 0;
        internal static int CrystalNinja = 0;
        internal static int Tiki = 0;
        internal static int Spooky = 0;
        internal static int Bee = 0;
        internal static int Spider = 0;
        internal static int Solar = 0;
        internal static int Vortex = 0;
        internal static int Nebula = 0;
        internal static int StarDust = 0;
        internal static int Forbidden = 0;
        internal static int SquireTier1 = 0;
        internal static int SquireTier2 = 0;
        internal static int ApprenticeTier1 = 0;
        internal static int ApprenticeTier2 = 0;
        internal static int HuntressTier1 = 0;
        internal static int HuntressTier2 = 0;
        internal static int MonkTier1 = 0;
        internal static int MonkTier2 = 0;
        internal static int ObsidianOutlaw = 0;
        #endregion
        public override void OnEnterWorld(Player player)
        {
            Manager.Activing = inventoryPlayer is null ? Manager.Create(player) : inventoryPlayer;
            base.OnEnterWorld(player);
        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            try
            {
                Item item = new(ModContent.ItemType<ModIntroduce>());
                return new Item[] { item };
            }
            catch
            {

            }
            return base.AddStartingItems(mediumCoreDeath);
        }
        public override void LoadData(TagCompound tag)
        {
            inventoryPlayer = Manager.Create(Player);
            LoadFrom = Flag.LoadingPath;
            FileLoader.Load(inventoryPlayer, Flag.LoadingPath);
            Flag.LoadingPath = string.Empty;
            base.LoadData(tag);
        }
        public override void SaveData(TagCompound tag)
        {
            FileSaver.Save(inventoryPlayer is null ? Manager.Create(Player) : inventoryPlayer);
            tag.Add("ForDoLoad", "ForDoLoad");
            base.SaveData(tag);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (!Flag.ChangeAvailable)
            {
                return;
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.ReverseDirection, out ModKeybind reverse))
            {
                if (reverse.Current)
                {
                    KeyControl.PageDirection = -1;
                }
                else
                {
                    KeyControl.PageDirection = 1;
                }
            }
            else
            {
                KeyControl.PageDirection = 1;
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.PageInventory, out ModKeybind pi))
            {
                if (pi.JustPressed)
                {
                    Manager.Activing.SaveInventory();
                    int index = Manager.Activing.NowInventoryIndex + KeyControl.PageDirection;
                    if (index < 0)
                    {
                        index = Manager.Activing.pages.Count - 1;
                    }
                    else if (index >= Manager.Activing.pages.Count)
                    {
                        index = 0;
                    }
                    Manager.Activing.NowInventoryIndex = index;
                    Manager.Activing.SendInventory();
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.PageArmor, out ModKeybind pa1))
            {
                if (pa1.JustPressed)
                {
                    Manager.Activing.SaveArmor();
                    int index = Manager.Activing.NowArmorIndex + KeyControl.PageDirection;
                    if (index < 0)
                    {
                        index = Manager.Activing.pages.Count - 1;
                    }
                    else if (index >= Manager.Activing.pages.Count)
                    {
                        index = 0;
                    }
                    Manager.Activing.NowArmorIndex = index;
                    Manager.Activing.SendArmor();
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.PageAccessory, out ModKeybind pa2))
            {
                if (pa2.JustPressed)
                {
                    Manager.Activing.SaveAccessory();
                    int index = Manager.Activing.NowAccessoryIndex + KeyControl.PageDirection;
                    if (index < 0)
                    {
                        index = Manager.Activing.pages.Count - 1;
                    }
                    else if (index >= Manager.Activing.pages.Count)
                    {
                        index = 0;
                    }
                    Manager.Activing.NowAccessoryIndex = index;
                    Manager.Activing.SendAccessory();
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.PageMisc, out ModKeybind pm))
            {
                if (pm.JustPressed)
                {
                    Manager.Activing.SaveMisc();
                    int index = Manager.Activing.NowMiscIndex + KeyControl.PageDirection;
                    if (index < 0)
                    {
                        index = Manager.Activing.pages.Count - 1;
                    }
                    else if (index >= Manager.Activing.pages.Count)
                    {
                        index = 0;
                    }
                    Manager.Activing.NowMiscIndex = index;
                    Manager.Activing.SendMisc();
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.SortAll, out ModKeybind sortall))
            {
                if (sortall.JustPressed)
                {
                    Page.QuickStackAll();
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorite, out ModKeybind alt))
            {
                if (alt.Current)
                {
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[0], out ModKeybind favorite0))
                    {
                        if (favorite0.JustPressed)
                        {
                            SaveFavorite(0);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[1], out ModKeybind favorite1))
                    {
                        if (favorite1.JustPressed)
                        {
                            SaveFavorite(1);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[2], out ModKeybind favorite2))
                    {
                        if (favorite2.JustPressed)
                        {
                            SaveFavorite(2);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[3], out ModKeybind favorite3))
                    {
                        if (favorite3.JustPressed)
                        {
                            SaveFavorite(3);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[4], out ModKeybind favorite4))
                    {
                        if (favorite4.JustPressed)
                        {
                            SaveFavorite(4);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[5], out ModKeybind favorite5))
                    {
                        if (favorite5.JustPressed)
                        {
                            SaveFavorite(5);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[6], out ModKeybind favorite6))
                    {
                        if (favorite6.JustPressed)
                        {
                            SaveFavorite(6);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[7], out ModKeybind favorite7))
                    {
                        if (favorite7.JustPressed)
                        {
                            SaveFavorite(7);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[8], out ModKeybind favorite8))
                    {
                        if (favorite8.JustPressed)
                        {
                            SaveFavorite(8);
                            return;
                        }
                    }
                    if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[9], out ModKeybind favorite9))
                    {
                        if (favorite9.JustPressed)
                        {
                            SaveFavorite(9);
                            return;
                        }
                    }
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[0], out ModKeybind f0))
            {
                if (f0.JustPressed)
                {
                    SetFavorite(0);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[1], out ModKeybind f1))
            {
                if (f1.JustPressed)
                {
                    SetFavorite(1);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[2], out ModKeybind f2))
            {
                if (f2.JustPressed)
                {
                    SetFavorite(2);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[3], out ModKeybind f3))
            {
                if (f3.JustPressed)
                {
                    SetFavorite(3);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[4], out ModKeybind f4))
            {
                if (f4.JustPressed)
                {
                    SetFavorite(4);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[5], out ModKeybind f5))
            {
                if (f5.JustPressed)
                {
                    SetFavorite(5);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[6], out ModKeybind f6))
            {
                if (f6.JustPressed)
                {
                    SetFavorite(6);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[7], out ModKeybind f7))
            {
                if (f7.JustPressed)
                {
                    SetFavorite(7);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[8], out ModKeybind f8))
            {
                if (f8.JustPressed)
                {
                    SetFavorite(8);
                    return;
                }
            }
            if (KeyControl.keybinds.TryGetValue(KeyControl.Favorites[9], out ModKeybind f9))
            {
                if (f9.JustPressed)
                {
                    SetFavorite(9);
                    return;
                }
            }

        }
        internal static void SaveFavorite(int index)
        {
            Manager.Activing.favorite[index][0] = Manager.Activing.NowInventoryIndex;
            Manager.Activing.favorite[index][1] = Manager.Activing.NowArmorIndex;
            Manager.Activing.favorite[index][2] = Manager.Activing.NowAccessoryIndex;
            Manager.Activing.favorite[index][3] = Manager.Activing.NowMiscIndex;
            Main.NewText(Language.GetTextValue("Mods.LargerInventory.Common.SaveFavorite") + $"[{index}]");
        }
        internal static void SetFavorite(int index)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Manager.Activing.favorite[index][i] == -1)
                {
                    return;
                }
            }
            Manager.Activing.SaveAll();
            Manager.Activing.NowInventoryIndex = Manager.Activing.favorite[index][0];
            Manager.Activing.NowArmorIndex = Manager.Activing.favorite[index][1];
            Manager.Activing.NowAccessoryIndex = Manager.Activing.favorite[index][2];
            Manager.Activing.NowMiscIndex = Manager.Activing.favorite[index][3];
            Manager.Activing.SendAll();
            Main.NewText(Language.GetTextValue("Mods.LargerInventory.Common.SetFavorite") + $"[{index}]");
        }
        internal static void ResetArmorCounter()
        {
            Mushroom = 0;
            Wooden = 0;
            MetalTier1 = 0;
            MetalTier2 = 0;
            Platinum = 0;
            Pumpkin = 0;
            Gladiator = 0;
            Ninja = 0;
            Fossil = 0;
            Bone = 0;
            BeetleAttack = 0;
            BeetleDefense = 0;
            Wizard = 0;
            MagicHat = 0;
            ShadowScale = 0;
            Crimson = 0;
            GhostHeal = 0;
            GhostHurt = 0;
            Meteor = 0;
            Frost = 0;
            Jungle = 0;
            Molten = 0;
            Mining = 0;
            Chlorophyte = 0;
            ChlorophyteMelee = 0;
            Cactus = 0;
            Turtle = 0;
            CobaltCaster = 0;
            CobaltMelee = 0;
            CobaltRanged = 0;
            MythrilCaster = 0;
            MythrilMelee = 0;
            MythrilRanged = 0;
            AdamantiteCaster = 0;
            AdamantiteMelee = 0;
            AdamantiteRanged = 0;
            Palladium = 0;
            Orichalcum = 0;
            Titanium = 0;
            Hallowed = 0;
            HallowedSummoner = 0;
            CrystalNinja = 0;
            Tiki = 0;
            Spooky = 0;
            Bee = 0;
            Spider = 0;
            Solar = 0;
            Vortex = 0;
            Nebula = 0;
            StarDust = 0;
            Forbidden = 0;
            SquireTier1 = 0;
            SquireTier2 = 0;
            ApprenticeTier1 = 0;
            ApprenticeTier2 = 0;
            HuntressTier1 = 0;
            HuntressTier2 = 0;
            MonkTier1 = 0;
            MonkTier2 = 0;
            ObsidianOutlaw = 0;
        }
        internal static void RestatisticsArmorCounter()
        {
            foreach (Page page in Manager.Activing.pages)
            {
                if (page.MushroomSet)
                {
                    Mushroom++;
                    continue;
                }
                if (page.WoodenSET)
                {
                    Wooden++;
                    continue;
                }
                if (page.MetalTier1Set)
                {
                    MetalTier1++;
                    continue;
                }
                if (page.MetalTier2Set)
                {
                    MetalTier2++;
                    continue;
                }
                if (page.PlatinumSet)
                {
                    Platinum++;
                    continue;
                }
                if (page.PumpkinSet)
                {
                    Pumpkin++;
                    continue;
                }
                if (page.GladiatorSet)
                {
                    Gladiator++;
                    continue;
                }
                if (page.NinjaSet)
                {
                    Ninja++;
                    continue;
                }
                if (page.FossilSet)
                {
                    Fossil++;
                    continue;
                }
                if (page.BoneSet)
                {
                    Bone++;
                    continue;
                }
                if (page.BeetleAttackSet)
                {
                    BeetleAttack++;
                    continue;
                }
                if (page.BeetleDefenseSet)
                {
                    BeetleDefense++;
                    continue;
                }
                if (page.WizardSet)
                {
                    Wizard++;
                    continue;
                }
                if (page.MagicHatSet)
                {
                    MagicHat++;
                    continue;
                }
                if (page.ShadowScaleSet)
                {
                    ShadowScale++;
                    continue;
                }
                if (page.CrimsonSet)
                {
                    Crimson++;
                    continue;
                }
                if (page.GhostHealSet)
                {
                    GhostHeal++;
                    continue;
                }
                if (page.GhostHurtSet)
                {
                    GhostHurt++;
                    continue;
                }
                if (page.MeteorSet)
                {
                    Meteor++;
                    continue;
                }
                if (page.FrostSet)
                {
                    Frost++;
                    continue;
                }
                if (page.JungleSet)
                {
                    Jungle++;
                    continue;
                }
                if (page.MoltenSet)
                {
                    Molten++;
                    continue;
                }
                if (page.MiningSet)
                {
                    Mining++;
                    continue;
                }
                if (page.ChlorophyteSet)
                {
                    Chlorophyte++;
                    continue;
                }
                if (page.ChlorophyteMeleeSet)
                {
                    ChlorophyteMelee++;
                    continue;
                }
                if (page.CactusSet)
                {
                    Cactus++;
                    continue;
                }
                if (page.TurtleSet)
                {
                    Turtle++;
                    continue;
                }
                if (page.CobaltCasterSet)
                {
                    CobaltCaster++;
                    continue;
                }
                if (page.CobaltMeleeSet)
                {
                    CobaltMelee++;
                    continue;
                }
                if (page.CobaltRangedSet)
                {
                    CobaltRanged++;
                    continue;
                }
                if (page.MythrilCasterSet)
                {
                    MythrilCaster++;
                    continue;
                }
                if (page.MythrilMeleeSet)
                {
                    MythrilMelee++;
                    continue;
                }
                if (page.MythrilRangedSet)
                {
                    MythrilRanged++;
                    continue;
                }
                if (page.AdamantiteCasterSet)
                {
                    AdamantiteCaster++;
                    continue;
                }
                if (page.AdamantiteMeleeSet)
                {
                    AdamantiteMelee++;
                    continue;
                }
                if (page.AdamantiteRangedSet)
                {
                    AdamantiteRanged++;
                    continue;
                }
                if (page.PalladiumSet)
                {
                    Palladium++;
                    continue;
                }
                if (page.OrichalcumSet)
                {
                    Orichalcum++;
                    continue;
                }
                if (page.TitaniumSet)
                {
                    Titanium++;
                    continue;
                }
                if (page.HallowedSet)
                {
                    Hallowed++;
                    continue;
                }
                if (page.HallowedSummonerSet)
                {
                    HallowedSummoner++;
                    continue;
                }
                if (page.CrystalNinjaSet)
                {
                    CrystalNinja++;
                    continue;
                }
                if (page.TikiSet)
                {
                    Tiki++;
                    continue;
                }
                if (page.SpookySet)
                {
                    Spooky++;
                    continue;
                }
                if (page.BeeSet)
                {
                    Bee++;
                    continue;
                }
                if (page.SpiderSet)
                {
                    Spider++;
                    continue;
                }
                if (page.SolarSet)
                {
                    Solar++;
                    continue;
                }
                if (page.VortexSet)
                {
                    Vortex++;
                    continue;
                }
                if (page.NebulaSet)
                {
                    Nebula++;
                    continue;
                }
                if (page.StarDustSet)
                {
                    StarDust++;
                    continue;
                }
                if (page.ForbiddenSet)
                {
                    Forbidden++;
                    continue;
                }
                if (page.SquireTier1Set)
                {
                    SquireTier1++;
                    continue;
                }
                if (page.SquireTier2Set)
                {
                    SquireTier2++;
                    continue;
                }
                if (page.ApprenticeTier1Set)
                {
                    ApprenticeTier1++;
                    continue;
                }
                if (page.ApprenticeTier2Set)
                {
                    ApprenticeTier2++;
                    continue;
                }
                if (page.HuntressTier1Set)
                {
                    HuntressTier1++;
                    continue;
                }
                if (page.HuntressTier2Set)
                {
                    HuntressTier2++;
                    continue;
                }
                if (page.MonkTier1Set)
                {
                    MonkTier1++;
                    continue;
                }
                if (page.MonkTier2Set)
                {
                    MonkTier2++;
                    continue;
                }
                if (page.ObsidianOutlawSet)
                {
                    ObsidianOutlaw++;
                    continue;
                }
            }
        }
        internal static void UpdateArmorSetBonus(Player player)
        {
            ResetArmorCounter();
            RestatisticsArmorCounter();
            player.setBonus = "";
            #region
            //ok
            if (Mushroom > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Shroomite") + $"]*{Mushroom}";
                player.shroomiteStealth = true;
                if (Mushroom > 0)
                {
                    player.GetDamage(DamageClass.Ranged) += (1 - player.stealth) * (Mushroom - 1) * 0.6f;
                    player.GetCritChance(DamageClass.Ranged) += (int)((1 - player.stealth) * (Mushroom - 1) * 10);
                    player.GetKnockback(DamageClass.Ranged) *= 1 + (1 - player.stealth) * (Mushroom - 1) * 0.5f;
                    player.aggro -= (int)((1 - player.stealth) * (Mushroom - 1) * 750f);
                }
            }
            //ok
            if (Wooden > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Wood") + $"]*{Wooden}";
                player.statDefense += Wooden;
            }
            //ok
            if (MetalTier1 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.MetalTier1") + $"]*{MetalTier1}";
                player.statDefense += 2 * MetalTier1;
            }
            //ok
            if (MetalTier2 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.MetalTier2") + $"]*{MetalTier2}";
                player.statDefense += 3 * MetalTier2;
            }
            //ok
            if (Platinum > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Platinum") + $"]*{Platinum}";
                player.statDefense += 4 * Platinum;
            }
            //ok
            if (Pumpkin > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Pumpkin") + $"]*{Pumpkin}";
                player.GetDamage(DamageClass.Generic) += 0.1f * Pumpkin;
            }
            //无可叠加效果
            if (Gladiator > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Gladiator");
                player.noKnockback = true;
            }
            //ok
            if (Ninja > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Ninja") + $"]*{Ninja}";
                player.moveSpeed += 0.2f * Ninja;
            }
            //无可叠加效果
            if (Fossil > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Fossil");
                player.ammoCost80 = true;
            }
            //ok
            if (Bone > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Bone") + $"]*{Bone}";
                player.GetCritChance(DamageClass.Ranged) += 10 * Bone;
            }
            //ok
            if (BeetleAttack > 0)
            {
                int num = 0;
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.BeetleDamage") + $"]*{BeetleAttack}";
                player.beetleOffense = true;
                player.beetleCounter -= BeetleAttack * 3;
                player.beetleCounter -= player.beetleCountdown / 10f;
                player.beetleCountdown += BeetleAttack;
                player.beetleCounter = Math.Max(player.beetleCounter, 0);
                if (player.beetleCounter > 7400)
                {
                    player.beetleCounter = 7400;
                }
                int beetlebuff = -1; ;
                if (player.beetleCounter > 6200)
                {
                    beetlebuff = 100;
                    num = 3;
                }
                else
                {
                    if (player.beetleCounter > 1600)
                    {
                        beetlebuff = 99;
                        num = 2;
                    }
                    else
                    {
                        if (player.beetleCounter > 400)
                        {
                            beetlebuff = 98;
                            num = 1;
                        }
                    }
                }
                if (beetlebuff != -1)
                {
                    player.AddBuff(beetlebuff, 5, false, false);
                }
                if (num < player.beetleOrbs)
                {
                    player.beetleCountdown = 0;
                }
                else
                {
                    if (num > player.beetleOrbs)
                    {
                        player.beetleCounter += 200f;
                    }
                }
                if (num != player.beetleOrbs && player.beetleOrbs > 0)
                {
                    for (int j = 0; j < Player.MaxBuffs; j++)
                    {
                        if (player.buffType[j] >= 98 && player.buffType[j] <= 100 && player.buffType[j] != 97 + num)
                        {
                            player.DelBuff(j);
                        }
                    }
                }
            }
            //ok
            if (BeetleDefense > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.BeetleDefense") + $"]*{BeetleDefense}";
                player.beetleDefense = true;
                player.beetleCounter += BeetleDefense;
                if (player.beetleCounter >= 180)
                {
                    if (player.beetleOrbs > 0 && player.beetleOrbs < 3)
                    {
                        for (int k = 0; k < Player.MaxBuffs; k++)
                        {
                            if (player.buffType[k] >= 95 && player.buffType[k] <= 96)
                            {
                                player.DelBuff(k);
                            }
                        }
                    }
                    if (player.beetleOrbs < 3)
                    {
                        player.AddBuff(95 + player.beetleOrbs, 5, false, false);
                        player.beetleCounter = 0f;
                    }
                    else
                    {
                        player.beetleCounter = 180;
                    }
                }
            }
            if (BeetleAttack == 0 && BeetleDefense == 0)
            {
                player.beetleCounter = 0;
            }
            else
            {
                player.beetleFrameCounter++;
                if (player.beetleFrameCounter >= 1)
                {
                    player.beetleFrameCounter = 0;
                    player.beetleFrame++;
                    if (player.beetleFrame > 2)
                    {
                        player.beetleFrame = 0;
                    }
                }
                for (int i = player.beetleOrbs; i < 3; i++)
                {
                    player.beetlePos[i].X = 0f;
                    player.beetlePos[i].Y = 0f;
                }
                for (int m = 0; m < player.beetleOrbs; m++)
                {
                    player.beetlePos[m] += player.beetleVel[m];
                    player.beetleVel[m] += Main.rand.NextVector2Unit() * Main.rand.NextFloat(-0.5f, 0.5f + float.Epsilon);
                    float orbdistance = player.beetlePos[m].Length();
                    if (orbdistance > 100f)
                    {
                        orbdistance = 20f / orbdistance;
                        player.beetlePos[m].X *= -orbdistance;
                        player.beetlePos[m].Y *= -orbdistance;
                        player.beetleVel[m].X = (player.beetleVel[m].X * 9f + player.beetlePos[m].X) / 9f;
                        player.beetleVel[m].Y = (player.beetleVel[m].Y * 9f + player.beetlePos[m].Y) / 9f;
                    }
                    else
                    {
                        if (orbdistance > 30f)
                        {
                            orbdistance = 10f / orbdistance;
                            player.beetlePos[m].X *= -orbdistance;
                            player.beetlePos[m].Y *= -orbdistance;
                            player.beetleVel[m].X = (player.beetleVel[m].X * 20f + player.beetlePos[m].X) / 20f;
                            player.beetleVel[m].Y = (player.beetleVel[m].Y * 20f + player.beetlePos[m].Y) / 20f;
                        }
                    }
                    player.beetlePos[m].X = player.beetleVel[m].X;
                    player.beetlePos[m].Y = player.beetleVel[m].Y;
                    orbdistance = player.beetlePos[m].Length();
                    if (orbdistance > 2f)
                    {
                        player.beetleVel[m] *= 0.9f;
                    }
                    player.beetlePos[m] -= player.velocity * 0.25f;
                }
            }
            //ok
            if (Wizard > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Wizard") + $"]*{Wizard}";
                player.GetCritChance(DamageClass.Magic) += 10 * Wizard;
            }
            //ok
            if (MagicHat > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.MagicHat") + $"]*{MagicHat}";
                player.statManaMax2 += 60 * MagicHat;
            }
            //ok
            if (ShadowScale > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.ShadowScale") + $"]*{ShadowScale}";
                player.moveSpeed += 0.15f * ShadowScale;
            }
            //未实现叠加效果
            if (Crimson > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Crimson");
                player.crimsonRegen = true;
            }
            //ok
            if (GhostHeal > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.SpectreHealing") + $"]*{GhostHeal}";
                player.ghostHeal = true;
                player.GetDamage(DamageClass.Magic) -= 0.4f * GhostHeal;
            }
            //ok
            if (GhostHurt > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.SpectreDamage") + $"]*{GhostHurt}";
                player.ghostHurt = true;
            }
            //无可叠加效果
            if (Meteor > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Meteor");
                player.spaceGun = true;
            }
            //ok
            if (Frost > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Frost") + $"]*{Frost}";
                player.frostBurn = true;
                player.GetDamage(DamageClass.Melee) += 0.1f * Frost;
                player.GetDamage(DamageClass.Ranged) += 0.1f * Frost;
            }
            //ok
            if (Jungle > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Jungle") + $"]*{Jungle}";
                player.manaCost -= 0.16f * Jungle;
            }
            //ok
            if (Molten > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Molten") + $"]*{Molten}";
                player.GetDamage(DamageClass.Melee) += 0.1f * Molten;
                player.buffImmune[24] = true;
            }
            //ok
            if (Mining > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Mining") + $"]*{Mining}";
                player.pickSpeed -= 0.3f * Mining;
            }
            //ok
            if (Chlorophyte > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Chlorophyte");
                player.AddBuff(60, 18000, true, false);
            }
            //ok
            if (ChlorophyteMelee > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.ChlorophyteMelee") + $"]*{ChlorophyteMelee}";
                player.AddBuff(60, 18000, true, false);
                player.endurance += 0.05f * ChlorophyteMelee;
            }
            if (Chlorophyte == 0 && ChlorophyteMelee == 0)
            {
                int index = player.FindBuffIndex(60);
                if (index != -1)
                {
                    player.DelBuff(index);
                }
            }
            //无法实现叠加
            if (Cactus > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Cactus");
                player.cactusThorns = true;
            }
            //ok
            if (Turtle > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Turtle") + $"]*{Turtle}";
                player.endurance += 0.15f * Turtle;
                player.thorns = 1f * Turtle;
                player.turtleThorns = true;
            }
            //ok
            if (CobaltCaster > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.CobaltCaster") + $"]*{CobaltCaster}";
                player.manaCost -= 0.14f * CobaltCaster;
            }
            //ok
            if (CobaltMelee > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.CobaltMelee") + $"]*{CobaltMelee}";
                player.GetAttackSpeed(DamageClass.Melee) += 0.15f * CobaltMelee;
            }
            //无可叠加效果
            if (CobaltRanged > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.CobaltRanged");
                player.ammoCost80 = true;
            }
            //ok
            if (MythrilCaster > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.MythrilCaster") + $"]*{MythrilCaster}";
                player.manaCost -= 0.17f * MythrilCaster;
            }
            //ok
            if (MythrilMelee > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.MythrilMelee") + $"]*{MythrilMelee}";
                player.GetCritChance(DamageClass.Melee) += 10 * MythrilMelee;
            }
            //无可叠加效果
            if (MythrilRanged > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.MythrilRanged");
                player.ammoCost80 = true;
            }
            //ok
            if (AdamantiteCaster > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.AdamantiteCaster") + $"]*{AdamantiteCaster}";
                player.manaCost -= 0.19f * AdamantiteCaster;
            }
            //ok
            if (AdamantiteMelee > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.AdamantiteMelee");
                player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
                player.moveSpeed += 0.2f;
            }
            //无可叠加效果
            if (AdamantiteRanged > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.AdamantiteRanged");
                player.ammoCost75 = true;
            }
            //未实现叠加效果
            if (Palladium > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Palladium");
                player.onHitRegen = true;
            }
            //未实现叠加效果
            if (Orichalcum > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Orichalcum");
                player.onHitPetal = true;
            }
            //未实现叠加效果
            if (Titanium > 0)
            {
                player.setBonus += "\n" + "\n" + Language.GetTextValue("ArmorSetBonus.Titanium");
                player.onHitTitaniumStorm = true;
            }
            //未实现叠加效果
            if (Hallowed > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Hallowed");
                player.onHitDodge = true;
            }
            //ok
            if (HallowedSummoner > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.HallowedSummoner") + $"]*{HallowedSummoner}";
                player.maxMinions += 2 * HallowedSummoner;
                player.onHitDodge = true;
            }
            //ok
            if (CrystalNinja > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.CrystalNinja") + $"]*{CrystalNinja}";
                player.GetDamage(DamageClass.Generic) += 0.1f * CrystalNinja;
                player.GetCritChance(DamageClass.Generic) += 10 * CrystalNinja;
                player.dashType = 5;
            }
            //ok
            if (Tiki > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Tiki") + $"]*{Tiki}";
                player.maxMinions += Tiki;
            }
            //ok
            if (Spooky > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Spooky") + $"]*{Spooky}";
                player.GetDamage(DamageClass.Summon) += 0.25f * Spooky;
            }
            //ok
            if (Bee > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Bee") + $"]*{Bee}";
                player.GetDamage(DamageClass.Summon) += 0.1f * Bee;
                if (player.itemAnimation > 0 && player.inventory[player.selectedItem].type == ItemID.BeeGun)
                {
                    AchievementsHelper.HandleSpecialEvent(player, 3);
                }
            }
            //ok
            if (Spider > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Spider") + $"]*{Spider}";
                player.GetDamage(DamageClass.Summon) += 0.12f * Spider;
            }
            //ok
            if (Solar > 0)
            {
                player.endurance += 0.12f * Solar;
                player.setSolar = true;
                player.setBonus += "\n[" + Language.GetTextValue("ArmorSetBonus.Solar") + $"]*{Solar}";
                player.solarCounter += Solar;
                if (player.solarCounter >= 180)
                {
                    if (player.solarShields > 0 && player.solarShields < 3)
                    {
                        for (int i = 0; i < Player.MaxBuffs; i++)
                        {
                            bool flag81 = player.buffType[i] >= 170 && player.buffType[i] <= 171;
                            if (flag81)
                            {
                                player.DelBuff(i);
                            }
                        }
                    }
                    if (player.solarShields < 3)
                    {
                        player.AddBuff(170 + player.solarShields, 5, false, false);
                        for (int j = 0; j < 16 * Solar; j++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 1f)];
                            dust.noGravity = true;
                            dust.scale = 1.7f;
                            dust.fadeIn = 0.5f;
                            dust.velocity *= 5f;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                        }
                        player.solarCounter = 0;
                    }
                    else
                    {
                        player.solarCounter = 180;
                    }
                }
                for (int i = player.solarShields; i < 3; i++)
                {
                    player.solarShieldPos[i] = Vector2.Zero;
                }
                for (int i = 0; i < player.solarShields; i++)
                {
                    player.solarShieldPos[i] += player.solarShieldVel[i];
                    Vector2 vector = (player.miscCounter / 100f * MathHelper.TwoPi + i * (MathHelper.TwoPi / player.solarShields)).ToRotationVector2() * 6f;
                    vector.X = player.direction * 20;
                    player.solarShieldVel[i] = (vector - player.solarShieldPos[i]) * 0.2f;
                }
                if (player.dashDelay >= 0)
                {
                    player.solarDashing = false;
                    player.solarDashConsumedFlare = false;
                }
                if (player.solarShields > 0 | player.solarDashing && player.dashDelay < 0)
                {
                    player.dashType = 3;
                }
            }
            else
            {
                player.solarCounter = 0;
            }
            //ok
            if (Vortex > 0)
            {
                player.setVortex = true;
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Vortex", Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN")) + $"]{Vortex}";
                if (Vortex > 1)
                {
                    if (player.setVortex)
                    {
                        player.GetDamage(DamageClass.Ranged) += (1 - player.stealth) * (Vortex - 1) * 0.8f;
                        player.GetCritChance(DamageClass.Ranged) += (int)((1 - player.stealth) * (Vortex - 1) * 20f);
                        player.GetKnockback(DamageClass.Ranged) *= 1 + (1 - player.stealth) * (Vortex - 1) * 0.5f;
                        player.aggro -= (int)((1 - player.stealth) * (Vortex - 1) * 1200f);
                    }
                }
            }
            else
            {
                player.setVortex = false;
            }
            //ok
            if (Nebula > 0)
            {
                if (player.nebulaCD > 0)
                {
                    player.nebulaCD -= Nebula;
                }
                player.setNebula = true;
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.Nebula") + $"]*{Nebula}";
            }
            //ok
            if (StarDust > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Stardust", Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
                player.setStardust = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (!player.HasBuff(187))
                    {
                        player.AddBuff(187, 3600, true, false);
                    }
                    if (player.ownedProjectileCounts[623] < 1)
                    {
                        Main.projectile[Projectile.NewProjectile(player.GetSource_GiftOrReward(),
                            player.Center.X,
                            player.Center.Y,
                            0,
                            -1,
                            623,
                            30 * StarDust,
                            10 * StarDust,
                            player.whoAmI,
                            0,
                            0)].originalDamage = 30 * StarDust;
                    }
                }
            }
            //ok
            if (Forbidden > 0)
            {
                player.setBonus += "\n" + Language.GetTextValue("ArmorSetBonus.Forbidden", Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
                player.setForbidden = true;
                player.UpdateForbiddenSetLock();
                Lighting.AddLight(player.Center, 0.8f * Forbidden, 0.7f * Forbidden, 0.2f * Forbidden);
            }
            //ok
            if (SquireTier1 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.SquireTier2") + $"]*{SquireTier1}";
                player.setSquireT2 = true;
                player.maxTurrets += SquireTier1;
            }
            //ok
            if (SquireTier2 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.SquireTier3") + $"]*{SquireTier2}";
                player.setSquireT3 = true;
                player.setSquireT2 = true;
                player.maxTurrets += SquireTier2;
            }
            //ok
            if (ApprenticeTier1 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.ApprenticeTier2") + $"]*{ApprenticeTier1}";
                player.setApprenticeT2 = true;
                player.maxTurrets += ApprenticeTier1;
            }
            //ok
            if (ApprenticeTier2 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.ApprenticeTier3") + $"]*{ApprenticeTier2}";
                player.setApprenticeT3 = true;
                player.setApprenticeT2 = true;
                player.maxTurrets += ApprenticeTier2;
            }
            //ok
            if (HuntressTier1 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.HuntressTier2") + $"]*{HuntressTier1}";
                player.setHuntressT2 = true;
                player.maxTurrets += HuntressTier1;
            }
            //ok
            if (HuntressTier2 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.HuntressTier3") + $"]*{HuntressTier2}";
                player.setHuntressT3 = true;
                player.setHuntressT2 = true;
                player.maxTurrets += HuntressTier2;
            }
            //ok
            if (MonkTier1 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.MonkTier2") + $"]*{MonkTier1}";
                player.setMonkT2 = true;
                player.maxTurrets += MonkTier1;
            }
            //ok
            if (MonkTier2 > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.MonkTier3") + $"]*{MonkTier2}";
                player.setMonkT3 = true;
                player.setMonkT2 = true;
                player.maxTurrets += MonkTier2;
            }
            //ok
            if (ObsidianOutlaw > 0)
            {
                player.setBonus += "\n" + "[" + Language.GetTextValue("ArmorSetBonus.ObsidianOutlaw") + $"]*{ObsidianOutlaw}";
                player.GetDamage(DamageClass.Summon) += 0.15f * ObsidianOutlaw;
                player.whipRangeMultiplier += 0.5f * ObsidianOutlaw;
                //player.whipUseTimeMultiplier *= 1f / (1.35f * ObsidianOutlaw);
                player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) *= 1f / (1.35f * ObsidianOutlaw);
            }
            //ok
            player.ApplyArmorSoundAndDustChanges();
            string bonus = player.setBonus;
            player.setBonus = "";
            Dictionary<string, int> dic = new();
            foreach (Page page in Manager.Activing.pages)
            {
                ItemLoader.UpdateArmorSet(player, page.Armor[0], page.Armor[1], page.Armor[2]);
                if (player.setBonus != "")
                {
                    if (dic.ContainsKey(player.setBonus))
                    {
                        dic[player.setBonus]++;
                    }
                    else
                    {
                        dic.Add(player.setBonus, 1);
                    }
                }
                player.setBonus = "";
            }
            using (var keys = dic.Keys.GetEnumerator())
            {
                while (keys.MoveNext())
                {
                    bonus += "\n[" + keys.Current + $"]*{dic[keys.Current]}";
                }
            }
            player.setBonus = bonus;
            #endregion
        }
        internal static bool QuickBuff_ShouldBotherUsingThisBuff(Player player, int attemptedType)
        {
            bool result = true;
            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (attemptedType == 27 && (player.buffType[i] == 27 || player.buffType[i] == 101 || player.buffType[i] == 102))
                {
                    result = false;
                    break;
                }
                if (BuffID.Sets.IsWellFed[attemptedType] && BuffID.Sets.IsWellFed[player.buffType[i]])
                {
                    result = false;
                    break;
                }
                if (player.buffType[i] == attemptedType)
                {
                    result = false;
                    break;
                }
                if (Main.meleeBuff[attemptedType] && Main.meleeBuff[player.buffType[i]])
                {
                    result = false;
                    break;
                }
            }
            if (Main.lightPet[attemptedType] || Main.vanityPet[attemptedType])
            {
                for (int j = 0; j < Player.MaxBuffs; j++)
                {
                    if (Main.lightPet[player.buffType[j]] && Main.lightPet[attemptedType])
                    {
                        result = false;
                    }
                    if (Main.vanityPet[player.buffType[j]] && Main.vanityPet[attemptedType])
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        internal static int QuickBuff_FindFoodPriority(int buffType)
        {
            int result;
            if (buffType != 26)
            {
                if (buffType != 206)
                {
                    if (buffType != 207)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = 3;
                    }
                }
                else
                {
                    result = 2;
                }
            }
            else
            {
                result = 1;
            }
            return result;
        }
        internal static Item QuickBuff_PickBestFoodItem(Player player)
        {
            int num = 0;
            Item item = null;
            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (player.buffTime[i] >= 1)
                {
                    int num2 = QuickBuff_FindFoodPriority(player.buffType[i]);
                    if (num <= num2)
                    {
                        num = num2 + 1;
                    }
                }
            }
            Item[] array = Manager.Activing.GetItemForQuickUse();
            for (int i = 0; i < array.Length; i++)
            {
                Item item2 = array[i];
                if (!item2.IsAir)
                {
                    int num3 = QuickBuff_FindFoodPriority(item2.buffType);
                    if (num3 >= num && (item is null || item.buffTime < item2.buffTime || num3 > num))
                    {
                        item = item2;
                        num = num3;
                    }
                }
            }
            return item;
        }
        internal static void QuickBuff(Player self)
        {
            if (!(self.cursed || self.CCed || self.dead))
            {
                LegacySoundStyle legacySoundStyle = null;
                if (self.CountBuffs() < Player.MaxBuffs)
                {
                    Item item = QuickBuff_PickBestFoodItem(self);
                    if (item is not null)
                    {
                        legacySoundStyle = item.UseSound;
                        int num = item.buffTime;
                        if (item.buffTime == 0)
                        {
                            num = 3600;
                        }
                        self.AddBuff(item.buffType, num, true, false);
                        if (item.consumable && ItemLoader.ConsumeItem(item, self))
                        {
                            item.stack--;
                            if (item.IsAir)
                            {
                                item.TurnToAir();
                            }
                        }
                    }
                    if (self.CountBuffs() < Player.MaxBuffs)
                    {
                        Item[] array = Manager.Activing.GetItemForQuickUse();
                        Item item2;
                        for (int i = 0; i < array.Length; i++)
                        {
                            item2 = array[i];
                            if (!(item2.stack <= 0 || item2.type <= ItemID.None || item2.buffType <= 0 || item2.DamageType == DamageClass.Summon))
                            {
                                int num2 = item2.buffType;
                                bool flag9 = CombinedHooks.CanUseItem(self, item2) && QuickBuff_ShouldBotherUsingThisBuff(self, num2);
                                if (item2.mana > 0 & flag9)
                                {
                                    if (self.CheckMana(item2, -1, true, true))
                                    {
                                        self.manaRegenDelay = (int)self.maxRegenDelay;
                                    }
                                }
                                if (self.whoAmI == Main.myPlayer && item2.type == ItemID.Carrot && !Main.runningCollectorsEdition)
                                {
                                    flag9 = false;
                                }
                                if (num2 == 27)
                                {
                                    num2 = Main.rand.Next(3);
                                    switch (num2)
                                    {
                                        case 0:
                                            {
                                                num2 = 27;
                                                break;
                                            }
                                        case 1:
                                            {
                                                num2 = 101;
                                                break;
                                            }
                                        case 2:
                                            {
                                                num2 = 103;
                                                break;
                                            }
                                    }
                                }
                                if (flag9)
                                {
                                    ItemLoader.UseItem(item2, self);
                                    legacySoundStyle = item2.UseSound;
                                    int num3 = item2.buffTime;
                                    if (num3 == 0)
                                    {
                                        num3 = 3600;
                                    }
                                    self.AddBuff(num2, num3, true, false);
                                    if (item2.consumable && ItemLoader.ConsumeItem(item2, self))
                                    {
                                        item2.stack--;
                                        if (item.IsAir)
                                        {
                                            item2.TurnToAir();
                                        }
                                    }
                                    if (self.CountBuffs() >= Player.MaxBuffs)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (legacySoundStyle is not null)
                    {
                        SoundEngine.PlaySound(legacySoundStyle, self.position);
                        Recipe.FindRecipes(false);
                    }
                }
            }
        }
        internal static void QuickMana(Player self)
        {
            if (!(self.cursed || self.CCed || self.dead || self.statMana == self.statManaMax2))
            {
                Item willuse;
                Item[] array = Manager.Activing.GetItemForQuickUse();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].stack > 0 && array[i].type > ItemID.None && array[i].healMana > 0 && (self.potionDelay == 0 || !array[i].potion) && CombinedHooks.CanUseItem(self, array[i]))
                    {
                        willuse = array[i];
                        goto mark;
                    }
                }
                return;
            mark:;
                SoundEngine.PlaySound(willuse.UseSound, self.position);
                if (willuse.potion)
                {
                    if (willuse.type == ItemID.RestorationPotion)
                    {
                        self.potionDelay = self.restorationDelayTime;
                        self.AddBuff(21, self.potionDelay, true, false);
                    }
                    else
                    {
                        if (willuse.type == ItemID.Mushroom)
                        {
                            self.potionDelay = self.mushroomDelayTime;
                            self.AddBuff(21, self.potionDelay, true, false);
                        }
                        else
                        {
                            self.potionDelay = self.potionDelayTime;
                            self.AddBuff(21, self.potionDelay, true, false);
                        }
                    }
                }
                ItemLoader.UseItem(willuse, self);
                int healLife = self.GetHealLife(willuse, true);
                int healMana = self.GetHealMana(willuse, true);
                self.statLife += healLife;
                self.statMana += healMana;
                if (self.statLife > self.statLifeMax2)
                {
                    self.statLife = self.statLifeMax2;
                }
                if (self.statMana > self.statManaMax2)
                {
                    self.statMana = self.statManaMax2;
                }
                if (healLife > 0 && Main.myPlayer == self.whoAmI)
                {
                    self.HealEffect(healLife, true);
                }
                if (healMana > 0)
                {
                    self.AddBuff(94, Player.manaSickTime, true, false);
                    bool flag10 = Main.myPlayer == self.whoAmI;
                    if (flag10)
                    {
                        self.ManaEffect(healMana);
                    }
                }
                if (ItemLoader.ConsumeItem(willuse, self))
                {
                    willuse.stack--;
                }
                if (willuse.IsAir)
                {
                    willuse.TurnToAir();
                }
                Recipe.FindRecipes();
            }
        }
        internal static void QuickHeal(Player self)
        {
            if (!(self.cursed || self.CCed || self.dead || self.statLife == self.statLifeMax2 || self.potionDelay > 0))
            {
                Item item = self.QuickHeal_GetItemToUse();
                if (item is not null)
                {
                    SoundEngine.PlaySound(item.UseSound, self.position);
                    if (item.potion)
                    {
                        if (item.type == ItemID.RestorationPotion)
                        {
                            self.potionDelay = self.restorationDelayTime;
                            self.AddBuff(21, self.potionDelay, true, false);
                        }
                        else
                        {
                            if (item.type == ItemID.Mushroom)
                            {
                                self.potionDelay = self.mushroomDelayTime;
                                self.AddBuff(21, self.potionDelay, true, false);
                            }
                            else
                            {
                                self.potionDelay = self.potionDelayTime;
                                self.AddBuff(21, self.potionDelay, true, false);
                            }
                        }
                    }
                    ItemLoader.UseItem(item, self);
                    int healLife = self.GetHealLife(item, true);
                    int healMana = self.GetHealMana(item, true);
                    self.statLife += healLife;
                    self.statMana += healMana;
                    if (self.statLife > self.statLifeMax2)
                    {
                        self.statLife = self.statLifeMax2;
                    }
                    if (self.statMana > self.statManaMax2)
                    {
                        self.statMana = self.statManaMax2;
                    }
                    if (healLife > 0 && Main.myPlayer == self.whoAmI)
                    {
                        self.HealEffect(healLife, true);
                    }
                    if (healMana > 0)
                    {
                        self.AddBuff(94, Player.manaSickTime, true, false);
                        if (Main.myPlayer == self.whoAmI)
                        {
                            self.ManaEffect(healMana);
                        }
                    }
                    if (ItemLoader.ConsumeItem(item, self))
                    {
                        item.stack--;
                    }
                    if (item.IsAir)
                    {
                        item.TurnToAir();
                    }
                    Recipe.FindRecipes();
                }
            }
        }
        internal static Item QuickMana_GetItemToUse(Player self)
        {
            Item result;
            Item[] array = Manager.Activing.GetItemForQuickUse();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].stack > 0 && array[i].type > ItemID.None && array[i].healMana > 0 && (self.potionDelay == 0 || !array[i].potion) && CombinedHooks.CanUseItem(self, array[i]))
                {
                    result = array[i];
                    return result;
                }
            }
            result = null;
            return result;
        }
        internal static Item QuickHeal_GetItemToUse(Player self)
        {
            int num = self.statLifeMax2 - self.statLife;
            Item result = null;
            int num2 = -self.statLifeMax2;
            Item[] array = Manager.Activing.GetItemForQuickUse();
            Item item;
            for (int i = 0; i < array.Length; i++)
            {
                item = array[i];
                if (!(item.stack <= 0 || item.type <= ItemID.None || !item.potion || item.healLife <= 0))
                {
                    if (CombinedHooks.CanUseItem(self, item))
                    {
                        int num3 = self.GetHealLife(item, true) - num;
                        if (item.type == ItemID.RestorationPotion && num3 < 0)
                        {
                            num3 += 30;
                            if (num3 > 0)
                            {
                                num3 = 0;
                            }
                        }
                        if (num2 < 0)
                        {
                            if (num3 > num2)
                            {
                                result = item;
                                num2 = num3;
                            }
                        }
                        else
                        {
                            if (num3 < num2 && num3 >= 0)
                            {
                                result = item;
                                num2 = num3;
                            }
                        }
                    }
                }
            }
            return result;
        }
        internal static void UpdateEquips(Player player)
        {
            Manager.Activing.SaveArmor();
            Manager.Activing.SaveAccessory();
            player.VanillaPreUpdateInventory();
            if (Config.EnableInventoryEnhancement)
            {
                foreach (Page page in Manager.Activing.pages)
                {
                    for (int i = 0; i < 58; i++)
                    {
                        player.VanillaUpdateInventory(page.Inventory[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 58; i++)
                {
                    player.VanillaUpdateInventory(player.inventory[i]);
                }
            }
            player.VanillaPostUpdateInventory();
            if (Config.EnableArmorEnhancement)
            {
                foreach (Page page in Manager.Activing.pages)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        player.VanillaUpdateEquip(page.Armor[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    player.VanillaUpdateEquip(player.armor[i]);
                }
            }
            player.equippedAnyWallSpeedAcc = false;
            player.equippedAnyTileSpeedAcc = false;
            player.equippedAnyTileRangeAcc = false;
            if (player.whoAmI == Main.myPlayer)
            {
                Main.musicBoxNotModifiedByVolume = -1;
            }
            int wingslot = -1;
            if (Config.EnableAccessoryEnhancement)
            {
                foreach (Item item in Manager.Activing.pages[Manager.Activing.NowAccessoryIndex].Armor[3..10])
                {
                    if (item.wingSlot != 0)
                    {
                        wingslot = item.wingSlot;
                    }
                }
                foreach (Page page in Manager.Activing.pages)
                {
                    for (int i = 3; i < 10; i++)
                    {
                        if (player.IsAValidEquipmentSlotForIteration(i))
                        {
                            player.VanillaUpdateEquip(page.Armor[i]);
                            player.ApplyEquipFunctional(page.Armor[i], page.HideAccessory[i]);
                        }
                    }
                }
                if (wingslot != -1)
                {
                    player.wingsLogic = wingslot;
                }
            }
            else
            {
                for (int i = 3; i < 10; i++)
                {
                    if (player.IsAValidEquipmentSlotForIteration(i))
                    {
                        player.VanillaUpdateEquip(player.armor[i]);
                        player.ApplyEquipFunctional(player.armor[i], player.hideVisibleAccessory[i]);
                    }
                }
            }
            PlayerLoader.UpdateEquips(player);
            if (player.kbGlove)
            {
                player.GetKnockback(DamageClass.Melee) *= 2f;
            }
            if (player.skyStoneEffects)
            {
                player.lifeRegen += 2;
                player.statDefense += 4;
                //player.meleeSpeed += 0.1f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
                player.GetDamage(DamageClass.Generic) += 0.1f;
                player.GetCritChance(DamageClass.Generic) += 2;
                player.pickSpeed -= 0.15f;
                //player.minionKB += 0.5f;
                player.GetKnockback(DamageClass.Summon) += 0.5f;
            }
            if (player.dd2Accessory)
            {
                player.GetDamage(DamageClass.Summon) += 0.1f;
                player.maxTurrets++;
            }
            if (Config.EnableDecorationEnhancement)
            {
                foreach (Page page in Manager.Activing.pages)
                {
                    for (int i = 13; i < 20; i++)
                    {
                        if (player.IsAValidEquipmentSlotForIteration(i))
                        {
                            player.ApplyEquipVanity(page.Armor[i]);
                        }
                    }
                }
            }
            else
            {
                for (int i = 13; i < 20; i++)
                {
                    if (player.IsAValidEquipmentSlotForIteration(i))
                    {
                        player.ApplyEquipVanity(player.armor[i]);
                    }
                }
            }
            if (player.wet && player.ShouldFloatInWater)
            {
                player.accFlipper = true;
            }
            if (player.whoAmI == Main.myPlayer && Main.SceneMetrics.HasClock && player.accWatch < 3)
            {
                player.accWatch++;
            }
            if (player.equippedAnyTileSpeedAcc && player.inventory[player.selectedItem].createTile != TileID.Torches)
            {
                player.tileSpeed += 0.5f;
            }
            if (player.equippedAnyWallSpeedAcc)
            {
                player.wallSpeed += 0.5f;
            }
            if (player.equippedAnyTileRangeAcc && player.whoAmI == Main.myPlayer)
            {
                Player.tileRangeX += 3;
                Player.tileRangeY += 2;
            }
            if (!player.accThirdEye)
            {
                player.accThirdEyeCounter = 0;
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
            {
                for (int n = 0; n < 255; n++)
                {
                    if (!(n == player.whoAmI || !Main.player[n].active || Main.player[n].dead || Main.player[n].team != player.team || Main.player[n].team == 0))
                    {
                        int num = 800;
                        if ((Main.player[n].Center - player.Center).Length() < num)
                        {
                            if (Main.player[n].accWatch > player.accWatch)
                            {
                                player.accWatch = Main.player[n].accWatch;
                            }
                            if (Main.player[n].accCompass > player.accCompass)
                            {
                                player.accCompass = Main.player[n].accCompass;
                            }
                            if (Main.player[n].accDepthMeter > player.accDepthMeter)
                            {
                                player.accDepthMeter = Main.player[n].accDepthMeter;
                            }
                            if (Main.player[n].accFishFinder)
                            {
                                player.accFishFinder = true;
                            }
                            if (Main.player[n].accWeatherRadio)
                            {
                                player.accWeatherRadio = true;
                            }
                            if (Main.player[n].accThirdEye)
                            {
                                player.accThirdEye = true;
                            }
                            if (Main.player[n].accJarOfSouls)
                            {
                                player.accJarOfSouls = true;
                            }
                            if (Main.player[n].accCalendar)
                            {
                                player.accCalendar = true;
                            }
                            if (Main.player[n].accStopwatch)
                            {
                                player.accStopwatch = true;
                            }
                            if (Main.player[n].accOreFinder)
                            {
                                player.accOreFinder = true;
                            }
                            if (Main.player[n].accCritterGuide)
                            {
                                player.accCritterGuide = true;
                            }
                            if (Main.player[n].accDreamCatcher)
                            {
                                player.accDreamCatcher = true;
                            }
                        }
                    }
                }
            }
            if (!player.accDreamCatcher && player.dpsStarted)
            {
                player.dpsStarted = false;
                player.dpsEnd = DateTime.Now;
            }
            if (player.HeldItem.type == 4760 && player.ownedProjectileCounts[866] < 1)
            {
                player.hasRaisableShield = true;
            }
        }
        internal static void PickAmmo(Player self, Item sItem, ref int projToShoot, ref float speed, ref bool canShoot, ref int damage, ref float knockBack, out int usedAmmoItemId, bool dontConsume)
        {
            Item item;
            usedAmmoItemId = 0;
            if (sItem.useAmmo == AmmoID.Coin)
            {
                for (int i = 50; i < 54; i++)
                {
                    if (Manager.Activing.NowInventory[i].ammo == sItem.useAmmo && Manager.Activing.NowInventory[i].stack > 0)
                    {
                        item = Manager.Activing.NowInventory[i];
                        canShoot = true;
                        goto endselect;
                    }
                }
                foreach (Page page in Manager.Activing.pages)
                {
                    if (page.index == Manager.Activing.NowInventoryIndex)
                    {
                        continue;
                    }
                    for (int i = 50; i < 54; i++)
                    {
                        if (page.Inventory[i].ammo == sItem.useAmmo && page.Inventory[i].stack > 0)
                        {
                            item = page.Inventory[i];
                            canShoot = true;
                            goto endselect;
                        }
                    }
                }
            }
            for (int i = 54; i < 58; i++)
            {
                if (Manager.Activing.NowInventory[i].ammo == sItem.useAmmo && Manager.Activing.NowInventory[i].stack > 0)
                {
                    item = Manager.Activing.NowInventory[i];
                    canShoot = true;
                    goto endselect;
                }
            }
            foreach (Page page in Manager.Activing.pages)
            {
                if (page.index == Manager.Activing.NowInventoryIndex)
                {
                    continue;
                }
                for (int i = 54; i < 58; i++)
                {
                    if (page.Inventory[i].ammo == sItem.useAmmo && page.Inventory[i].stack > 0)
                    {
                        item = page.Inventory[i];
                        canShoot = true;
                        goto endselect;
                    }
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if (Manager.Activing.NowInventory[i].ammo == sItem.useAmmo && Manager.Activing.NowInventory[i].stack > 0)
                {
                    item = Manager.Activing.NowInventory[i];
                    canShoot = true;
                    goto endselect;
                }
            }
            foreach (Page page in Manager.Activing.pages)
            {
                if (page.index == Manager.Activing.NowInventoryIndex)
                {
                    continue;
                }
                for (int i = 0; i < 50; i++)
                {
                    if (page.Inventory[i].ammo == sItem.useAmmo && page.Inventory[i].stack > 0)
                    {
                        item = page.Inventory[i];
                        canShoot = true;
                        goto endselect;
                    }
                }
            }
            if (Config.PickAmmoFromSpeicalChest)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (self.bank.item[i].ammo == sItem.useAmmo && self.bank.item[i].stack > 0)
                    {
                        item = self.bank.item[i];
                        canShoot = true;
                        goto endselect;
                    }
                }
                for (int i = 0; i < 40; i++)
                {
                    if (self.bank2.item[i].ammo == sItem.useAmmo && self.bank2.item[i].stack > 0)
                    {
                        item = self.bank2.item[i];
                        canShoot = true;
                        goto endselect;
                    }
                }
                for (int i = 0; i < 40; i++)
                {
                    if (self.bank3.item[i].ammo == sItem.useAmmo && self.bank3.item[i].stack > 0)
                    {
                        item = self.bank3.item[i];
                        canShoot = true;
                        goto endselect;
                    }
                }
                for (int i = 0; i < 40; i++)
                {
                    if (self.bank4.item[i].ammo == sItem.useAmmo && self.bank4.item[i].stack > 0)
                    {
                        item = self.bank4.item[i];
                        canShoot = true;
                        goto endselect;
                    }
                }
            }
            canShoot = false;
            return;
        endselect:;
            usedAmmoItemId = item.type;
            if (AmmoID.Sets.SpecificLauncherAmmoProjectileMatches.TryGetValue(sItem.type, out Dictionary<int, int> dic) && dic.TryGetValue(item.type, out int picked))
            {
                projToShoot = picked;
            }
            else
            {
                if (sItem.type == ItemID.SnowmanCannon)
                {
                    projToShoot = 338 + item.type - 771;
                    if (projToShoot > 341)
                    {
                        projToShoot = 341;
                    }
                }
                else
                {
                    if (sItem.type == 3930)
                    {
                        projToShoot = 715 + item.type - AmmoID.Rocket;
                    }
                    else
                    {
                        if (sItem.useAmmo == AmmoID.Rocket)
                        {
                            projToShoot += item.shoot;
                        }
                        else
                        {
                            if (sItem.useAmmo == 780)
                            {
                                projToShoot += item.shoot;
                            }
                            else
                            {
                                if (item.shoot > ProjectileID.None)
                                {
                                    projToShoot = item.shoot;
                                }
                            }
                        }
                    }
                }
            }
            if (sItem.type == ItemID.HellwingBow && projToShoot == 1)
            {
                projToShoot = 485;
            }
            if (sItem.type == ItemID.ShadowFlameBow)
            {
                projToShoot = 495;
            }
            if (sItem.type == 4953 && projToShoot == 1)
            {
                projToShoot = 932;
            }
            if (sItem.type == 4381)
            {
                projToShoot = 819;
            }
            if (sItem.type == 4058 && projToShoot == 474)
            {
                projToShoot = 117;
            }
            if (projToShoot == 42)
            {
                if (item.type == ItemID.EbonsandBlock)
                {
                    projToShoot = 65;
                    damage += 5;
                }
                else
                {
                    if (item.type == ItemID.PearlsandBlock)
                    {
                        projToShoot = 68;
                        damage += 5;
                    }
                    else
                    {
                        if (item.type == ItemID.CrimsandBlock)
                        {
                            projToShoot = 354;
                            damage += 5;
                        }
                    }
                }
            }
            if (self.HeldItem.type == ItemID.BeesKnees && projToShoot == 1)
            {
                projToShoot = 469;
            }
            if (self.hasMoltenQuiver && projToShoot == 1)
            {
                projToShoot = 2;
                damage += 2;
            }
            if (self.magicQuiver && (sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake))
            {
                knockBack *= 1.1f;
                speed *= 1.1f;
            }
            speed += item.shootSpeed;
            if (item.DamageType == DamageClass.Ranged)
            {
                if (item.damage > 0)
                {
                    if (sItem.damage > 0)
                    {
                        damage += item.damage * damage / sItem.damage;
                    }
                    else
                    {
                        damage += item.damage;
                    }
                }
            }
            else
            {
                damage += item.damage;
            }
            if ((sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake) && self.archery)
            {
                if (speed < 20f)
                {
                    speed *= 1.2f;
                    if (speed > 20f)
                    {
                        speed = 20f;
                    }
                }
            }
            knockBack += item.knockBack;
            ItemLoader.PickAmmo(sItem, item, self, ref projToShoot, ref speed, ref damage, ref knockBack);
            bool willcosume = dontConsume;
            if (sItem.type == ItemID.VortexBeater && Main.rand.Next(3) != 0)
            {
                willcosume = true;
            }
            if (sItem.type == 3930 && Main.rand.Next(2) == 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.Phantasm && Main.rand.Next(3) != 0)
            {
                willcosume = true;
            }
            if (self.magicQuiver && (sItem.useAmmo == AmmoID.Arrow || sItem.useAmmo == AmmoID.Stake) && Main.rand.Next(5) == 0)
            {
                willcosume = true;
            }
            if (self.ammoBox && Main.rand.Next(5) == 0)
            {
                willcosume = true;
            }
            if (self.ammoPotion && Main.rand.Next(5) == 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.CandyCornRifle && Main.rand.Next(3) == 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.Minishark && Main.rand.Next(3) == 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.Gatligator && Main.rand.Next(2) == 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.Megashark && Main.rand.Next(2) == 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.ChainGun && Main.rand.Next(2) == 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.SDMG && Main.rand.Next(3) != 0)
            {
                willcosume = true;
            }
            if (sItem.type == ItemID.ClockworkAssaultRifle && self.itemAnimation < self.itemAnimationMax)
            {
                willcosume = true;
            }
            if (sItem.type == 4953 && self.itemAnimation < self.itemAnimationMax)
            {
                willcosume = true;
            }
            if (self.huntressAmmoCost90 && Main.rand.Next(10) == 0)
            {
                willcosume = true;
            }
            if (self.chloroAmmoCost80 && Main.rand.Next(5) == 0)
            {
                willcosume = true;
            }
            if (self.ammoCost80 && Main.rand.Next(5) == 0)
            {
                willcosume = true;
            }
            if (self.ammoCost75 && Main.rand.Next(4) == 0)
            {
                willcosume = true;
            }
            if (projToShoot == 85 && self.itemAnimation < self.itemAnimationMax - 5)
            {
                willcosume = true;
            }
            if ((projToShoot == 145 || projToShoot == 146 || projToShoot == 147 || projToShoot == 148 || projToShoot == 149) && self.itemAnimation < self.itemAnimationMax - 5)
            {
                willcosume = true;
            }
            if (!(willcosume | !CombinedHooks.CanConsumeAmmo(self, sItem, item)) && item.consumable)
            {
                CombinedHooks.OnConsumeAmmo(self, sItem, item);
                item.stack--;
                if (item.IsAir)
                {
                    item.active = false;
                    item.TurnToAir();
                }
            }
        }
        internal static bool Player_BuyItem(Player self, int price, int customCurrency)
        {
            if (customCurrency != -1)
            {
                return CustomCurrencyManager.BuyItem(self, price, customCurrency);
            }
            Item[] invs = Manager.Activing.GetItemForBuySell();
            long num = Utils.CoinsCount(out bool flag, invs, Array.Empty<int>());
            long num2 = Utils.CoinsCount(out flag, self.bank.item, Array.Empty<int>());
            long num3 = Utils.CoinsCount(out flag, self.bank2.item, Array.Empty<int>());
            long num4 = Utils.CoinsCount(out flag, self.bank3.item, Array.Empty<int>());
            long num5 = Utils.CoinsCount(out flag, self.bank4.item, Array.Empty<int>());
            if (Utils.CoinsCombineStacks(out flag, new long[]
            {
                num,
                num2,
                num3,
                num4,
                num5
            }) < price)
            {
                return false;
            }
            List<Item[]> list = new();
            Dictionary<int, List<int>> dictionary = new();
            List<Point> list2 = new();
            List<Point> list3 = new();
            List<Point> list4 = new();
            List<Point> list5 = new();
            List<Point> list6 = new();
            List<Point> list7 = new();
            list.Add(invs);
            list.Add(self.bank.item);
            list.Add(self.bank2.item);
            list.Add(self.bank3.item);
            list.Add(self.bank4.item);
            for (int i = 0; i < list.Count; i++)
            {
                dictionary.Add(i, new List<int>());
            }
            for (int j = 0; j < list.Count; j++)
            {
                for (int k = 0; k < list[j].Length; k++)
                {
                    if (!dictionary[j].Contains(k) && list[j][k].IsACoin)
                    {
                        list3.Add(new Point(j, k));
                    }
                }
            }
            int num6 = 0;
            for (int l = list[num6].Length - 1; l >= 0; l--)
            {
                if (!dictionary[num6].Contains(l) && (list[num6][l].type == ItemID.None || list[num6][l].stack == 0))
                {
                    list2.Add(new Point(num6, l));
                }
            }
            num6 = 1;
            for (int m = list[num6].Length - 1; m >= 0; m--)
            {
                if (!dictionary[num6].Contains(m) && (list[num6][m].type == ItemID.None || list[num6][m].stack == 0))
                {
                    list4.Add(new Point(num6, m));
                }
            }
            num6 = 2;
            for (int n = list[num6].Length - 1; n >= 0; n--)
            {
                if (!dictionary[num6].Contains(n) && (list[num6][n].type == ItemID.None || list[num6][n].stack == 0))
                {
                    list5.Add(new Point(num6, n));
                }
            }
            num6 = 3;
            for (int num7 = list[num6].Length - 1; num7 >= 0; num7--)
            {
                if (!dictionary[num6].Contains(num7) && (list[num6][num7].type == ItemID.None || list[num6][num7].stack == 0))
                {
                    list6.Add(new Point(num6, num7));
                }
            }
            num6 = 4;
            for (int num8 = list[num6].Length - 1; num8 >= 0; num8--)
            {
                if (!dictionary[num6].Contains(num8) && (list[num6][num8].type == ItemID.None || list[num6][num8].stack == 0))
                {
                    list7.Add(new Point(num6, num8));
                }
            }
            return !TryPurchasingCoins(price, list, list3, list2, list4, list5, list6, list7);
        }
        private static bool TryPurchasingCoins(int price, List<Item[]> inv, List<Point> slotCoins, List<Point> slotsEmpty, List<Point> slotEmptyBank, List<Point> slotEmptyBank2, List<Point> slotEmptyBank3, List<Point> slotEmptyBank4)
        {
            long num = price;
            Dictionary<Point, Item> dictionary = new();
            bool result = false;
            while (num > 0L)
            {
                //dictionary.Clear();
                long num2 = 1000000L;
                for (int i = 0; i < 4; i++)
                {
                    if (num >= num2)
                    {
                        using (List<Point>.Enumerator enumerator = slotCoins.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Point current = enumerator.Current;
                                if (inv[current.X][current.Y].type == 74 - i)
                                {
                                    long num3 = num / num2;
                                    dictionary[current] = inv[current.X][current.Y].Clone();
                                    //dictionary.Add(current, inv[current.X][current.Y].Clone());
                                    if (num3 < inv[current.X][current.Y].stack)
                                    {
                                        inv[current.X][current.Y].stack -= (int)num3;
                                    }
                                    else
                                    {
                                        inv[current.X][current.Y].SetDefaults(0);
                                        //slotsEmpty.Add(current);
                                    }
                                    num -= num2 * (long)(dictionary[current].stack - inv[current.X][current.Y].stack);
                                }
                            }
                        }
                    }
                    num2 /= 100L;
                }
                if (num > 0L)
                {
                    if (slotsEmpty.Count <= 0)
                    {
                        using (Dictionary<Point, Item>.Enumerator enumerator2 = dictionary.GetEnumerator())
                        {
                            while (enumerator2.MoveNext())
                            {
                                KeyValuePair<Point, Item> current2 = enumerator2.Current;
                                inv[current2.Key.X][current2.Key.Y] = current2.Value.Clone();
                            }
                        }
                        result = true;
                        break;
                    }
                    slotsEmpty.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
                    Point point = new(-1, -1);
                    for (int j = 0; j < inv.Count; j++)
                    {
                        num2 = 10000L;
                        for (int k = 0; k < 3; k++)
                        {
                            if (num >= num2)
                            {
                                using (List<Point>.Enumerator enumerator = slotCoins.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        Point current3 = enumerator.Current;
                                        if (current3.X == j && inv[current3.X][current3.Y].type == 74 - k && inv[current3.X][current3.Y].stack >= 1)
                                        {
                                            List<Point> list = slotsEmpty;
                                            if (j == 1 && slotEmptyBank.Count > 0)
                                            {
                                                list = slotEmptyBank;
                                            }
                                            if (j == 2 && slotEmptyBank2.Count > 0)
                                            {
                                                list = slotEmptyBank2;
                                            }
                                            if (j == 3 && slotEmptyBank3.Count > 0)
                                            {
                                                list = slotEmptyBank3;
                                            }
                                            if (j == 4 && slotEmptyBank4.Count > 0)
                                            {
                                                list = slotEmptyBank4;
                                            }
                                            Item expr_266 = inv[current3.X][current3.Y];
                                            int num4 = expr_266.stack - 1;
                                            expr_266.stack = num4;
                                            if (num4 <= 0)
                                            {
                                                inv[current3.X][current3.Y].SetDefaults(0);
                                                list.Add(current3);
                                            }
                                            dictionary.Add(list[0], inv[list[0].X][list[0].Y].Clone());
                                            inv[list[0].X][list[0].Y].SetDefaults(73 - k);
                                            inv[list[0].X][list[0].Y].stack = 100;
                                            point = list[0];
                                            list.RemoveAt(0);
                                            break;
                                        }
                                    }
                                }
                            }
                            if (point.X != -1 || point.Y != -1)
                            {
                                break;
                            }
                            num2 /= 100L;
                        }
                        for (int l = 0; l < 2; l++)
                        {
                            if (point.X == -1 && point.Y == -1)
                            {
                                using (List<Point>.Enumerator enumerator = slotCoins.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        Point current4 = enumerator.Current;
                                        if (current4.X == j && inv[current4.X][current4.Y].type == 73 + l && inv[current4.X][current4.Y].stack >= 1)
                                        {
                                            List<Point> list2 = slotsEmpty;
                                            if (j == 1 && slotEmptyBank.Count > 0)
                                            {
                                                list2 = slotEmptyBank;
                                            }
                                            if (j == 2 && slotEmptyBank2.Count > 0)
                                            {
                                                list2 = slotEmptyBank2;
                                            }
                                            if (j == 3 && slotEmptyBank3.Count > 0)
                                            {
                                                list2 = slotEmptyBank3;
                                            }
                                            if (j == 4 && slotEmptyBank4.Count > 0)
                                            {
                                                list2 = slotEmptyBank4;
                                            }
                                            Item expr_46D = inv[current4.X][current4.Y];
                                            int num4 = expr_46D.stack - 1;
                                            expr_46D.stack = num4;
                                            if (num4 <= 0)
                                            {
                                                inv[current4.X][current4.Y].SetDefaults(0);
                                                list2.Add(current4);
                                            }
                                            dictionary.Add(list2[0], inv[list2[0].X][list2[0].Y].Clone());
                                            inv[list2[0].X][list2[0].Y].SetDefaults(72 + l);
                                            inv[list2[0].X][list2[0].Y].stack = 100;
                                            point = list2[0];
                                            list2.RemoveAt(0);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (point.X != -1 && point.Y != -1)
                        {
                            slotCoins.Add(point);
                            break;
                        }
                    }
                    slotsEmpty.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
                    slotEmptyBank.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
                    slotEmptyBank2.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
                    slotEmptyBank3.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
                    slotEmptyBank4.Sort(new Comparison<Point>(DelegateMethods.CompareYReverse));
                }
            }
            return result;
        }
        internal static bool Player_BuyItem_Custom(Player player, int price, int currencyIndex)
        {
            var dictionary = (Dictionary<int, CustomCurrencySystem>)typeof(CustomCurrencyManager).GetField("_currencies", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance).GetValue(null);
            if (dictionary is null || !dictionary.TryGetValue(currencyIndex, out CustomCurrencySystem customCurrencySystem))
            {
                return false;
            }
            Item[] invs = Manager.Activing.GetItemForBuySell();
            long num = customCurrencySystem.CountCurrency(out bool flag, invs, Array.Empty<int>());
            long num2 = customCurrencySystem.CountCurrency(out flag, player.bank.item, Array.Empty<int>());
            long num3 = customCurrencySystem.CountCurrency(out flag, player.bank2.item, Array.Empty<int>());
            long num4 = customCurrencySystem.CountCurrency(out flag, player.bank3.item, Array.Empty<int>());
            long num5 = customCurrencySystem.CountCurrency(out flag, player.bank4.item, Array.Empty<int>());
            if (customCurrencySystem.CombineStacks(out flag, new long[]
            {
                num,
                num2,
                num3,
                num4,
                num5
            }) < price)
            {
                return false;
            }
            List<Item[]> list = new();
            Dictionary<int, List<int>> dictionary2 = new();
            List<Point> list2 = new();
            List<Point> list3 = new();
            List<Point> list4 = new();
            List<Point> list5 = new();
            List<Point> list6 = new();
            List<Point> list7 = new();
            list.Add(player.inventory);
            list.Add(player.bank.item);
            list.Add(player.bank2.item);
            list.Add(player.bank3.item);
            list.Add(player.bank4.item);
            for (int i = 0; i < list.Count; i++)
            {
                dictionary2.Add(i, new List<int>());
            }
            for (int j = 0; j < list.Count; j++)
            {
                for (int k = 0; k < list[j].Length; k++)
                {
                    if (!dictionary2[j].Contains(k) && customCurrencySystem.Accepts(list[j][k]))
                    {
                        list3.Add(new Point(j, k));
                    }
                }
            }
            FindEmptySlots(list, dictionary2, list2, 0);
            FindEmptySlots(list, dictionary2, list4, 1);
            FindEmptySlots(list, dictionary2, list5, 2);
            FindEmptySlots(list, dictionary2, list6, 3);
            FindEmptySlots(list, dictionary2, list7, 4);
            return customCurrencySystem.TryPurchasing(price, list, list3, list2, list4, list5, list6, list7);
        }
        private static void FindEmptySlots(List<Item[]> inventories, Dictionary<int, List<int>> slotsToIgnore, List<Point> emptySlots, int currentInventoryIndex)
        {
            for (int i = inventories[currentInventoryIndex].Length - 1; i >= 0; i--)
            {
                if (!slotsToIgnore[currentInventoryIndex].Contains(i) && (inventories[currentInventoryIndex][i].type == ItemID.None || inventories[currentInventoryIndex][i].stack == 0))
                {
                    emptySlots.Add(new Point(currentInventoryIndex, i));
                }
            }
        }
        internal static bool SellItem(Player player, Item item, int stack = -1)
        {
            player.GetItemExpectedPrice(item, out int num, out int num2);
            if (num <= 0)
            {
                return false;
            }
            if (stack == -1)
            {
                stack = item.stack;
            }
            Item[] arrayorig;
            List<Item> items = new();
            foreach (Page p in Manager.Activing.pages)
            {
                items.AddRange(p.Inventory[0..58]);
            }
            arrayorig = items.ToArray();
            Item[] array = new Item[arrayorig.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = arrayorig[i].Clone();
            }
            int j = num / 5;
            if (j < 1)
            {
                j = 1;
            }
            int num3 = j;
            j *= stack;
            int amount = Main.shopSellbackHelper.GetAmount(item);
            if (amount > 0)
            {
                j += (-num3 + num2) * Math.Min(amount, item.stack);
            }
            bool flag = false;
            while (j >= 1000000)
            {
                if (flag)
                {
                    break;
                }
                int num4 = -1;
                for (int k = 53; k >= 0; k--)
                {
                    if (num4 == -1 && (player.inventory[k].type == ItemID.None || player.inventory[k].stack == 0))
                    {
                        num4 = k;
                    }
                    while (player.inventory[k].type == ItemID.PlatinumCoin && player.inventory[k].stack < player.inventory[k].maxStack && j >= 1000000)
                    {
                        player.inventory[k].stack++;
                        j -= 1000000;
                        DoCoins(player.inventory[k]);
                        if (player.inventory[k].stack == 0 && num4 == -1)
                        {
                            num4 = k;
                        }
                    }
                }
                if (j >= 1000000)
                {
                    if (num4 == -1)
                    {
                        flag = true;
                    }
                    else
                    {
                        player.inventory[num4].SetDefaults(74);
                        j -= 1000000;
                    }
                }
            }
            while (j >= 10000)
            {
                if (flag)
                {
                    break;
                }
                int num5 = -1;
                for (int l = 53; l >= 0; l--)
                {
                    if (num5 == -1 && (player.inventory[l].type == ItemID.None || player.inventory[l].stack == 0))
                    {
                        num5 = l;
                    }
                    while (player.inventory[l].type == ItemID.GoldCoin && player.inventory[l].stack < player.inventory[l].maxStack && j >= 10000)
                    {
                        player.inventory[l].stack++;
                        j -= 10000;
                        DoCoins(player.inventory[l]);
                        if (player.inventory[l].stack == 0 && num5 == -1)
                        {
                            num5 = l;
                        }
                    }
                }
                if (j >= 10000)
                {
                    if (num5 == -1)
                    {
                        flag = true;
                    }
                    else
                    {
                        player.inventory[num5].SetDefaults(73);
                        j -= 10000;
                    }
                }
            }
            while (j >= 100)
            {
                if (flag)
                {
                    break;
                }
                int num6 = -1;
                for (int m = 53; m >= 0; m--)
                {
                    if (num6 == -1 && (player.inventory[m].type == ItemID.None || player.inventory[m].stack == 0))
                    {
                        num6 = m;
                    }
                    while (player.inventory[m].type == ItemID.SilverCoin && player.inventory[m].stack < player.inventory[m].maxStack && j >= 100)
                    {
                        player.inventory[m].stack++;
                        j -= 100;
                        DoCoins(player.inventory[m]);
                        if (player.inventory[m].stack == 0 && num6 == -1)
                        {
                            num6 = m;
                        }
                    }
                }
                if (j >= 100)
                {
                    if (num6 == -1)
                    {
                        flag = true;
                    }
                    else
                    {
                        player.inventory[num6].SetDefaults(72);
                        j -= 100;
                    }
                }
            }
            while (j >= 1 && !flag)
            {
                int num7 = -1;
                for (int n = 53; n >= 0; n--)
                {
                    if (num7 == -1 && (player.inventory[n].type == ItemID.None || player.inventory[n].stack == 0))
                    {
                        num7 = n;
                    }
                    while (player.inventory[n].type == ItemID.CopperCoin && player.inventory[n].stack < player.inventory[n].maxStack && j >= 1)
                    {
                        player.inventory[n].stack++;
                        j--;
                        DoCoins(player.inventory[n]);
                        if (player.inventory[n].stack == 0 && num7 == -1)
                        {
                            num7 = n;
                        }
                    }
                }
                if (j >= 1)
                {
                    if (num7 == -1)
                    {
                        flag = true;
                    }
                    else
                    {
                        player.inventory[num7].SetDefaults(71);
                        j--;
                    }
                }
            }
            if (flag)
            {
                for (int num8 = 0; num8 < 58; num8++)
                {
                    player.inventory[num8] = array[num8].Clone();
                }
                return false;
            }
            return true;
        }
        internal static void DoCoins(Item item)
        {
            if (item.stack != 100 || (item.type != ItemID.CopperCoin && item.type != ItemID.SilverCoin && item.type != ItemID.GoldCoin))
            {
                return;
            }
            item.SetDefaults(item.type + 1);
            foreach (Item item2 in Manager.Activing.GetItemForBuySell())
            {
                if (item2.IsSameAs(item) && item2 != item && item2.type == item.type && item2.stack < item2.maxStack)
                {
                    item2.stack++;
                    item.SetDefaults(0);
                    item.active = false;
                    item.TurnToAir();
                    DoCoins(item2);
                }
            }
        }
    }
}