using LargerInventory.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace LargerInventory
{
    [Label("$Mods.LargerInventory.ConfigLabel.TitilLabel")]
    [BackgroundColor(92,179,204)]
    internal class Config:ModConfig
    {
        /// <summary>
        /// 默认背包页数
        /// </summary>
        internal static int DefaultNumberOfBackpacks = 10;
        /// <summary>
        /// 增删页面物品追加
        /// </summary>
        internal static bool AdditionalItemsAdded = true;
        /// <summary>
        /// 跨页面合成
        /// </summary>
        internal static bool EnableRecipeEnhancement = false;
        /// <summary>
        /// 合成物品时无视收藏
        /// </summary>
        internal static bool IgnoreFavouritesWhenCraftingItems = true;
        /// <summary>
        /// 跨页面快捷使用
        /// </summary>
        internal static bool EnableQuickUse = false;
        /// <summary>
        /// 快速使用物品时无视收藏
        /// </summary>
        internal static bool IgnoreFavouritesWhenQuickUseItems = true;
        /// <summary>
        /// 跨页面背包物品更新
        /// </summary>
        internal static bool EnableInventoryEnhancement = false;
        /// <summary>
        /// 跨页面使用弹药
        /// </summary>
        internal static bool EnableAmmoPickEnhancement = false;
        /// <summary>
        /// 支取来自飞猪存钱罐等箱子的弹药
        /// </summary>
        internal static bool PickAmmoFromSpeicalChest = false;
        /// <summary>
        /// 跨页面盔甲生效
        /// </summary>
        internal static bool EnableArmorEnhancement = false;
        /// <summary>
        /// 跨页面饰品生效
        /// </summary>
        internal static bool EnableAccessoryEnhancement = false;
        /// <summary>
        /// 跨页面时装生效
        /// </summary>
        internal static bool EnableDecorationEnhancement = false;
        /// <summary>
        /// 跨页面捡拾
        /// </summary>
        internal static bool EnableCrossPagePickup = false;
        /// <summary>
        /// 跨页面购买销售
        /// </summary>
        internal static bool EnableCrosspageUseCoinsOrTokens = false;
        /// <summary>
        /// 模组介绍合成
        /// </summary>
        internal static bool EnableModIntroduceCraft = true;
        /// <summary>
        /// 滚轮翻页
        /// </summary>
        internal static bool EnableScrollPage = true;
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        [Slider]
        [Range(1,50)]
        [Increment(1)]
        [DefaultValue(10)]
        [SliderColor(235,60,122)]
        [BackgroundColor(65,179,73)]
        [Label("$Mods.LargerInventory.ConfigLabel.DefaultNumberOfBackpacks")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.DefaultNumberOfBackpacks")]
        public int defaultNumberOfBackpacks = 10;

        [DefaultValue(true)]
        [ReloadRequired]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.AdditionalItemsAdded")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.AdditionalItemsAdded")]
        public bool additionalItemsAdded;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableRecipeEnhancement")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableRecipeEnhancement")]
        public bool enableRecipeEnhancement;

        [DefaultValue(true)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.IgnoreFavouritesWhenCraftingItems")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.IgnoreFavouritesWhenCraftingItems")]
        public bool ignoreFavouritesWhenCraftingItems;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableQuickUse")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableQuickUse")]
        public bool enableQuickUse;

        [DefaultValue(true)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.IgnoreFavouritesWhenQuickUseItems")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.IgnoreFavouritesWhenQuickUseItems")]
        public bool ignoreFavouritesWhenQuickUseItems;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableInventoryEnhancement")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableInventoryEnhancement")]
        public bool enableInventoryEnhancement;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableAmmoPickEnhancement")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableAmmoPickEnhancement")]
        public bool enableAmmoPickEnhancement;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.PickAmmoFromSpeicalChest")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.PickAmmoFromSpeicalChest")]
        public bool pickAmmoFromSpeicalChest;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableArmorEnhancement")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableArmorEnhancement")]
        public bool enableArmorEnhancement;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableAccessoryEnhancement")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableAccessoryEnhancement")]
        public bool enableAccessoryEnhancement;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableDecorationEnhancement")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableDecorationEnhancement")]
        public bool enableDecorationEnhancement;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableCrossPagePickup")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableCrossPagePickup")]
        public bool enableCrossPagePickup;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableCrosspageUseCoinsOrTokens")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableCrosspageUseCoinsOrTokens")]
        public bool enableCrosspageUseCoinsOrTokens;

        [Increment(0.005f)]
        [Range(0f, 1f)]
        [DefaultValue(0.180f)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.UIHorizontalOffset")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.UIHorizontalOffset")]
        [Slider]
        public float ForWidth;

        [Increment(0.005f)]
        [Range(0f, 1f)]
        [DefaultValue(0.365f)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.UIVerticalOffset")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.UIVerticalOffset")]
        [Slider]
        public float ForHeight;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.HideUIAlways")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.HideUIAlways")]
        public bool hideuialways;

        [DefaultValue(true)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableModIntroduceCraft")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableModIntroduceCraft")]
        public bool enableModIntroduceCraft;

        [DefaultValue(true)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableScrollPage")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableScrollPage")]
        public bool enableScrollPage;
        public override void OnChanged()
        {
            DefaultNumberOfBackpacks = defaultNumberOfBackpacks;
            EnableRecipeEnhancement = enableRecipeEnhancement;
            IgnoreFavouritesWhenCraftingItems = ignoreFavouritesWhenCraftingItems;
            EnableQuickUse = enableQuickUse;
            IgnoreFavouritesWhenQuickUseItems = ignoreFavouritesWhenQuickUseItems;
            EnableInventoryEnhancement = enableInventoryEnhancement;
            EnableAmmoPickEnhancement = enableAmmoPickEnhancement;
            PickAmmoFromSpeicalChest = pickAmmoFromSpeicalChest;
            EnableArmorEnhancement = enableArmorEnhancement;
            EnableAccessoryEnhancement = enableAccessoryEnhancement;
            EnableDecorationEnhancement = enableDecorationEnhancement;
            EnableCrossPagePickup = enableCrossPagePickup;
            EnableCrosspageUseCoinsOrTokens= enableCrosspageUseCoinsOrTokens;
            EnableModIntroduceCraft = enableModIntroduceCraft;
            EnableScrollPage = enableScrollPage;
            PackageUI.Width = ForWidth;
            PackageUI.Height = ForHeight;
            PackageUI.HideAlways = hideuialways;
            base.OnChanged();
        }
    }
}
