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
using static EvDevEngine.EvDevEngine.Engine;


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
        public Vector2 Scale = Vector2.Zero();
        public string Directory = "";
        public string Tag = "";
        public Texture2D Sprite = null;
        private Texture2D OriginalSprite;
        public bool IsReference = false;
        public SpriteEffects Flipped = SpriteEffects.None;
        public float ScreenScale = 1f;
        public float Rotation = 0f;
        public EvDevEngine game;
        public Rectangle? SourceRectangle = null;
        public Color Tint = Color.White;
        public LayerDepth layerDepth = LayerDepth.MiddleGround;
        public bool Centered = false;
        public float floatScale = 1f;
        public bool FromRectangle = false;

        public Vector2 Origin
        {
            get
            {
                if (!Centered) return Vector2.Zero();
                if (FromRectangle)
                    return new Vector2(Scale.X / 2, Scale.Y / 2);
                else return new Vector2((float)(OriginalSprite.Width / 2), (float)(OriginalSprite.Height / 2));
            }
        }
        public Rectangle rectangle
        {
            get
            {
                if(FromRectangle)
                    return new Rectangle((int)Position.X, (int)Position.Y, (int)(Scale.X * ScreenScale), (int)(Scale.Y * ScreenScale));
                else return new Rectangle((int)Position.X, (int)Position.Y, (int)(Sprite.Width * floatScale * ScreenScale), (int)(Sprite.Height * floatScale * ScreenScale));
            }
        }

        public Vector2 Min 
        { 
            get
            {
                if(IsReference) { return null; }
                if(FromRectangle)
                    return new Vector2(Position.X - Origin.X - Scale.X * 2 * ScreenScale, Position.Y - Origin.Y - Scale.Y * 2 * ScreenScale);
                else return new Vector2(Position.X - Origin.X - Sprite.Width * floatScale * 2 * ScreenScale, Position.Y - Origin.Y - Sprite.Height * floatScale * 2 * ScreenScale);
            }
        }
        public Vector2 Max
        {
            get
            {
                if(IsReference) { return null; }
                if (FromRectangle)
                    return new Vector2(Position.X + (Scale.X * ScreenScale), Position.Y + (Scale.Y * ScreenScale));
                else return new Vector2(Position.X + (Sprite.Width * floatScale * ScreenScale), Position.Y + (Sprite.Height * floatScale * ScreenScale));

            }
        }
        
        public Sprite2D(EvDevEngine game, Vector2 Position, Vector2 Scale, string Directory, string Tag, bool Centered = false)
        {
            this.FromRectangle = true;
            this.Centered = Centered;
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;
            this.game = game;
            Sprite = game.Content.Load<Texture2D>(Directory);
            OriginalSprite = Sprite;
            EvDevEngine.RegisterSprite(this);
        }
        public Sprite2D(EvDevEngine game, Vector2 Position, float Scale, string Directory, string Tag, bool Centered = false)
        {
            this.Centered = Centered;
            this.Position = Position;
            this.floatScale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;
            this.game = game;
            Sprite = game.Content.Load<Texture2D>(Directory);
            OriginalSprite = Sprite;

            EvDevEngine.RegisterSprite(this);
        }
        public Sprite2D(Game game, string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;

            Sprite = game.Content.Load<Texture2D>(Directory);
            OriginalSprite = Sprite;

        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag, bool Centered = false)
        {
            this.FromRectangle = true;
            this.Centered = Centered;
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;
            OriginalSprite = Sprite;


            EvDevEngine.RegisterSprite(this);
        }
        public Sprite2D(Vector2 Position, float Scale, Sprite2D reference, string Tag, bool Centered = false)
        {
            this.Centered = Centered;
            this.Position = Position;
            this.floatScale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;
            OriginalSprite = Sprite;


            EvDevEngine.RegisterSprite(this);
        }
        public void DrawSelf()
        {
            if(FromRectangle)
            {
                sprites.Draw(Sprite, rectangle, SourceRectangle, Tint, GetRotation(Rotation), Vec2(Origin), Flipped, (int)layerDepth / 2);
                return;
            }
            sprites.Draw(Sprite, Vec2(Position), SourceRectangle, Tint, GetRotation(Rotation), Vec2(Origin), floatScale, Flipped, (int)layerDepth / 2);
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
