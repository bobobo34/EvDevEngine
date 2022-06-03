using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace EvDevEngine.EvDevEngine
{
    public enum Direction
    { 
        Up,
        Down,
        Left,
        Right
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
        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Scale.X, (int)Scale.Y);
            }
        }

        public Vector2 Min 
        { 
            get
            {
                if(IsReference) { return null; }
                return new Vector2(Position.X, Position.Y);
            }
        }
        public Vector2 Max
        {
            get
            {
                if(IsReference) { return null; }
                return new Vector2(Position.X + Scale.X, Position.Y + Scale.Y);
            }
        }
        
        public Sprite2D(Game game, Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Sprite = game.Content.Load<Texture2D>(Directory);


            Log.Info($"[SHAPE2D]({Tag}) - Has been registered!");
            EvDevEngine.RegisterSprite(this);
        }
        public Sprite2D(Game game, string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;

            Sprite = game.Content.Load<Texture2D>(Directory);
        }   

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;

            Log.Info($"[SHAPE2D]({Tag}) - Has been registered!");
            EvDevEngine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has been destroyed!");
            EvDevEngine.UnregisterSprite(this);
        }
    }
}
