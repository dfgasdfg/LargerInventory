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
        /// 紫幽修改
        /// 尝试拾取时跨页面堆叠
        /// </summary>
        internal static bool EnableCrossPageStack = false;
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

        [Increment(0.005f)]
        [Range(0f, 1f)]
        [DefaultValue(0.180f)]
        [Label("$Mods.LargerInventory.ConfigLabel.UIHorizontalOffset")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.UIHorizontalOffset")]
        [Slider]
        public float ForWidth;

        [Increment(0.005f)]
        [Range(0f, 1f)]
        [DefaultValue(0.365f)]
        [Label("$Mods.LargerInventory.ConfigLabel.UIVerticalOffset")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.UIVerticalOffset")]
        [Slider]
        public float ForHeight;

        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.HideUIAlways")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.HideUIAlways")]
        public bool hideuialways;

        //改
        [DefaultValue(false)]
        [BackgroundColor(65, 179, 73)]
        [Label("$Mods.LargerInventory.ConfigLabel.EnableCrossPageStack")]
        [Tooltip("$Mods.LargerInventory.ConfigTooltip.EnableCrossPageStack")]
        public bool enableCrossPageStack;
        public override void OnChanged()
        {
            DefaultNumberOfBackpacks = defaultNumberOfBackpacks;
            EnableRecipeEnhancement = enableRecipeEnhancement;
            IgnoreFavouritesWhenCraftingItems = ignoreFavouritesWhenCraftingItems;
            EnableQuickUse = enableQuickUse;
            IgnoreFavouritesWhenQuickUseItems = ignoreFavouritesWhenQuickUseItems;
            EnableInventoryEnhancement = enableInventoryEnhancement;
            EnableAmmoPickEnhancement = enableAmmoPickEnhancement;
            EnableArmorEnhancement = enableArmorEnhancement;
            EnableAccessoryEnhancement = enableAccessoryEnhancement;
            EnableDecorationEnhancement = enableDecorationEnhancement;
            EnableCrossPagePickup = enableCrossPagePickup;
            PackageUI.Width = ForWidth;
            PackageUI.Height = ForHeight;
            PackageUI.HideAlways = hideuialways;
            base.OnChanged();

            //改
            EnableCrossPageStack = enableCrossPageStack;
        }
    }
}
