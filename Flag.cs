using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.IO;

namespace LargerInventory
{
    internal static class Flag
    {
        internal static bool LoadingHooks = false;
        internal static bool UnloadingHooks = false;
        internal static string LoadingPath = string.Empty;
        internal static bool PayersLoading = false;
        internal static PlayerFileData SavingFile = null;
        internal static readonly Dictionary<Player, List<(ExceptionFlag, Exception)>> PlayerException = new();
        internal static readonly Dictionary<ExceptionFlag, List<Exception>> Exceptions = new();
        internal static bool ChangeAvailable => Manager.Activing is not null;
        internal enum ExceptionFlag
        {
            Unknow,
            Loading,
            Saveing,
            UpdateArmorSet,
            FindRecipe,
            PickAmmo,
            GrabItems,
            Equips,
            GhostHurt,
            GhostHeal,
            QuickBuffs,
            QuickHeal,
            QuickMana,
            CraftItem,
            AromorSet,
            IOException,
            NullReferenceException,
            FileLoadException,
            FileNotFoundException,

            Uncategorized
        }
        internal static void CatchExptionForPlayer(Player player, ExceptionFlag flag, Exception exception)
        {
            if (PlayerException.ContainsKey(player))
            {
                PlayerException[player].Add((flag, exception));
            }
            else
            {
                PlayerException.Add(player, new List<(ExceptionFlag, Exception)>() { (flag, exception) });
            }
            CatchException(exception);
        }
        internal static bool TryFindNewestException(Player player, out (ExceptionFlag, Exception) newest)
        {
            if (PlayerException.TryGetValue(player, out List<(ExceptionFlag, Exception)> list))
            {
                newest = list[^1];
                return true;
            }
            else
            {
                newest = (ExceptionFlag.Unknow, null);
                return false;
            }
        }
        internal static bool TryFindNewestException(Player player, ExceptionFlag flag, out (ExceptionFlag, Exception) newest)
        {
            if (PlayerException.TryGetValue(player, out List<(ExceptionFlag, Exception)> list))
            {
                List<(ExceptionFlag, Exception)> finded = list.FindAll(new Predicate<(ExceptionFlag, Exception)>((info) => info.Item1 == flag));
                if (finded.Count > 0)
                {
                    newest = finded[^1];
                    return true;
                }
                else
                {
                    newest = (ExceptionFlag.Unknow, null);
                    return false;
                }
            }
            else
            {
                newest = (ExceptionFlag.Unknow, null);
                return false;
            }
        }
        internal static void CatchException(ExceptionFlag flag, Exception exception)
        {
            if(Exceptions.ContainsKey(flag))
            {
                Exceptions[flag].Add(exception);
            }
            else
            {
                Exceptions.Add(flag, new List<Exception> { exception });
            }
            TimeLogger.DrawException(exception);
            LargerInventory.Instance.Logger.Warn(exception);
            switch(flag)
            {
                case ExceptionFlag.Unknow:
                    {
                        Main.NewText($"An unknown exception was caught by {LargerInventory.Instance.DisplayName}'s Self-check defense mechanism.\n" +
                            $"See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.Loading:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on save loading.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.Saveing:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on save saveing.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.UpdateArmorSet:
                case ExceptionFlag.AromorSet:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on try attempt to implement armor's bonus enhancements.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.FindRecipe:
                case ExceptionFlag.CraftItem:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on try attempt to implement recipe enhancements.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.PickAmmo:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on try attempt to implement pickammo enhancements.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.GrabItems:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on try attempt to implement grab items enhancements.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.Equips:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on try attempt to implement armors enhancements.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.GhostHurt:
                case ExceptionFlag.GhostHeal:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on try attempt to implement ghostarmor's bonus enhancements.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                case ExceptionFlag.QuickBuffs:
                case ExceptionFlag.QuickHeal:
                case ExceptionFlag.QuickMana:
                    {
                        Main.NewText($"{LargerInventory.Instance.DisplayName}'s Self-check defense mechanism caught an exception on try attempt to implement quick use enhancements.\n" +
                            $" This may cause the mod to not function properly.\n" +
                            $" See the error log for details",
                            Color.Red);
                        break;
                    }
                default:
                    {
                        Main.NewText($"An other types of exceptions exception was caught by {LargerInventory.Instance.DisplayName}'s Self-check defense mechanism.\n" +
                            $"See the error log for details",
                            Color.Red);
                        break;
                    }
            }
            Main.NewText("To avoid game crashes, related settings and functions have been turned off." +
                " But that doesn't mean bugs will disappear or never appear again." +
                " Please check according to the prompt information to see if there are other modules with functions related to the prompt exception type, which may conflict with each other." +
                " For feedback, please bring the error log file stored in the C:/Users/account name/Documents/My Games/Terraria/ModLoader/Beta/Logs directory (please provide client.log for stand-alone mode exceptions, and server.log for online mode exceptions)");
        }
        internal static void CatchException(Exception exception)
        {
            if(exception is IOException)
            {
                CatchException(ExceptionFlag.IOException, exception);
                return;
            }
            if(exception is NullReferenceException )
            {
                CatchException(ExceptionFlag.NullReferenceException, exception);
                return;
            }
            if(exception is FileLoadException)
            {
                CatchException(ExceptionFlag.FileLoadException, exception);
                return;
            }
            if(exception is FileNotFoundException)
            {
                CatchException(ExceptionFlag.FileNotFoundException, exception);
                return;
            }
            CatchException(ExceptionFlag.Uncategorized, exception);
        }
    }
}