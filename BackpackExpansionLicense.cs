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
    internal class BackpackExpansionLicense:ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BackpackExpansionLicense");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "背包扩容许可");
            Tooltip.SetDefault("Add a page of backpack pages\n" +
                "at the same time, expand the number of accessories, armor, and additional equipment pages\n" +
                "'Where did you get it from ? buddy'" +
                "Right Click to use");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),
                "在末尾增加一页背包页数\n" +
                "同时扩容饰品，盔甲，额外装备页数\n" +
                "“你从哪里搞到它的？伙计”\n" +
                "右键点击可打开\n");
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
                Manager.Activing.CreatePage();
                CombatText.NewText(player.Hitbox,
                    Color.Green,
                    Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ?
                    "扩容完成" :
                    "Expansion completed");
                Player.SavePlayer(Main.ActivePlayerFileData);
            }
            base.RightClick(player);
        }
    }
}
