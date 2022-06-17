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
using static EvDevEngine.EvDevEngine.XNAfuncs;
namespace EvDevEngine.EvDevEngine
{
    public class NormalButton : IUIButton
    {
        public Vector2 Scale { get; set; }
        public Vector2 Position { get; set; }
        public EvDevEngine game { get; set; }
        public FontText Font { get; set; }
        public Texture2D BackgroundImage { get; set; }
        public bool MouseDown = false;
        public bool Centered = false;
        public float ScreenScale = 1f;
        public Action OnClick { get; set; }
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
                return ScaledRectangle(new Rectangle((int)Position.X, (int)Position.Y, BackgroundImage.Width, BackgroundImage.Height), Scale * ScreenScale);
            }
        }
        public Rectangle ReferenceRectangle
        {
            get
            {
                if (!Centered) return BackgroundRectangle;
                return new Rectangle((int)(Center.X - BackgroundRectangle.Width / 2), (int)(Center.Y - BackgroundRectangle.Height / 2), BackgroundRectangle.Width, BackgroundRectangle.Height);

            }
        }
        public Vector2 Origin
        {
            get
            {
                if (!Centered) return Vector2.Zero();
                return new Vector2(BackgroundImage.Width / 2, BackgroundImage.Height / 2);
            }
        }
        public Vector2 Center
        {
            get
            {
                if (Centered) return new Vector2(Position.X + Scale.X / 2, Position.Y + Scale.Y / 2);
                return new Vector2(BackgroundRectangle.Center);
            }
        }
        public NormalButton(EvDevEngine game, FontText Font, Vector2 Scale, Vector2 Position, bool Centered = false)
        {
            this.Centered = Centered;
            this.Scale = Scale;
            this.Position = Position;
            this.game = game;
            this.Font = Font;
            BackgroundImage = game.Content.Load<Texture2D>("TestButton");
        }
        public void Update(GameTime gameTime)
        {
            if(IsClicked(ReferenceRectangle, false, ref MouseDown)) OnClick();
        }
        public void Draw(GameTime gameTime)
        {
            game.sprites.Draw(BackgroundImage, BackgroundRectangle, null, Tint, 0f, Vec2(Origin), SpriteEffects.None, 0);
            Font.DrawSelf();

        }
        public void ChangeSize(Vector2 OldScreenSize, Vector2 NewScreenSize)
        {
            Font.ChangeSize(OldScreenSize, NewScreenSize);
            Position.X = NewScreenSize.X * (Position.X / OldScreenSize.X);
            Position.Y = NewScreenSize.Y * (Position.Y / OldScreenSize.Y);
            ScreenScale *= NewScreenSize.X / OldScreenSize.X;
        }
    }
}
