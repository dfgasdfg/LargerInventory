using IL.Terraria;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;

namespace LargerInventory
{
    internal class Hooks
    {
        internal static void Unload()
        {
            Flag.UnloadingHooks = true;
            On.Terraria.Utilities.FileUtilities.Delete -= FileUtilities_Delete;
            On.Terraria.Player.LoadPlayer -= CatchLoadPath;
            On.Terraria.Player.SavePlayer -= CatchSavePath;
            On.Terraria.Player.UpdateArmorSets -= UpdateArmorSets_Replace;
            On.Terraria.Recipe.FindRecipes -= FindRecipes_Replace;
            On.Terraria.Main.CraftItem -= CraftItem_Replace;
            On.Terraria.Player.QuickHeal_GetItemToUse -= QuickHeal_GetItemToUse_Replace;
            On.Terraria.Player.QuickHeal -= QuickHeal_Replace;
            On.Terraria.Player.QuickMana_GetItemToUse -= QuickMana_GetItemToUse_Replace;
            On.Terraria.Player.QuickMana -= QuickMana_Replace;
            On.Terraria.Player.QuickBuff -= QuickBuff_Replace;
            On.Terraria.Projectile.ghostHeal -= Projectile_ghostHeal_Replace;
            On.Terraria.Projectile.ghostHurt -= Projectile_ghostHurt_Replace;
            On.Terraria.Player.UpdateEquips -= UpdateEquips_Replace;
            //On.Terraria.Player.GrabItems -= GrabItems_Replace;
            On.Terraria.Player.GetItem += Player_GetItem;
            On.Terraria.Player.PickAmmo -= PickAmmo_Replace;
            On.Terraria.Player.BuyItem -= Player_BuyItem_Replace;
            On.Terraria.GameContent.UI.CustomCurrencyManager.BuyItem -= CustomCurrencyManager_BuyItem_Replace;
            On.Terraria.Player.SellItem -= Player_SellItem_Replace;
            On.Terraria.Player.DoCoins -= Player_DoCoins_Replace;
            On.Terraria.Player.HasAmmo -= Player_HasAmmo_Replace;
            Flag.UnloadingHooks = false;
        }
        internal static void Load()
        {
            Unload();
            Flag.LoadingHooks = true;
            On.Terraria.Utilities.FileUtilities.Delete += FileUtilities_Delete;
            On.Terraria.Player.LoadPlayer += CatchLoadPath;
            On.Terraria.Player.SavePlayer += CatchSavePath;
            On.Terraria.Player.UpdateArmorSets += UpdateArmorSets_Replace;
            On.Terraria.Recipe.FindRecipes += FindRecipes_Replace;
            On.Terraria.Main.CraftItem += CraftItem_Replace;
            On.Terraria.Player.QuickHeal_GetItemToUse += QuickHeal_GetItemToUse_Replace;
            On.Terraria.Player.QuickHeal += QuickHeal_Replace;
            On.Terraria.Player.QuickMana_GetItemToUse += QuickMana_GetItemToUse_Replace;
            On.Terraria.Player.QuickMana += QuickMana_Replace;
            On.Terraria.Player.QuickBuff += QuickBuff_Replace;
            On.Terraria.Projectile.ghostHeal += Projectile_ghostHeal_Replace;
            On.Terraria.Projectile.ghostHurt += Projectile_ghostHurt_Replace;
            On.Terraria.Player.UpdateEquips += UpdateEquips_Replace;
            //On.Terraria.Player.GrabItems += GrabItems_Replace;
            On.Terraria.Player.GetItem += Player_GetItem;
            On.Terraria.Player.PickAmmo += PickAmmo_Replace;
            On.Terraria.Player.BuyItem += Player_BuyItem_Replace;
            On.Terraria.GameContent.UI.CustomCurrencyManager.BuyItem += CustomCurrencyManager_BuyItem_Replace;
            On.Terraria.Player.SellItem += Player_SellItem_Replace;
            On.Terraria.Player.DoCoins += Player_DoCoins_Replace;
            On.Terraria.Player.HasAmmo += Player_HasAmmo_Replace;
            Flag.LoadingHooks = false;
        }
        private static bool Player_HasAmmo_Replace(On.Terraria.Player.orig_HasAmmo orig, Terraria.Player self, Terraria.Item sItem, bool canUse)
        {
            if (Flag.ChangeAvailable && Config.EnableAmmoPickEnhancement && Config.PickAmmoFromSpeicalChest)
            {
                if (sItem.useAmmo > 0)
                {
                    foreach (Page page in Manager.Activing.pages)
                    {
                        foreach (Terraria.Item item in page.Inventory[0..58])
                        {
                            if(item.ammo==sItem.useAmmo&&item.stack>0)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                return canUse;
            }
            else
            {
                return orig.Invoke(self, sItem, canUse);
            }
        }
        private static void Player_DoCoins_Replace(On.Terraria.Player.orig_DoCoins orig, Terraria.Player self, int i)
        {
            if (Flag.ChangeAvailable && Config.EnableCrosspageUseCoinsOrTokens)
            {
                PlayerManager.DoCoins(self.inventory[i]);
            }
            else
            {
                orig.Invoke(self, i);
            }
        }
        private static bool Player_SellItem_Replace(On.Terraria.Player.orig_SellItem orig, Terraria.Player self, Terraria.Item item, int stack)
        {
            if(Flag.ChangeAvailable&&Config.EnableCrosspageUseCoinsOrTokens)
            {
                try
                {
                    return PlayerManager.SellItem(self,item,stack);
                }
                catch(Exception e)
                {
                    Config.EnableCrosspageUseCoinsOrTokens = false;
                    Flag.CatchException(Flag.ExceptionFlag.SellItem, e);
                    On.Terraria.GameContent.UI.CustomCurrencyManager.BuyItem -= CustomCurrencyManager_BuyItem_Replace;
                    On.Terraria.Player.BuyItem -= Player_BuyItem_Replace;
                    On.Terraria.Player.SellItem -= Player_SellItem_Replace;
                    On.Terraria.Player.DoCoins -= Player_DoCoins_Replace;
                    return orig.Invoke(self,item,stack);
                }
            }
            else
            {
                return orig.Invoke(self,item,stack);
            }
        }
        private static bool CustomCurrencyManager_BuyItem_Replace(On.Terraria.GameContent.UI.CustomCurrencyManager.orig_BuyItem orig, Terraria.Player player, int price, int currencyIndex)
        {
            if (Flag.ChangeAvailable && Config.EnableCrosspageUseCoinsOrTokens)
            {
                try
                {
                    return PlayerManager.Player_BuyItem_Custom(player, price, currencyIndex);
                }
                catch (Exception ex)
                {
                    Config.EnableCrosspageUseCoinsOrTokens = false;
                    Flag.CatchException(Flag.ExceptionFlag.BuyItem, ex);
                    On.Terraria.GameContent.UI.CustomCurrencyManager.BuyItem -= CustomCurrencyManager_BuyItem_Replace;
                    On.Terraria.Player.BuyItem -= Player_BuyItem_Replace;
                    On.Terraria.Player.SellItem -= Player_SellItem_Replace;
                    On.Terraria.Player.DoCoins -= Player_DoCoins_Replace;
                    return orig.Invoke(player, price, currencyIndex);
                }
            }
            else
            {
                return orig.Invoke(player, price, currencyIndex);
            }
        }
        private static bool Player_BuyItem_Replace(On.Terraria.Player.orig_BuyItem orig, Terraria.Player self, int price, int customCurrency)
        {
            if(Flag.ChangeAvailable&&Config.EnableCrosspageUseCoinsOrTokens)
            {
                try
                {
                    return PlayerManager.Player_BuyItem(self, price, customCurrency);
                }
                catch(Exception ex)
                {
                    Config.EnableCrosspageUseCoinsOrTokens=false;
                    Flag.CatchException(Flag.ExceptionFlag.BuyItem, ex);
                    On.Terraria.GameContent.UI.CustomCurrencyManager.BuyItem -= CustomCurrencyManager_BuyItem_Replace;
                    On.Terraria.Player.BuyItem -= Player_BuyItem_Replace;
                    On.Terraria.Player.SellItem -= Player_SellItem_Replace;
                    On.Terraria.Player.DoCoins -= Player_DoCoins_Replace;
                    return orig.Invoke(self, price, customCurrency);
                }
            }
            else
            {
                return orig.Invoke(self, price, customCurrency);
            }
        }
        private static void PickAmmo_Replace(On.Terraria.Player.orig_PickAmmo orig, Terraria.Player self, Terraria.Item sItem, ref int projToShoot, ref float speed, ref bool canShoot, ref int Damage, ref float KnockBack, out int usedAmmoItemId, bool dontConsume)
        {
            if (Flag.ChangeAvailable && Config.EnableAmmoPickEnhancement)
            {
                try
                {
                    PlayerManager.PickAmmo(self, sItem, ref projToShoot, ref speed, ref canShoot, ref Damage, ref KnockBack, out usedAmmoItemId, dontConsume);
                }
                catch (Exception ex)
                {
                    Flag.CatchException(Flag.ExceptionFlag.PickAmmo, ex);
                    Config.EnableAmmoPickEnhancement = false;
                    orig.Invoke(self, sItem, ref projToShoot, ref speed, ref canShoot, ref Damage, ref KnockBack, out usedAmmoItemId, dontConsume);
                }
            }
            else
            {
                orig.Invoke(self, sItem, ref projToShoot, ref speed, ref canShoot, ref Damage, ref KnockBack, out usedAmmoItemId, dontConsume);
            }
        }
        //private static void GrabItems_Replace(On.Terraria.Player.orig_GrabItems orig, Terraria.Player self, int i)
        //{
        //    orig.Invoke(self, i);
        //    if (Flag.ChangeAvailable && Config.EnableCrossPagePickup)
        //    {
        //        try
        //        {
        //            Dictionary<int, (List<Terraria.Item>, List<Terraria.Item>, List<Terraria.Item>)> items = new();
        //            foreach (Page p in Manager.Activing.pages)
        //            {
        //                for (int index = 0; index < 58; index++)
        //                {
        //                    if (items.ContainsKey(p.Inventory[index].netID))
        //                    {
        //                        if (index < 50)
        //                        {
        //                            items[p.Inventory[index].netID].Item1.Add(p.Inventory[index]);
        //                        }
        //                        else if (index < 54)
        //                        {
        //                            items[p.Inventory[index].netID].Item2.Add(p.Inventory[index]);
        //                        }
        //                        else
        //                        {
        //                            items[p.Inventory[index].netID].Item3.Add(p.Inventory[index]);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (index < 50)
        //                        {
        //                            items.Add(p.Inventory[index].netID, (new() { p.Inventory[index] }, new(), new()));
        //                        }
        //                        else if (index < 54)
        //                        {
        //                            items.Add(p.Inventory[index].netID, (new(), new() { p.Inventory[index] }, new()));
        //                        }
        //                        else
        //                        {
        //                            items.Add(p.Inventory[index].netID, (new(), new(), new() { p.Inventory[index] }));
        //                        }
        //                    }
        //                }
        //            }
        //            for (int j = 0; j < Terraria.Main.item.Length; j++)
        //            {
        //                Terraria.Item item = Terraria.Main.item[j];
        //                if (!(!item.active || item.noGrabDelay != 0 || item.playerIndexTheItemIsReservedFor != i || !self.CanAcceptItemIntoInventory(item)))
        //                {
        //                    if (item.stack > 0 && ItemLoader.CanPickup(item, self) && item.Hitbox.Intersects(self.Hitbox))
        //                    {
        //                        if (!ItemLoader.OnPickup(item, self))
        //                        {
        //                            Terraria.Main.item[j] = new();
        //                            if (Terraria.Main.netMode == NetmodeID.MultiplayerClient)
        //                            {
        //                                Terraria.NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
        //                            }
        //                            Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Grab, item.position);
        //                        }
        //                        else
        //                        {
        //                            self.PickupItem(i, j, item);
        //                        }
        //                        if (item.IsAir)
        //                        {
        //                            continue;
        //                        }
        //                        List<Terraria.Item> list = new();
        //                        if (items.TryGetValue(item.netID, out var sametype))
        //                        {
        //                            if (item.IsACoin)
        //                            {
        //                                list.AddRange(sametype.Item2);
        //                            }
        //                            if (item.ammo > 0 && !item.notAmmo)
        //                            {
        //                                list.AddRange(sametype.Item3);
        //                            }
        //                            list.AddRange(sametype.Item1);
        //                            list.Sort(new ItemStackComparer());
        //                            for (int k = 0; k < list.Count; k++)
        //                            {
        //                                if (!(list[k].ModItem?.CanStack(item) ?? true) || item.prefix != list[k].prefix)
        //                                {
        //                                    continue;
        //                                }
        //                                int stackcost = (item.stack + list[k].stack) > list[k].maxStack ? (list[k].maxStack - list[k].stack) : item.stack;
        //                                item.stack -= stackcost;
        //                                list[k].stack += stackcost;
        //                                if (stackcost > 0)
        //                                {
        //                                    Terraria.PopupText.NewText(Terraria.PopupTextContext.RegularItemPickup, item, stackcost);
        //                                    Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Grab, item.position);
        //                                }
        //                                if (item.IsAir)
        //                                {
        //                                    Terraria.Main.item[j] = new();
        //                                    if (Terraria.Main.netMode == NetmodeID.MultiplayerClient)
        //                                    {
        //                                        Terraria.NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
        //                                    }
        //                                    goto nextloop;
        //                                }
        //                            }
        //                        }
        //                        if(items.TryGetValue(0,out var voidsolt))
        //                        {
        //                            list.Clear();
        //                            if (item.IsACoin)
        //                            {
        //                                list.AddRange(voidsolt.Item2);
        //                            }
        //                            if (item.ammo > 0 && !item.notAmmo)
        //                            {
        //                                list.AddRange(voidsolt.Item3);
        //                            }
        //                            list.AddRange(voidsolt.Item1);
        //                            for (int k = 0; k < list.Count; k++)
        //                            {
        //                                list[k].SetDefaults(item.type);
        //                                int stackcost = Math.Min(item.stack, item.maxStack);
        //                                item.stack -= stackcost;
        //                                list[k].stack = stackcost;
        //                                if (stackcost > 0)
        //                                {
        //                                    Terraria.PopupText.NewText(Terraria.PopupTextContext.RegularItemPickup, item, stackcost);
        //                                    Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Grab, item.position);
        //                                }
        //                                if (item.IsAir)
        //                                {
        //                                    Terraria.Main.item[j] = new();
        //                                    if (Terraria.Main.netMode == NetmodeID.MultiplayerClient)
        //                                    {
        //                                        Terraria.NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
        //                                    }
        //                                    goto nextloop;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            nextloop:;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Flag.CatchException(Flag.ExceptionFlag.GrabItems, e);
        //            Config.EnableCrossPagePickup = false;
        //        }
        //    }
        //}
        private static void UpdateEquips_Replace(On.Terraria.Player.orig_UpdateEquips orig, Terraria.Player self, int i)
        {
            if (Flag.ChangeAvailable)
            {
                try
                {
                    PlayerManager.UpdateEquips(self);
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.Equips, e);
                    Config.EnableInventoryEnhancement = false;
                    Config.EnableArmorEnhancement = false;
                    Config.EnableAccessoryEnhancement = false;
                    Config.EnableDecorationEnhancement = false;
                }
            }
            else
            {
                orig.Invoke(self, i);
            }
        }
        private static void Projectile_ghostHurt_Replace(On.Terraria.Projectile.orig_ghostHurt orig, Terraria.Projectile self, int dmg, Microsoft.Xna.Framework.Vector2 Position, Terraria.Entity victim)
        {
            if (Flag.ChangeAvailable && PlayerManager.GhostHurt > 1)
            {
                try
                {
                    if (self.DamageType != DamageClass.Magic || self.damage <= 0)
                    {
                        return;
                    }
                    int num = self.damage;
                    if (dmg <= 1)
                    {
                        return;
                    }
                    int num2 = 1500;
                    if (Terraria.Main.LocalPlayer.ghostDmg > num2)
                    {
                        return;
                    }
                    Terraria.Main.LocalPlayer.ghostDmg += num;
                    int[] array = new int[200];
                    int num3 = 0;
                    int num4 = 0;
                    for (int i = 0; i < 200; i++)
                    {
                        if (Terraria.Main.npc[i].CanBeChasedBy(self, false))
                        {
                            float num5 = Math.Abs(Terraria.Main.npc[i].position.X + Terraria.Main.npc[i].width / 2 - self.position.X + self.width / 2) + Math.Abs(Terraria.Main.npc[i].position.Y + Terraria.Main.npc[i].height / 2 - self.position.Y + self.height / 2);
                            if (num5 < 800f)
                            {
                                if (Terraria.Collision.CanHit(self.position, 1, 1, Terraria.Main.npc[i].position, Terraria.Main.npc[i].width, Terraria.Main.npc[i].height) && num5 > 50f)
                                {
                                    array[num4] = i;
                                    num4++;
                                }
                                else if (num4 == 0)
                                {
                                    array[num3] = i;
                                    num3++;
                                }
                            }
                        }
                    }
                    if (num3 != 0 || num4 != 0)
                    {
                        int num6 = (num4 <= 0) ? array[Terraria.Main.rand.Next(num3)] : array[Terraria.Main.rand.Next(num4)];
                        float num7 = Terraria.Main.rand.Next(-100, 101);
                        float num8 = Terraria.Main.rand.Next(-100, 101);
                        float num9 = (float)Math.Sqrt((double)(num7 * num7 + num8 * num8));
                        num9 = 4f / num9;
                        num7 *= num9;
                        num8 *= num9;
                        Terraria.Projectile.NewProjectile(self.GetSource_OnHit(victim), Position.X, Position.Y, num7, num8, 356, num * PlayerManager.GhostHurt, 0f, self.owner, num6, 0f);
                    }
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.GhostHeal, e);
                    On.Terraria.Projectile.ghostHurt -= Projectile_ghostHurt_Replace;
                }
            }
        }
        private static void Projectile_ghostHeal_Replace(On.Terraria.Projectile.orig_ghostHeal orig, Terraria.Projectile self, int dmg, Microsoft.Xna.Framework.Vector2 Position, Terraria.Entity victim)
        {
            if (Flag.ChangeAvailable && PlayerManager.GhostHeal > 1)
            {
                try
                {
                    float num = 0.2f;
                    num -= self.numHits * 0.05f;
                    if (num <= 0f)
                    {
                        return;
                    }
                    float num2 = dmg * num;
                    if ((int)num2 <= 0 || Terraria.Main.player[Terraria.Main.myPlayer].lifeSteal <= 0f)
                    {
                        return;
                    }
                    Terraria.Main.player[Terraria.Main.myPlayer].lifeSteal -= num2;
                    if (self.DamageType != DamageClass.Magic)
                    {
                        return;
                    }
                    float num3 = 0f;
                    int num4 = self.owner;
                    for (int i = 0; i < 255; i++)
                    {
                        if (Terraria.Main.player[i].active &&
                            !Terraria.Main.player[i].dead &&
                            ((!Terraria.Main.player[self.owner].hostile && !Terraria.Main.player[i].hostile) ||
                            Terraria.Main.player[self.owner].team == Terraria.Main.player[i].team) &&
                            Math.Abs(Terraria.Main.player[i].position.X + (float)(Terraria.Main.player[i].width / 2) - self.position.X + (float)(self.width / 2)) + Math.Abs(Terraria.Main.player[i].position.Y + (float)(Terraria.Main.player[i].height / 2) - self.position.Y + self.height / 2) < 1200f && (float)(Terraria.Main.player[i].statLifeMax2 - Terraria.Main.player[i].statLife) > num3)
                        {
                            num3 = (float)(Terraria.Main.player[i].statLifeMax2 - Terraria.Main.player[i].statLife);
                            num4 = i;
                        }
                    }
                    Terraria.Projectile.NewProjectile(self.GetSource_OnHit(victim), Position.X, Position.Y, 0f, 0f, 298, 0, 0f, self.owner, num4, num2 * PlayerManager.GhostHeal);
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.GhostHurt, e);
                    On.Terraria.Projectile.ghostHeal -= Projectile_ghostHeal_Replace;
                }
            }
        }
        private static void QuickBuff_Replace(On.Terraria.Player.orig_QuickBuff orig, Terraria.Player self)
        {
            if (Flag.ChangeAvailable && Config.EnableQuickUse)
            {
                try
                {
                    PlayerManager.QuickBuff(self);
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.QuickBuffs, e);
                    Config.EnableQuickUse = false;
                }
            }
            else
            {
                orig.Invoke(self);
            }
        }
        private static void QuickMana_Replace(On.Terraria.Player.orig_QuickMana orig, Terraria.Player self)
        {
            if (Flag.ChangeAvailable && Config.EnableQuickUse)
            {
                try
                {
                    PlayerManager.QuickMana(self);
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.QuickMana, e);
                    Config.EnableQuickUse = false;
                }
            }
            else
            {
                orig.Invoke(self);
            }
        }
        private static void QuickHeal_Replace(On.Terraria.Player.orig_QuickHeal orig, Terraria.Player self)
        {
            if (Flag.ChangeAvailable && Config.EnableQuickUse)
            {
                try
                {
                    PlayerManager.QuickHeal(self);
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.QuickHeal, e);
                    Config.EnableQuickUse = false;
                }
            }
            else
            {
                orig.Invoke(self);
            }
        }
        private static Terraria.Item QuickMana_GetItemToUse_Replace(On.Terraria.Player.orig_QuickMana_GetItemToUse orig, Terraria.Player self)
        {
            if (Flag.ChangeAvailable && Config.EnableQuickUse)
            {
                try
                {
                    Terraria.Item item = PlayerManager.QuickMana_GetItemToUse(self);
                    return item;
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.QuickMana, e);
                    Config.EnableQuickUse = false;
                    return orig.Invoke(self);
                }
            }
            else
            {
                return orig.Invoke(self);
            }
        }
        private static Terraria.Item QuickHeal_GetItemToUse_Replace(On.Terraria.Player.orig_QuickHeal_GetItemToUse orig, Terraria.Player self)
        {
            if (Flag.ChangeAvailable && Config.EnableQuickUse)
            {
                try
                {
                    Terraria.Item item = PlayerManager.QuickHeal_GetItemToUse(self);
                    return item;
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.QuickHeal, e);
                    Config.EnableQuickUse = false;
                    return orig.Invoke(self);
                }
            }
            else
            {
                return orig.Invoke(self);
            }
        }
        private static void CraftItem_Replace(On.Terraria.Main.orig_CraftItem orig, Terraria.Recipe r)
        {
            if (Flag.ChangeAvailable && Config.EnableRecipeEnhancement)
            {
                try
                {
                    RecipeManager.CraftItem(r);
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.CraftItem, e);
                    Config.EnableRecipeEnhancement = false;
                }
            }
            else
            {
                orig.Invoke(r);
            }
        }
        private static void FindRecipes_Replace(On.Terraria.Recipe.orig_FindRecipes orig, bool canDelayCheck)
        {
            if (canDelayCheck || !Flag.ChangeAvailable)
            {
                orig.Invoke(canDelayCheck);
            }
            else
            {
                if (Config.EnableRecipeEnhancement)
                {
                    try
                    {
                        RecipeManager.FindRecipes();
                    }
                    catch (Exception e)
                    {
                        Flag.CatchException(Flag.ExceptionFlag.FindRecipe, e);
                        Config.EnableRecipeEnhancement = false;
                    }
                }
                else
                {
                    orig.Invoke(canDelayCheck);
                }
            }
        }
        private static void UpdateArmorSets_Replace(On.Terraria.Player.orig_UpdateArmorSets orig, Terraria.Player self, int i)
        {
            if (Flag.ChangeAvailable && Config.EnableArmorEnhancement)
            {
                try
                {
                    PlayerManager.UpdateArmorSetBonus(self);
                }
                catch (Exception e)
                {
                    Flag.CatchException(Flag.ExceptionFlag.UpdateArmorSet, e);
                    Config.EnableArmorEnhancement = false;
                }
            }
            else
            {
                orig.Invoke(self, i);
            }
        }
        private static void CatchSavePath(On.Terraria.Player.orig_SavePlayer orig, Terraria.IO.PlayerFileData playerFile, bool skipMapSave)
        {
            Flag.SavingFile = playerFile;
            orig.Invoke(playerFile, skipMapSave);
        }
        private static Terraria.IO.PlayerFileData CatchLoadPath(On.Terraria.Player.orig_LoadPlayer orig, string playerPath, bool cloudSave)
        {
            Flag.LoadingPath = playerPath;
            return orig.Invoke(playerPath, cloudSave);
        }
        private static void FileUtilities_Delete(On.Terraria.Utilities.FileUtilities.orig_Delete orig, string path, bool cloud, bool forceDeleteFile)
        {
            if (path.Contains(".plr"))
            {
                string accompany = Path.ChangeExtension(path, ".lis");
                if (File.Exists(accompany))
                {
                    try
                    {
                        File.Delete(accompany);
                    }
                    catch
                    {
                        LargerInventory.Instance.Logger.Warn("Try Delete " + accompany + " when player save delete is fail");
                    }
                }
                accompany = Path.ChangeExtension(path, ".lisb");
                if (File.Exists(accompany))
                {
                    try
                    {
                        File.Delete(accompany);
                    }
                    catch
                    {
                        LargerInventory.Instance.Logger.Warn("Try Delete " + accompany + " when player save delete is fail");
                    }
                }
            }
            orig.Invoke(path, cloud, forceDeleteFile);
        }


        public static bool TryStackItem(Terraria.Item target, Terraria.Item pickItem, out bool HasSpaceLeft)
        {
            if (!target.IsSameAs(pickItem))
            {
                if (target.IsAir || target.stack < target.maxStack) {
                    HasSpaceLeft = true;
                }
                else HasSpaceLeft = false;
                return false;
            }
            int space = target.maxStack - target.stack;
            if (space > 0)
            {
                if (pickItem.IsACoin) 
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.CoinPickup, pickItem.position);
                }
                else
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Grab, pickItem.position);
                }
                Terraria.PopupText.NewText(Terraria.PopupTextContext.RegularItemPickup, pickItem, Math.Min(space, pickItem.stack), noStack: false, Terraria.GetItemSettings.PickupItemFromWorld.LongText);
            }
            if (space > pickItem.stack)
            {
                target.stack += pickItem.stack;
                pickItem.stack = 0;
                HasSpaceLeft = true;
            }
            else if (space == pickItem.stack)
            {
                target.stack += pickItem.stack;
                pickItem.stack = 0;
                HasSpaceLeft = false;
            }
            else
            {
                target.stack = target.maxStack;
                pickItem.stack -= space;
                HasSpaceLeft = false;
            }
            if (target.IsACoin)
            {
                PlayerManager.DoCoins(target);
                if (target.IsAir || target.stack < target.maxStack)
                {
                    HasSpaceLeft = true;
                }
                else
                {
                    HasSpaceLeft = false;
                }
            }

            return true;
        }
        public static int Wrap(int from, int to, int t)
        {
            while (t >= to) t -= to - from;
            while (t < from) t += to - from;
            return t;
        }
        private static Dictionary<int, (int index, int page)> possibleStackPos = new Dictionary<int, (int index, int page)>();
        private static Terraria.Item Player_GetItem(On.Terraria.Player.orig_GetItem orig, Terraria.Player self, int plr, Terraria.Item item, Terraria.GetItemSettings settings)
        {
            if (Flag.ChangeAvailable && Config.EnableCrossPagePickup)
            {
                try
                {
                    item.TryCombiningIntoNearbyItems(item.whoAmI);
                    var player = Manager.Activing;
                    player.SaveInventory();
                    if (item.noGrabDelay > 0 || (item.uniqueStack && self.HasItem(item.type)))
                    {
                        return item;
                    }

                    if (item.maxStack == 1 && !item.FitsAmmoSlot())
                    {
                        for (int j = 0; j < player.pages.Count; j++)
                        {
                            int CurrentPage = Wrap(0, player.pages.Count, player.NowInventoryIndex + j);
                            for (int k = 0; k < 50; k++)
                            {
                                if (player.pages[CurrentPage].Inventory[k].IsAir)
                                {
                                    if (player.NowInventoryIndex == CurrentPage)
                                    {
                                        self.inventory[k] = item;
                                    }
                                    else 
                                    { 
                                        player.pages[CurrentPage].Inventory[k] = item; 
                                    }
                                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Grab, item.position);
                                    Terraria.PopupText.NewText(Terraria.PopupTextContext.RegularItemPickup, item, 1, noStack: true, Terraria.GetItemSettings.PickupItemFromWorld.LongText);
                                    return new Terraria.Item();
                                }
                            }
                        }
                        return item;
                    }
                    possibleStackPos[0] = (-1, 0);
                    var func = (int from, int to, out Terraria.Item result) =>
                    {
                        for (int j = 0; j < player.pages.Count; j++)
                        {
                            int CurrentPage = Wrap(0, player.pages.Count, player.NowInventoryIndex - j - 1);
                            for (int k = from; k > to; k--)
                            {
                                var t = player.pages[CurrentPage].Inventory[k];
                                if (TryStackItem(player.pages[CurrentPage].Inventory[k], item, out bool left) && !left)
                                {
                                    possibleStackPos.Remove(t.netID);
                                }

                                if (left)
                                {
                                    possibleStackPos[t.netID] = (k, CurrentPage);
                                }

                                if (item.stack == 0)
                                {
                                    result = new Terraria.Item();
                                    return true;
                                }
                            }
                        }
                        if (item.stack != 0 && possibleStackPos[0].index != -1)
                        {
                            if (possibleStackPos[0].page == player.NowInventoryIndex)
                            {
                                self.inventory[possibleStackPos[0].index] = item;
                            }
                            else
                            {
                                player.pages[possibleStackPos[0].page].Inventory[possibleStackPos[0].index] = item;
                            }

                            result = new Terraria.Item();
                            if (item.IsACoin)
                            {
                                Terraria.Audio.SoundEngine.PlaySound(SoundID.CoinPickup, item.position);
                            }
                            else
                            {
                                Terraria.Audio.SoundEngine.PlaySound(SoundID.Grab, item.position);
                            }

                            Terraria.PopupText.NewText(Terraria.PopupTextContext.RegularItemPickup, item, item.stack, noStack: false, Terraria.GetItemSettings.PickupItemFromWorld.LongText);
                            return true;
                        }
                        result = item;
                        return false;
                    };

                    if (item.maxStack != 1 && possibleStackPos.TryGetValue(item.netID, out var value))
                    {
                        if (TryStackItem(player.pages[value.page].Inventory[value.index], item, out bool _))
                        {
                            if (item.stack == 0)
                            {
                                return new Terraria.Item();
                            }
                            else
                            {
                                possibleStackPos.Remove(item.netID);
                            }
                        }
                    }

                    if (item.FitsAmmoSlot() && func(57, 53, out Terraria.Item result))
                    {
                        return result;
                    }

                    if (item.IsACoin)
                    {
                        if (func(53, 49, out result))
                        {
                            PlayerManager.DoCoins(item);
                            return result;
                        }
                    }
                    if (func(49, -1, out result))
                    {
                        return result;
                    }

                    if (settings.CanGoIntoVoidVault &&
                        self.IsVoidVaultEnabled &&
                        MyCanVoidVaultAccept(item) &&
                        (getItem_VoidVault?.Invoke(self, plr, self.bank4.item, item, settings, item) ?? false))
                    {
                        return new Terraria.Item();
                    }

                    return item;
                }
                catch (Exception ex)
                {
                    Flag.CatchException(Flag.ExceptionFlag.GetItem, ex);
                    Config.EnableCrossPagePickup = false;
                    On.Terraria.Player.GetItem -= Player_GetItem;
                    return orig(self, plr, item, settings);
                }
            }
            else
            {
                return orig(self, plr, item, settings);
            }
        }
        //不会真的有人用了多背包后还要虚空囊吧，反射开销大没办法
        private static bool MyCanVoidVaultAccept(Terraria.Item item)
        {
            return !(item.questItem || item.type == ItemID.DD2EnergyCrystal);
        }
        private delegate bool getItem_VoidVaultType(Terraria.Player player, int plr, Terraria.Item[] inventory, Terraria.Item newItem, Terraria.GetItemSettings settings, Terraria.Item returnItem);
        private readonly static getItem_VoidVaultType getItem_VoidVault = typeof(Terraria.Player).GetMethod("GetItem_VoidVault", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).CreateDelegate<getItem_VoidVaultType>();
    }
}