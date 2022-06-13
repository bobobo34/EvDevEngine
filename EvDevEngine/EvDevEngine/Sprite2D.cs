using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using static EvDevEngine.EvDevEngine.XNAfuncs;
using Color = Microsoft.Xna.Framework.Color;

namespace EvDevEngine.EvDevEngine
{
    public enum Direction
    { 
        Up,
        Down,
        Left,
        Right
    }
    public enum LayerDepth
    {
        Background = 0,
        MiddleGround = 1,
        ForeGround = 2
    }
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Texture2D Sprite = null;
        public bool IsReference = false;
        public SpriteEffects Flipped = SpriteEffects.None;
        public float ScreenScale = 1f;
        public float Rotation = 0f;
        public EvDevEngine game;
        public Rectangle? SourceRectangle = null;
        public Color Tint = Color.White;
        public LayerDepth layerDepth = LayerDepth.MiddleGround;
        public bool Centered = false;
        public Vector2 Origin
        {
            get
            {
                if (!Centered) return Vector2.Zero();
                return new Vector2(Scale.X / 2, Scale.Y / 2);
            }
        }
        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Scale.X * ScreenScale), (int)(Scale.Y * ScreenScale));
            }
        }

        public Vector2 Min 
        { 
            get
            {
                if(IsReference) { return null; }
                return new Vector2(Position.X - Origin.X - Scale.X * 2 * ScreenScale, Position.Y - Origin.Y - Scale.Y * 2 * ScreenScale);
            }
        }
        public Vector2 Max
        {
            get
            {
                if(IsReference) { return null; }
                return new Vector2(Position.X + (Scale.X * ScreenScale), Position.Y + (Scale.Y * ScreenScale));
            }
        }
        
        public Sprite2D(EvDevEngine game, Vector2 Position, Vector2 Scale, string Directory, string Tag, bool Centered = false)
        {
            this.Centered = Centered;
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;
            this.game = game;
            Sprite = game.Content.Load<Texture2D>(Directory);

            EvDevEngine.RegisterSprite(this);
        }
        public Sprite2D(Game game, string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;

            Sprite = game.Content.Load<Texture2D>(Directory);
        }   

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag, bool Centered = false)
        {
            this.Centered = Centered;
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;


            EvDevEngine.RegisterSprite(this);
        }
        public void DrawSelf()
        {
            game.sprites.Draw(Sprite, rectangle, SourceRectangle, Tint, GetRotation(Rotation), Vec2(Origin), Flipped, (int)layerDepth);
        }
        public void ChangeSize(Vector2 OldScreenSize, Vector2 NewScreenSize)
        {
            Position.X = NewScreenSize.X * (Position.X / OldScreenSize.X);
            Position.Y = NewScreenSize.Y * (Position.Y / OldScreenSize.Y);
            ScreenScale *= NewScreenSize.X / OldScreenSize.X;
        }
        public void DestroySelf()
        {

            EvDevEngine.UnregisterSprite(this);
        }
    }
}
