using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace LargerInventory
{
    internal class BurdenReliefCertificate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BackpackExpansionLicense");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "背包减负凭证");
            Tooltip.SetDefault("Destroy the end page backpack\n" +
                "Destroy accessories, armor, extra equipment space at the same time\n" +
                "Content will fall\n" +
                "'what a pity'\n" +
                "Right Click to use");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "摧毁末尾页背包\n" +
                "同时摧毁饰品，盔甲，额外装备空间\n" +
                "内容物会掉落\n" +
                "“多么可惜”\n" +
                "右键点击可打开");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.scale = 0.4f;
            Item.value = 0;
            Item.maxStack = 10;
            base.SetDefaults();
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {

            if (!Flag.ChangeAvailable)
            {
                CombatText.NewText(player.Hitbox,
                    Color.Red,
                    Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ?
                    "当前存在某种错误，扩展背包功能不可用" :
                    "There is currently some kind of bug and the extended backpack feature is not available");
            }
            else
            {
                if (Manager.Activing.pages.Count == 1)
                {
                    CombatText.NewText(player.Hitbox,
                       Color.Orange,
                       Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ?
                       "不可摧毁最后一页背包" :
                       "The last page backpack cannot be destroyed");
                    return;
                }
                Manager.Activing.SaveAll();
                if (Manager.Activing.NowInventoryIndex == Manager.Activing.pages.Count - 1)
                {
                    Manager.Activing.NowInventoryIndex--;
                }
                if (Manager.Activing.NowArmorIndex == Manager.Activing.pages.Count - 1)
                {
                    Manager.Activing.NowArmorIndex--;
                }
                if (Manager.Activing.NowAccessoryIndex == Manager.Activing.pages.Count - 1)
                {
                    Manager.Activing.NowAccessoryIndex--;
                }
                if (Manager.Activing.NowMiscIndex == Manager.Activing.pages.Count - 1)
                {
                    Manager.Activing.NowMiscIndex--;
                }
                Manager.Activing.SendAll();
                foreach (Item item in Manager.Activing.pages[^1].Inventory)
                {
                    if (!item.IsAir)
                    {
                        Item.NewItem(null, player.Hitbox, item.type, item.stack, prefixGiven: item.prefix);
                        item.TurnToAir();
                    }
                }
                foreach (Item item in Manager.Activing.pages[^1].Armor)
                {
                    if (!item.IsAir)
                    {
                        Item.NewItem(null, player.Hitbox, item.type, item.stack, prefixGiven: item.prefix);
                        item.TurnToAir();
                    }
                }
                foreach (Item item in Manager.Activing.pages[^1].Dye)
                {
                    if (!item.IsAir)
                    {
                        Item.NewItem(null, player.Hitbox, item.type, item.stack, prefixGiven: item.prefix);
                        item.TurnToAir();
                    }
                }
                foreach (Item item in Manager.Activing.pages[^1].MiscEquips)
                {
                    if (!item.IsAir)
                    {
                        Item.NewItem(null, player.Hitbox, item.type, item.stack, prefixGiven: item.prefix);
                        item.TurnToAir();
                    }
                }
                foreach (Item item in Manager.Activing.pages[^1].MiscDyes)
                {
                    if (!item.IsAir)
                    {
                        Item.NewItem(null, player.Hitbox, item.type, item.stack, prefixGiven: item.prefix);
                        item.TurnToAir();
                    }
                }
                Page p = Manager.Activing.pages[^1];
                p.Deleted = true;
                Manager.Activing.pages.Remove(p);
                CombatText.NewText(player.Hitbox,
                    Color.Green,
                    Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ?
                    "减负完成" :
                    "Burden reduction completed");
                Player.SavePlayer(Main.ActivePlayerFileData);
            }
            base.RightClick(player);
        }
    }
}