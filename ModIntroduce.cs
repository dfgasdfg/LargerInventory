using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LargerInventory
{
    internal class ModIntroduce:ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ModIntroduce");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "模组介绍");
            Tooltip.SetDefault("This mod expands the backpack space\n" +
                "You can set detailed details on the mod settings page of the game's initial page\n" +
                "When the backpack is open and not in a chest or NPC dialogue will show part of the UI\n" +
                "You can set the function keys in the in-game settings-controls to use the quick page turning function\n" +
                "The following content is only for Chinese players\n" +
                "Since Terraria does not currently support Chinese translation of hotkeys, the following is an introduction to hotkey functions\n" +
                "Reversedirection: The page turning direction of other hotkeys is forward when pressed, otherwise it is backward\n" +
                "Pageinventory: Scrolls the current backpack by one page\n" +
                "Pagearmor: Turns the current armor (including the corresponding outfit, dye) by one page\n" +
                "Pageaccessory: Make the current accessory (including the corresponding outfit, dye) turn a page\n" +
                "Pagemisc: Turns the current extra equipment (including the corresponding dye) by one page\n" +
                "Favorite: When used with FavoritesX, save the current backpack, armor, accessories, and extra equipment to the X collection\n" +
                "FavoritesX: Quickly switch to the X-th favorite combination when used alone");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "本模组增加扩展了背包空间\n" +
                "你可以在游戏初始页面的模组设置页面设置详细细节\n" +
                "当背包打开且不处于箱子或NPC对话将展示部分UI\n" +
                "你可以在游戏内设置-控件中设置功能键以便于使用快速翻页功能\n" +
                "以下内容仅供中国玩家\n" +
                "由于泰拉瑞亚暂不支持热键的中文翻译，以下为热键功能介绍\n" +
                "Reversedirection:按住时其余热键翻页方向为向前，否则为向后\n" +
                "Pageinventory:使当前背包翻动一页\n" +
                "Pagearmor:使当前盔甲(包含对应时装，染料)翻动一页\n" +
                "Pageaccessory:使当前饰品(包含对应时装，染料)翻动一页\n" +
                "Pagemisc:使当前额外装备(包含对应染料)翻动一页\n" +
                "Favorite:与FavoritesX同时使用时将当前的背包，盔甲，饰品，额外装备组合收藏至第X收藏\n" +
                "FavoritesX:单独使用时快速切换到第x收藏的组合");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.scale = 0.4f;
            Item.value = 0;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCondition(NetworkText.Empty, new Predicate<Recipe>((r) => !Manager.Activing.GetItemForFindRecipes().Any(item => item.type == Type) && Config.EnableModIntroduceCraft))
                .Register();
            base.AddRecipes();
        }
    }
}
