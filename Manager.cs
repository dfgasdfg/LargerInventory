using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace LargerInventory
{
    internal static class Manager
    {
        internal static List<InventoryPlayer> IPlayers = new();
        /// <summary>
        /// 使用前必须做空检查!
        /// </summary>
        internal static InventoryPlayer Activing;
        internal static InventoryPlayer Create(Player player)
        {
            InventoryPlayer newplayer = new(player);
            newplayer.SaveAll();
            IPlayers.Add(newplayer);
            return IPlayers[^1];
        }
        internal static void RefreshNowPage()
        {
            if(Flag.ChangeAvailable)
            {
                Activing.SaveAll();
                Activing.SendAll();
                Recipe.FindRecipes();
            }
        }
        internal static bool TryRemove(Player player)
        {
            return IPlayers.RemoveAll(new Predicate<InventoryPlayer>((p) => p.player == player)) > 0;
        }
    }
}
