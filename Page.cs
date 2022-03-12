using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LargerInventory
{
    internal class Page
    {
        internal Page(int whoami)
        {
            index = whoami;
            for (int i = 0; i < 59; i++)
            {
                Inventory[i] = new();
            }
            for (int i = 0; i < 10; i++)
            {
                HideAccessory[i] = false;
            }
            for (int i = 0; i < 20; i++)
            {
                Armor[i] = new();
            }
            for (int i = 0; i < 10; i++)
            {
                Dye[i] = new();
            }
            for (int i = 0; i < 5; i++)
            {
                MiscEquips[i] = new();
            }
            for (int i = 0; i < 5; i++)
            {
                MiscDyes[i] = new();
            }
        }
        internal Item[] Inventory = new Item[59];
        internal bool[] HideAccessory = new bool[10];
        internal Item[] Armor = new Item[20];
        internal Item[] Dye = new Item[10];
        internal Item[] MiscEquips = new Item[5];
        internal Item[] MiscDyes = new Item[5];
        internal List<Mod> BlackList = new();
        internal List<Mod> WhiteList = new();
        internal int index;
        internal void QuickStack()
        {
            if(Flag.ChangeAvailable)
            {
                if(Manager.Activing.NowInventoryIndex != index)
                {
                    //记录当前索引
                    int old = Manager.Activing.NowInventoryIndex;
                    //保存当前页面
                    Manager.Activing.SaveInventory();
                    //设置到该页面
                    Manager.Activing.NowInventoryIndex = index;
                    //推送到背包
                    Manager.Activing.SendInventory();
                    //调用原版排序
                    ItemSorting.SortInventory();
                    //保存当前页面
                    Manager.Activing.SaveInventory();
                    //将页码还原
                    Manager.Activing.NowInventoryIndex = old;
                    //将原页面推送到背包
                    Manager.Activing.SendInventory();
                }
                else
                {
                    //调用排序
                    ItemSorting.SortInventory();
                    //保存页面
                    Manager.Activing.SaveInventory();
                    //推送到背包
                    Manager.Activing.SendInventory();
                }
            }
        }
        internal static void QuickStackAll()
        {
            if(Flag.ChangeAvailable)
            {
                foreach(Page p in Manager.Activing.pages)
                {
                    p.QuickStack();
                }
            }
        }
        #region
        internal bool MushroomSet => Armor[1].bodySlot == 67 && Armor[2].legSlot == 56 && Armor[0].headSlot >= 103 && Armor[0].headSlot <= 105;
        internal bool WoodenSET => (Armor[0].headSlot == 52 && Armor[1].bodySlot == 32 && Armor[2].legSlot == 31) || (Armor[0].headSlot == 53 && Armor[1].bodySlot == 33 && Armor[2].legSlot == 32) || (Armor[0].headSlot == 54 && Armor[1].bodySlot == 34 && Armor[2].legSlot == 33) || (Armor[0].headSlot == 55 && Armor[1].bodySlot == 35 && Armor[2].legSlot == 34) || (Armor[0].headSlot == 71 && Armor[1].bodySlot == 47 && Armor[2].legSlot == 43) || (Armor[0].headSlot == 166 && Armor[1].bodySlot == 173 && Armor[2].legSlot == 108) || (Armor[0].headSlot == 167 && Armor[1].bodySlot == 174 && Armor[2].legSlot == 109);
        internal bool MetalTier1Set => (Armor[0].headSlot == 1 && Armor[1].bodySlot == 1 && Armor[2].legSlot == 1) || ((Armor[0].headSlot == 72 || Armor[0].headSlot == 2) && Armor[1].bodySlot == 2 && Armor[2].legSlot == 2) || (Armor[0].headSlot == 47 && Armor[1].bodySlot == 28 && Armor[2].legSlot == 27);
        internal bool MetalTier2Set => (Armor[0].headSlot == 3 && Armor[1].bodySlot == 3 && Armor[2].legSlot == 3) || ((Armor[0].headSlot == 73 || Armor[0].headSlot == 4) && Armor[1].bodySlot == 4 && Armor[2].legSlot == 4) || (Armor[0].headSlot == 48 && Armor[1].bodySlot == 29 && Armor[2].legSlot == 28) || (Armor[0].headSlot == 49 && Armor[1].bodySlot == 30 && Armor[2].legSlot == 29);
        internal bool PlatinumSet => Armor[0].headSlot == 50 && Armor[1].bodySlot == 31 && Armor[2].legSlot == 30;
        internal bool PumpkinSet => Armor[0].headSlot == 112 && Armor[1].bodySlot == 75 && Armor[2].legSlot == 64;
        internal bool GladiatorSet => Armor[0].headSlot == 180 && Armor[1].bodySlot == 182 && Armor[2].legSlot == 122;
        internal bool NinjaSet => Armor[0].headSlot == 22 && Armor[1].bodySlot == 14 && Armor[2].legSlot == 14;
        internal bool FossilSet => Armor[0].headSlot == 188 && Armor[1].bodySlot == 189 && Armor[2].legSlot == 129;
        internal bool BoneSet => (Armor[0].headSlot == 75 || Armor[0].headSlot == 7) && Armor[1].bodySlot == 7 && Armor[2].legSlot == 7;
        internal bool BeetleAttackSet => Armor[0].headSlot == 157 && Armor[1].bodySlot == 105 && Armor[2].legSlot == 98;
        internal bool BeetleDefenseSet => Armor[0].headSlot == 157 && Armor[1].bodySlot == 106 && Armor[2].legSlot == 98;
        internal bool BeetleSet => BeetleAttackSet || BeetleDefenseSet;
        internal bool WizardSet => Armor[0].headSlot == 14 && ((Armor[1].bodySlot >= 58 && Armor[1].bodySlot <= 63) || Armor[1].bodySlot == 167 || Armor[1].bodySlot == 213);
        internal bool MagicHatSet => Armor[0].headSlot == 159 && ((Armor[1].bodySlot >= 58 && Armor[1].bodySlot <= 63) || Armor[1].bodySlot == 167 || Armor[1].bodySlot == 213);
        internal bool ShadowScaleSet => (Armor[0].headSlot == 5 || Armor[0].headSlot == 74) && (Armor[1].bodySlot == 5 || Armor[1].bodySlot == 48) && (Armor[2].legSlot == 5 || Armor[2].legSlot == 44);
        internal bool CrimsonSet => Armor[0].headSlot == 57 && Armor[1].bodySlot == 37 && Armor[2].legSlot == 35;
        internal bool GhostHealSet => Armor[0].headSlot == 101 && Armor[1].bodySlot == 66 && Armor[2].legSlot == 55;
        internal bool GhostHurtSet => Armor[0].headSlot == 156 && Armor[1].bodySlot == 66 && Armor[2].legSlot == 55;
        internal bool MeteorSet => Armor[0].headSlot == 6 && Armor[1].bodySlot == 6 && Armor[2].legSlot == 6;
        internal bool FrostSet => Armor[0].headSlot == 46 && Armor[1].bodySlot == 27 && Armor[2].legSlot == 26;
        internal bool JungleSet => (Armor[0].headSlot == 76 || Armor[0].headSlot == 8) && (Armor[1].bodySlot == 49 || Armor[1].bodySlot == 8) && (Armor[2].legSlot == 45 || Armor[2].legSlot == 8);
        internal bool MoltenSet => Armor[0].headSlot == 9 && Armor[1].bodySlot == 9 && Armor[2].legSlot == 9;
        internal bool MiningSet => (Armor[0].headSlot == 11 && Armor[1].bodySlot == 20 && Armor[2].legSlot == 19) || (Armor[0].headSlot == 216 && Armor[1].bodySlot == 20 && Armor[2].legSlot == 19);
        internal bool ChlorophyteSet => (Armor[0].headSlot == 80 && Armor[0].headSlot == 79) && Armor[1].bodySlot == 51 && Armor[2].legSlot == 47;
        internal bool ChlorophyteMeleeSet => Armor[0].headSlot == 78 && Armor[1].bodySlot == 51 && Armor[2].legSlot == 47;
        internal bool CactusSet => Armor[0].headSlot == 70 && Armor[1].bodySlot == 46 && Armor[2].legSlot == 42;
        internal bool TurtleSet => Armor[0].headSlot == 99 && Armor[1].bodySlot == 65 && Armor[2].legSlot == 54;
        internal bool CobaltCasterSet => Armor[0].headSlot == 29 && Armor[1].bodySlot == 17 && Armor[2].legSlot == 16;
        internal bool CobaltMeleeSet => Armor[0].headSlot == 30 && Armor[1].bodySlot == 17 && Armor[2].legSlot == 16;
        internal bool CobaltRangedSet => Armor[0].headSlot == 31 && Armor[1].bodySlot == 17 && Armor[2].legSlot == 16;
        internal bool MythrilCasterSet => Armor[0].headSlot == 32 && Armor[1].bodySlot == 18 && Armor[2].legSlot == 17;
        internal bool MythrilMeleeSet => Armor[0].headSlot == 33 && Armor[1].bodySlot == 18 && Armor[2].legSlot == 17;
        internal bool MythrilRangedSet => Armor[0].headSlot == 34 && Armor[1].bodySlot == 18 && Armor[2].legSlot == 17;
        internal bool AdamantiteCasterSet => Armor[0].headSlot == 35 && Armor[1].bodySlot == 19 && Armor[2].legSlot == 18;
        internal bool AdamantiteMeleeSet => Armor[0].headSlot == 36 && Armor[1].bodySlot == 19 && Armor[2].legSlot == 18;
        internal bool AdamantiteRangedSet => Armor[0].headSlot == 37 && Armor[1].bodySlot == 19 && Armor[2].legSlot == 18;
        internal bool PalladiumSet => Armor[1].bodySlot == 54 && Armor[2].legSlot == 49 && (Armor[0].headSlot == 83 || Armor[0].headSlot == 84 || Armor[0].headSlot == 85);
        internal bool OrichalcumSet => Armor[1].bodySlot == 55 && Armor[2].legSlot == 50 && (Armor[0].headSlot == 86 || Armor[0].headSlot == 87 || Armor[0].headSlot == 88);
        internal bool TitaniumSet => (Armor[0].headSlot >= 89 && Armor[0].headSlot <= 91) && Armor[1].bodySlot == 56 && Armor[2].legSlot == 51;
        internal bool HallowedSet => (Armor[1].bodySlot == 24 || Armor[1].bodySlot == 229) && (Armor[2].legSlot == 23 || Armor[2].legSlot == 212) && (Armor[0].headSlot == 42 || Armor[0].headSlot == 41 || Armor[0].headSlot == 43 || Armor[0].headSlot == 257 || Armor[0].headSlot == 256 || Armor[0].headSlot == 255);
        internal bool HallowedSummonerSet => (Armor[1].bodySlot == 24 || Armor[1].bodySlot == 229) && (Armor[2].legSlot == 23 || Armor[2].legSlot == 212) && (Armor[0].headSlot == 254 || Armor[0].headSlot == 258);
        internal bool CrystalNinjaSet => Armor[0].headSlot == 261 && Armor[1].bodySlot == 230 && Armor[2].legSlot == 213;
        internal bool TikiSet => Armor[0].headSlot == 82 && Armor[1].bodySlot == 53 && Armor[2].legSlot == 48;
        internal bool SpookySet => Armor[0].headSlot == 134 && Armor[1].bodySlot == 95 && Armor[2].legSlot == 79;
        internal bool BeeSet => Armor[0].headSlot == 160 && Armor[1].bodySlot == 168 && Armor[2].legSlot == 103;
        internal bool SpiderSet => Armor[0].headSlot == 162 && Armor[1].bodySlot == 170 && Armor[2].legSlot == 105;
        internal bool SolarSet => Armor[0].headSlot == 171 && Armor[1].bodySlot == 177 && Armor[2].legSlot == 112;
        internal bool VortexSet => Armor[0].headSlot == 169 && Armor[1].bodySlot == 175 && Armor[2].legSlot == 110;
        internal bool NebulaSet => Armor[0].headSlot == 170 && Armor[1].bodySlot == 176 && Armor[2].legSlot == 111;
        internal bool StarDustSet => Armor[0].headSlot == 189 && Armor[1].bodySlot == 190 && Armor[2].legSlot == 130;
        internal bool ForbiddenSet => Armor[0].headSlot == 200 && Armor[1].bodySlot == 198 && Armor[2].legSlot == 142;
        internal bool SquireTier1Set => Armor[0].headSlot == 204 && Armor[1].bodySlot == 201 && Armor[2].legSlot == 145;
        internal bool SquireTier2Set => Armor[0].headSlot == 210 && Armor[1].bodySlot == 204 && Armor[2].legSlot == 152;
        internal bool ApprenticeTier1Set => Armor[0].headSlot == 203 && Armor[1].bodySlot == 200 && Armor[2].legSlot == 144;
        internal bool ApprenticeTier2Set => Armor[0].headSlot == 211 && Armor[1].bodySlot == 205 && Armor[2].legSlot == 153;
        internal bool HuntressTier1Set => Armor[0].headSlot == 205 && Armor[1].bodySlot == 202 && (Armor[2].legSlot == 147 || Armor[2].legSlot == 146);
        internal bool HuntressTier2Set => Armor[0].headSlot == 212 && Armor[1].bodySlot == 206 && (Armor[2].legSlot == 154 || Armor[2].legSlot == 155);
        internal bool MonkTier1Set => Armor[0].headSlot == 206 && Armor[1].bodySlot == 203 && Armor[2].legSlot == 148;
        internal bool MonkTier2Set => Armor[0].headSlot == 213 && Armor[1].bodySlot == 207 && Armor[2].legSlot == 156;
        internal bool ObsidianOutlawSet => Armor[0].headSlot == 185 && Armor[1].bodySlot == 187 && Armor[2].legSlot == 127;
        #endregion
    }
}
