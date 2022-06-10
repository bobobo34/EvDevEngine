using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework.Input;
using Vector2 = EvDevEngine.EvDevEngine.Vector2;
namespace EvDevEngine
{
    public class TestButton : IUIButton
    {
        public string Text { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Position { get; set; }
        public EvDevEngine.EvDevEngine game { get; set; }
        public SpriteFont Font { get; set; }
        public Texture2D BackgroundImage { get; set; }
        public bool MouseDown = false;
        public Color Tint
        {
            get
            {
                if (MouseDown) return Color.LightGray;
                else return Color.White;
            }
        }
        public Rectangle BackgroundRectangle
        {
            get
            { 
                return XNAfuncs.ScaledRectangle(new Rectangle((int)Position.X, (int)Position.Y, BackgroundImage.Width, BackgroundImage.Height), Scale);
            }
        }
        public TestButton(EvDevEngine.EvDevEngine game)
        {
            Scale = new Vector2(10f, 10f);
            Position = new Vector2(100f, 100f);
            this.game = game;
            Font = game.Content.Load<SpriteFont>("PixelFont");
            BackgroundImage = game.Content.Load<Texture2D>("TestButton");
            Text = "Test Button";
        }
        public void Update(GameTime gameTime)
        {
            if(XNAfuncs.IsClicked(BackgroundRectangle, false, ref MouseDown)) OnClick();
        }
        public void Draw(GameTime gameTime)
        {
            game.sprites.Draw(BackgroundImage, BackgroundRectangle, Tint);

            Vector2 drawposition = XNAfuncs.CenterDrawPoint(Font, Text, BackgroundRectangle);
            game.sprites.DrawString(Font, Text, XNAfuncs.Vec2(drawposition), Color.Black);
        }

        public void OnClick()
        {
            Log.Info("Clicked!");
        }
    }
}
