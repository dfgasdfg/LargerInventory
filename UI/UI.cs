using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.ID;

namespace LargerInventory.UI
{
    class MultibackpackSystem : ModSystem
    {
        internal PackageUI Package;
        internal UserInterface PackageUserInterface;
        public override void Load()
        {
            Package = new PackageUI();
            Package.Activate();
            PackageUserInterface = new UserInterface();
            PackageUserInterface.SetState(Package);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (PackageUI.Open)
                PackageUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            //寻找一个名字为Vanilla: Mouse Text的绘制层，也就是绘制鼠标字体的那一层，并且返回那一层的索引
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            //寻找到索引时
            if (MouseTextIndex != -1)
            {
                //往绘制层集合插入一个成员，第一个参数是插入的地方的索引，第二个参数是绘制层
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   //这里是绘制层的名字
                   "I : PackageUI",
                   //这里是匿名方法
                   delegate
                   {
                       if (PackageUI.Open)
                           Package.Draw(Main.spriteBatch);
                       return true;
                   },
                   //这里是绘制层的类型
                   InterfaceScaleType.UI)
               );
            }
            base.ModifyInterfaceLayers(layers);
        }

    }
    public class PackageForItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<MultibackpackPlayer>().CantUse)
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }
    }
    class MultibackpackPlayer : ModPlayer
    {
        public bool CantUse = false;
        public override void ResetEffects()
        {
            if (Main.playerInventory && Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1)
            {
                PackageUI.Open = true;
            }
            else
            {
                PackageUI.Open = false;
                CantUse = false;
            }
            base.ResetEffects();
        }
    }
    public class PackageUI : UIState
    {
        public static bool Open = false;
        internal static bool Sound = false;
        public static new float Width = 0.180f;//UI位置平移
        public static new float Height = 0.365f;//UI位置竖移
        public static bool HideAlways = false;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(HideAlways)
            {
                return;
            }
            /*Texture2D texturestar = ModContent.Request<Texture2D>("LargerInventory/UI/Star").Value;
            Rectangle Barstar = new Rectangle((int)(Main.screenWidth * (Width + 0.086f)) + 28, (int)(Main.screenHeight * 0.365f), 26, 28);
            Color colorstar = new Color(75, 75, 75, 75);
            Rectangle barstar = new Rectangle(0, 0, 26, 28);
            if (Barstar.Contains(Main.MouseScreen.ToPoint()))
            {
                
                
                if (!Sound)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    Sound = true;
                }
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, Language.GetTextValue("Mods.LargerInventory.Common.3"),
                new Vector2(Main.mouseX + 16f, Main.mouseY + 10f), Color.White, 0f, Vector2.Zero, new Vector2(1f));//把文字的翻译加上
                colorstar = Color.White;
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                    //收藏
                }
            }
            spriteBatch.Draw(texturestar, Barstar, barstar, colorstar, 0f, Vector2.Zero, SpriteEffects.None, 0f);*/
            var widthX = 73;
            var heightY = 265;
            int num = Manager.Activing.NowInventoryIndex + 1;
            Vector2 vector = new Vector2((int)(widthX+181), (int)(heightY));
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, $"{num}",
            vector, Color.White, 0f, Vector2.Zero, new Vector2(1.1f));

            Texture2D texture01 = ModContent.Request<Texture2D>("LargerInventory/UI/PackButton01").Value;
            Texture2D texture02 = ModContent.Request<Texture2D>("LargerInventory/UI/PackButton02").Value;
            
            Rectangle Bar011 = new Rectangle((int)(widthX+96), (int)(heightY), 32, 28);
            Rectangle Bar012 = new Rectangle((int)(widthX) + 138, (int)(heightY), 18, 28);
            Rectangle Bar021 = new Rectangle((int)(widthX + 211), (int)(heightY), 18, 28);
            Rectangle Bar022 = new Rectangle((int)(widthX + 239), (int)(heightY), 32, 28);
            Color color01 = new Color(75, 75, 75, 75);
            Color color02 = new Color(75, 75, 75, 75);
            Color color03 = new Color(75, 75, 75, 75);
            Color color04 = new Color(75, 75, 75, 75);
            Rectangle Bar01 = new Rectangle(0, 0, 32, 28);
            Rectangle Bar02 = new Rectangle(42, 0, 18, 28);
            Rectangle Bar03 = new Rectangle(0, 0, 18, 28);
            Rectangle Bar04 = new Rectangle(28, 0, 32, 28);

            Texture2D texture03 = ModContent.Request<Texture2D>("LargerInventory/UI/QuickStack01").Value;
            Texture2D texture04 = ModContent.Request<Texture2D>("LargerInventory/UI/QuickStack1").Value;
            Rectangle Bar003 = new Rectangle((int)(widthX), (int)(heightY) - 1, 32, 30);
            Rectangle Bar004 = new Rectangle((int)(widthX +40), (int)(heightY) - 1, 32, 30);
            Rectangle bar = new Rectangle(0, 0, 32, 30);
            if (Bar012.Contains(Main.MouseScreen.ToPoint()))
            {
                if (Flag.ChangeAvailable && Manager.Activing.NowInventoryIndex > 0)
                {
                    Main.LocalPlayer.creativeInterface = true;
                    Main.LocalPlayer.mouseInterface = true;
                    if (!Sound)
                    {
                        SoundEngine.PlaySound(SoundID.MenuTick);
                        Sound = true;
                    }
                    color02 = Color.White;
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                        Manager.Activing.SaveInventory();
                        Manager.Activing.NowInventoryIndex--;
                        Manager.Activing.SendInventory();
                    }
                }
            }
            else if (Bar021.Contains(Main.MouseScreen.ToPoint()))
            {
                if (Flag.ChangeAvailable && Manager.Activing.NowInventoryIndex < Manager.Activing.pages.Count - 1)
                {
                    Main.LocalPlayer.creativeInterface = true;
                    Main.LocalPlayer.mouseInterface = true;
                    if (!Sound)
                    {
                        SoundEngine.PlaySound(SoundID.MenuTick);
                        Sound = true;
                    }
                    color03 = Color.White;
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                        Manager.Activing.SaveInventory();
                        Manager.Activing.NowInventoryIndex++;
                        Manager.Activing.SendInventory();
                    }
                }
            }
            else if (Bar011.Contains(Main.MouseScreen.ToPoint()))
            {
                if (Flag.ChangeAvailable && Manager.Activing.NowInventoryIndex > 0)
                {
                    Main.LocalPlayer.creativeInterface = true;
                    Main.LocalPlayer.mouseInterface = true;
                    if (!Sound)
                    {
                        SoundEngine.PlaySound(SoundID.MenuTick);
                        Sound = true;
                    }
                    color01 = Color.White;
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                        Manager.Activing.SaveInventory();
                        Manager.Activing.NowInventoryIndex = 0;
                        Manager.Activing.SendInventory();
                    }
                }
            }
            else if (Bar022.Contains(Main.MouseScreen.ToPoint()))
            {
                if (Flag.ChangeAvailable && Manager.Activing.NowInventoryIndex < Manager.Activing.pages.Count - 1)
                {
                    Main.LocalPlayer.creativeInterface = true;
                    Main.LocalPlayer.mouseInterface = true;
                    if (!Sound)
                    {
                        SoundEngine.PlaySound(SoundID.MenuTick);
                        Sound = true;
                    }
                    color04 = Color.White;
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                        Manager.Activing.SaveInventory();
                        Manager.Activing.NowInventoryIndex = int.MaxValue;
                        Manager.Activing.SendInventory();
                    }
                }
            }
            else if (Bar003.Contains(Main.MouseScreen.ToPoint()))
            {
                Main.LocalPlayer.creativeInterface = true;
                Main.LocalPlayer.mouseInterface = true;
                if (!Sound)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    Sound = true;
                }
                texture03 = ModContent.Request<Texture2D>("LargerInventory/UI/QuickStack02").Value;
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, Language.GetTextValue("Mods.LargerInventory.Common.1"),
                new Vector2(Main.mouseX + 16f, Main.mouseY + 10f), Color.White, 0f, Vector2.Zero, new Vector2(1f));
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                    Manager.Activing.pages[Manager.Activing.NowInventoryIndex].QuickStack();
                }
            }
            else if (Bar004.Contains(Main.MouseScreen.ToPoint()))
            {
                Main.LocalPlayer.creativeInterface = true;
                Main.LocalPlayer.mouseInterface = true;
                if (!Sound)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    Sound = true;
                }
                texture04 = ModContent.Request<Texture2D>("LargerInventory/UI/QuickStack2").Value;
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, Language.GetTextValue("Mods.LargerInventory.Common.2"),
                new Vector2(Main.mouseX + 16f, Main.mouseY + 10f), Color.White, 0f, Vector2.Zero, new Vector2(1f));
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                    Page.QuickStackAll();
                }
            }
            else
            {
                Main.LocalPlayer.creativeInterface = false;
                if (Sound)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    Sound = false;
                }
            }
            spriteBatch.Draw(texture01, Bar011, Bar01, color01, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture01, Bar012, Bar02, color02, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture02, Bar021, Bar03, color03, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture02, Bar022, Bar04, color04, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            spriteBatch.Draw(texture03, Bar003, bar, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture04, Bar004, bar, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
