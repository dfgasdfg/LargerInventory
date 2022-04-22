using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace LargerInventory
{
    internal class RecipeManager
    {
        internal static void FindRecipes()
        {
            int focusing = Main.availableRecipe[Main.focusRecipe];
            float focusingY = Main.availableRecipeY[Main.focusRecipe];
            for (int i = 0; i < Recipe.maxRecipes; i++)
            {
                Main.availableRecipe[i] = 0;
            }
            Main.numAvailableRecipes = 0;
            if (Main.guideItem.type > ItemID.None && Main.guideItem.stack > 0 && Main.guideItem.Name != "")
            {
                int index = 0;
                while (index < Recipe.maxRecipes && Main.recipe[index].createItem.type != ItemID.None)
                {
                    for (int j = 0; j < Main.recipe[index].requiredItem.Count; j++)
                    {
                        if (Main.guideItem.IsSameAs(Main.recipe[index].requiredItem[j]) ||
                            UseWood(Main.recipe[index], Main.guideItem.type, Main.recipe[index].requiredItem[j].type) ||
                            UseSand(Main.recipe[index], Main.guideItem.type, Main.recipe[index].requiredItem[j].type) ||
                            UseIronBar(Main.recipe[index], Main.guideItem.type, Main.recipe[index].requiredItem[j].type) ||
                            UseFragment(Main.recipe[index], Main.guideItem.type, Main.recipe[index].requiredItem[j].type) ||
                            UsePressurePlate(Main.recipe[index], Main.guideItem.type, Main.recipe[index].requiredItem[j].type) ||
                            Main.recipe[index].AcceptedByItemGroups(Main.guideItem.type, Main.recipe[index].requiredItem[j].type))
                        {
                            Main.availableRecipe[Main.numAvailableRecipes] = index;
                            Main.numAvailableRecipes++;
                            break;
                        }
                    }
                    index++;
                }
            }
            else
            {
                Dictionary<int, int> dictionary = new();
                Item item;
                Item[] array = Manager.Activing.GetItemForFindRecipes();
                if (Main.player[Main.myPlayer].chest != -1)
                {
                    int start = array.Length;
                    Array.Resize(ref array, start + 40);
                    if (Main.player[Main.myPlayer].chest > -1)
                    {
                        array = Main.chest[Main.player[Main.myPlayer].chest].item;
                    }
                    else
                    {
                        switch (Main.player[Main.myPlayer].chest)
                        {
                            case -2:
                                {
                                    Main.player[Main.myPlayer].bank.item.CopyTo(array, start);
                                    break;
                                }
                            case -3:
                                {
                                    Main.player[Main.myPlayer].bank2.item.CopyTo(array, start);
                                    break;
                                }
                            case -4:
                                {
                                    Main.player[Main.myPlayer].bank3.item.CopyTo(array, start);
                                    break;
                                }
                            case -5:
                                {
                                    Main.player[Main.myPlayer].bank4.item.CopyTo(array, start);
                                    break;
                                }
                            default:
                                {
                                    Main.chest[Main.LocalPlayer.chest].item.CopyTo(array, start);
                                    break;
                                }
                        }
                    }
                }
                for (int i = 0; i < array.Length; i++)
                {
                    item = array[i];
                    if (item.stack > 0)
                    {
                        if (dictionary.ContainsKey(item.netID))
                        {
                            dictionary[item.netID] += item.stack;
                        }
                        else
                        {
                            dictionary.Add(item.netID, item.stack);
                        }
                    }
                }
                int index = 0;
                while (index < Recipe.maxRecipes && Main.recipe[index].createItem.type != ItemID.None)
                {
                    Recipe Checking = Main.recipe[index];
                    if (!CheckTileRequire(Checking))
                    {
                        index++;
                        continue;
                    }
                    if (!CheckCondition(Checking))
                    {
                        index++;
                        continue;
                    }
                    if (!CheckRequire(Checking, dictionary))
                    {
                        index++;
                        continue;
                    }
                    if (!RecipeLoader.RecipeAvailable(Checking))
                    {
                        index++;
                        continue;
                    }
                    Main.availableRecipe[Main.numAvailableRecipes] = index;
                    Main.numAvailableRecipes++;
                    index++;
                }
            }
            for (int i = 0; i < Main.numAvailableRecipes; i++)
            {
                if (focusing == Main.availableRecipe[i])
                {
                    Main.focusRecipe = i;
                    break;
                }
            }
            if (Main.focusRecipe >= Main.numAvailableRecipes)
            {
                Main.focusRecipe = Main.numAvailableRecipes - 1;
            }
            if (Main.focusRecipe < 0)
            {
                Main.focusRecipe = 0;
            }
            float movement = Main.availableRecipeY[Main.focusRecipe] - focusingY;
            for (int i = 0; i < Recipe.maxRecipes; i++)
            {
                Main.availableRecipeY[i] -= movement;
            }
        }
        internal static bool UseWood(Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.Wood) && RecipeGroup.recipeGroups[RecipeGroupID.Wood].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.Wood].ContainsItem(reqType);
        }
        internal static bool UseIronBar(Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.IronBar) && RecipeGroup.recipeGroups[RecipeGroupID.IronBar].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.IronBar].ContainsItem(reqType);
        }
        internal static bool UseSand(Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.Sand) && RecipeGroup.recipeGroups[RecipeGroupID.Sand].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.Sand].ContainsItem(reqType);
        }
        internal static bool UseFragment(Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.Fragment) && RecipeGroup.recipeGroups[RecipeGroupID.Fragment].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.Fragment].ContainsItem(reqType);
        }
        internal static bool UsePressurePlate(Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.PressurePlate) && RecipeGroup.recipeGroups[RecipeGroupID.PressurePlate].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.PressurePlate].ContainsItem(reqType);
        }
        internal static bool CheckTileRequire(Recipe Checking)
        {
            int index = 0;
            while (index < Checking.requiredTile.Count && Checking.requiredTile[index] > 0 && Checking.requiredTile[index] < TileLoader.TileCount)
            {
                if (!Main.LocalPlayer.adjTile[Checking.requiredTile[index]])
                {
                    return false;
                }
                index++;
            }
            return true;
        }
        internal static bool CheckCondition(Recipe Checking)
        {
            if (!(!Checking.HasCondition(Recipe.Condition.NearWater) || Main.player[Main.myPlayer].adjWater &
                            !Checking.HasCondition(Recipe.Condition.NearHoney) || Checking.HasCondition(Recipe.Condition.NearHoney) == Main.player[Main.myPlayer].adjHoney &
                            !Checking.HasCondition(Recipe.Condition.NearLava) || Checking.HasCondition(Recipe.Condition.NearLava) == Main.player[Main.myPlayer].adjLava &
                            !Checking.HasCondition(Recipe.Condition.InSnow) || Main.player[Main.myPlayer].ZoneSnow &
                            !Checking.HasCondition(Recipe.Condition.InGraveyardBiome) || Main.player[Main.myPlayer].ZoneGraveyard))
            {
                return false;
            }
            foreach (Recipe.Condition condition in Checking.Conditions)
            {
                if (!condition.RecipeAvailable(Checking))
                {
                    return false;
                }
            }
            return true;
        }
        internal static bool CheckRequire(Recipe Checking, Dictionary<int, int> Dictionary)
        {
            int RequireCount;
            //int key;
            foreach (Item RequireItem in Checking.requiredItem)
            {
                RequireCount = RequireItem.stack;
                if (RequireCount <= 0)
                {
                    continue;
                }
                if (Dictionary.ContainsKey(RequireItem.netID))
                {
                    RequireCount -= Dictionary[RequireItem.netID];
                    if (RequireCount <= 0)
                    {
                        continue;
                    }
                }
                using (var keys = Dictionary.Keys.GetEnumerator())
                {
                    while (keys.MoveNext())
                    {
                        if (Checking.AcceptedByItemGroups(keys.Current, RequireItem.netID))
                        {
                            RequireCount -= Dictionary[keys.Current];
                            if (RequireCount <= 0)
                            {
                                break;
                            }
                        }
                    }
                }
                if (RequireCount > 0)
                {
                    return false;
                }
            }
            return true;
        }
        internal static void CraftItem(Recipe r)
        {
            if (Create(r))
            {
                int nowstack = Main.mouseItem.stack;
                Main.mouseItem = r.createItem.Clone();
                Main.mouseItem.stack += nowstack;
                if (nowstack <= 0)
                {
                    Main.mouseItem.Prefix(-1);
                }
                Main.mouseItem.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Main.mouseItem.width / 2);
                Main.mouseItem.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Main.mouseItem.height / 2);
                PopupText.NewText(PopupTextContext.ItemCraft, Main.mouseItem, r.createItem.stack, false, false);
                if (Main.mouseItem.type > ItemID.None || r.createItem.type > ItemID.None)
                {
                    ItemLoader.OnCreate(Main.mouseItem, new RecipeCreationContext
                    {
                        recipe = r
                    });
                    RecipeLoader.OnCraft(Main.mouseItem, r);
                    SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
                }
                AchievementsHelper.NotifyItemCraft(r);
                AchievementsHelper.NotifyItemPickup(Main.player[Main.myPlayer], r.createItem);
                Recipe.FindRecipes(false);
            }
        }
        internal static bool Create(Recipe r)
        {
            (int, int)[] cost = new (int, int)[r.requiredItem.Count];
            for (int i = 0; i < cost.Length; i++)
            {
                cost[i] = (r.requiredItem[i].type, r.requiredItem[i].stack);
            }
            if (Main.LocalPlayer.alchemyTable)
            {
                for (int i = 0; i < cost.Length; i++)
                {
                    for (int j = 0; j < cost[i].Item2; j++)
                    {
                        cost[i].Item2 -= Main.rand.Next(3) == 0 ? 1 : 0;
                    }
                    RecipeLoader.ConsumeItem(r, cost[i].Item1, ref cost[i].Item2);
                }
            }
            Dictionary<Item, int> waitcost = new();
            Item[] array = Manager.Activing.GetItemForFindRecipes();
            if (Main.LocalPlayer.chest != -1)
            {
                int start = array.Length;
                Array.Resize(ref array, start + 40);
                switch (Main.LocalPlayer.chest)
                {
                    case -2:
                        {
                            Main.LocalPlayer.bank.item.CopyTo(array, start);
                            break;
                        }
                    case -3:
                        {
                            Main.LocalPlayer.bank2.item.CopyTo(array, start);
                            break;
                        }
                    case -4:
                        {
                            Main.LocalPlayer.bank3.item.CopyTo(array, start);
                            break;
                        }
                    case -5:
                        {
                            Main.LocalPlayer.bank4.item.CopyTo(array, start);
                            break;
                        }
                    default:
                        {
                            Main.chest[Main.LocalPlayer.chest].item.CopyTo(array, start);
                            break;
                        }
                }
            }
            bool cancreate = true;
            for (int i = 0; i < cost.Length; i++)
            {
                if (cost[i].Item2 <= 0)
                {
                    continue;
                }
                for (int j = 0; j < array.Length; j++)
                {
                    if (!Config.IgnoreFavouritesWhenCraftingItems && array[j].favorited)
                    {
                        continue;
                    }
                    if (r.AcceptedByItemGroups(array[j].type, cost[i].Item1) || cost[i].Item1 == array[j].type)
                    {
                        int cancost = array[j].stack - (waitcost.ContainsKey(array[j]) ? waitcost[array[j]] : 0);
                        if (cancost < 0)
                        {
                            cancost = 0;
                        }
                        int used = Math.Min(cancost, cost[i].Item2);
                        if (used > 0)
                        {
                            if (waitcost.ContainsKey(array[j]))
                            {
                                waitcost[array[j]] += used;
                            }
                            else
                            {
                                waitcost.Add(array[j], used);
                            }
                        }
                        cost[i].Item2 -= used;
                        if (cost[i].Item2 < 0)
                        {
                            break;
                        }
                    }
                }
                if (cost[i].Item2 > 0)
                {
                    cancreate = false;
                    break;
                }
            }
            if (cancreate)
            {
                using (var items = waitcost.Keys.GetEnumerator())
                {
                    while (items.MoveNext())
                    {
                        items.Current.stack -= waitcost[items.Current];
                        if (items.Current.stack < 0)
                        {
                            items.Current.TurnToAir();
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
    internal static class MoreMethods
    {
        public static bool IsSameAs(this Item item, Item compareItem)
        {
            return item.netID == compareItem.netID && item.type == compareItem.type;
        }
        public static void PickupItem(this Player player, int playerIndex, int worldItemArrayIndex, Item itemToPickUp)
        {
            if (ItemID.Sets.NebulaPickup[itemToPickUp.type])
            {
                SoundEngine.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                int num = itemToPickUp.buffType;
                Main.item[itemToPickUp.whoAmI] = new();
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.NebulaLevelupRequest, -1, -1, null, playerIndex, (float)num, player.Center.X, player.Center.Y, 0, 0, 0);
                }
                else
                {
                    player.NebulaLevelup(num);
                }
            }
            if (itemToPickUp.type == ItemID.Heart || itemToPickUp.type == ItemID.CandyApple || itemToPickUp.type == ItemID.CandyCane)
            {
                SoundEngine.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                player.Heal(20);
                Main.item[itemToPickUp.whoAmI] = new();
            }
            else if (itemToPickUp.type == ItemID.Star || itemToPickUp.type == ItemID.SoulCake || itemToPickUp.type == ItemID.SugarPlum)
            {
                SoundEngine.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                player.statMana += 100;
                if (Main.myPlayer == player.whoAmI)
                {
                    player.ManaEffect(100);
                }
                if (player.statMana > player.statManaMax2)
                {
                    player.statMana = player.statManaMax2;
                }
                Main.item[itemToPickUp.whoAmI] = new();
            }
            else if (itemToPickUp.type == 4143)
            {
                SoundEngine.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                player.statMana += 50;
                if (Main.myPlayer == player.whoAmI)
                {
                    player.ManaEffect(50);
                }
                if (player.statMana > player.statManaMax2)
                {
                    player.statMana = player.statManaMax2;
                }
                Main.item[itemToPickUp.whoAmI] = new();
            }
            else
            {
                Main.item[itemToPickUp.whoAmI] = player.GetItem(playerIndex, itemToPickUp, GetItemSettings.PickupItemFromWorld);
            }
            Main.item[worldItemArrayIndex] = itemToPickUp;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, worldItemArrayIndex, 0f, 0f, 0f, 0, 0, 0);
            }
        }
        public static void Heal(this Player player,int amount)
        {
            player.statLife += amount;
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(amount, true);
            }
            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }
        }
    }
}
