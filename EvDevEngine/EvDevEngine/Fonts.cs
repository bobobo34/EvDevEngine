using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static EvDevEngine.EvDevEngine.XNAfuncs;
using Microsoft.Xna.Framework;

namespace EvDevEngine.EvDevEngine
{
    public enum Fonts
    {
        Pixelated
    }
    public class FontText
    {
        public string Text;
        public Vector2 Size;
        public EvDevEngine game;
        public float Rotation = 0f;
        public float Scale = 1f;
        private float ScreenScale = 1f;
        public Vector2 Origin
        {
            get
            {
                return new Vector2(Size.X / 2, Size.Y / 2);
            }
        }
        public Vector2 Position;
        public SpriteFont font;
        public Color SpriteColor = Color.Black;
        private float GetRotation(float degrees)
        {
            return (((float)Math.PI / 360) * degrees);
        }
        public FontText(SpriteFont font, string Text, Vector2 position, EvDevEngine game)
        {
            this.font = font;
            this.Size = Vec2(font.MeasureString(Text));
            Position = position;  
            this.Text = Text;
            this.game = game;
        }
        public void DrawSelf()
        {
            game.sprites.DrawString(font, Text, Vec2(Position), SpriteColor, GetRotation(Rotation), Vec2(Origin), Scale * ScreenScale, SpriteEffects.None, 1);
        }
        //Vec2(new Vector2(game.ScreenCenter().X, game.ScreenCenter().Y - 200))
        //font, Text, Position, SpriteColor, GetRotation(FontRotation), Vec2(Origin), FontScale, SpriteEffects.None, 1
        public void ChangeSize(Vector2 OldScreenSize, Vector2 NewScreenSize)
        {
            Position.X = NewScreenSize.X * (Position.X / OldScreenSize.X);
            Position.Y = NewScreenSize.Y * (Position.Y / OldScreenSize.Y);
            ScreenScale *= NewScreenSize.X / OldScreenSize.X;
        }

    }
}
