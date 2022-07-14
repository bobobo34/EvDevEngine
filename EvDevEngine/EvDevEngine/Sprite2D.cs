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
using System.Threading;

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
        public float layerDepth = 0f;
        public bool Centered = false;
        public float floatScale = 1f;
        public bool FromRectangle = false;
            #nullable enable
        public Vector2? NewOrigin;
#nullable disable
        public readonly Color[] TextureData;

        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Vec2(Origin), 0)) *
                    Matrix.CreateRotationZ(MathHelper.ToRadians(Rotation)) *
                    Matrix.CreateTranslation(new Vector3(Vec2(Position), 0));
            }
        }
        public Vector2 Origin
        {
            get
            {
                if (NewOrigin != null) return NewOrigin;
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
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, (int)(Sprite.Width * floatScale), (int)(Sprite.Height * floatScale));
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
            //var textureDataGathered = new ManualResetEvent(false);

            TextureData = new Color[Sprite.Width * Sprite.Height];
            Sprite.GetData(TextureData);
            

           // textureDataGathered.WaitOne();
           // textureDataGathered.Reset();
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

            //var textureDataGathered = new ManualResetEvent(false);

            TextureData = new Color[Sprite.Width * Sprite.Height];

                Sprite.GetData(TextureData);


            //textureDataGathered.WaitOne();
            //textureDataGathered.Reset();

            EvDevEngine.RegisterSprite(this);
        }
        public Sprite2D(EvDevEngine game, string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;

            Sprite = game.Content.Load<Texture2D>(Directory);
            //var textureDataGathered = new ManualResetEvent(false);

            TextureData = new Color[Sprite.Width * Sprite.Height];
            //Log.Info(Directory);

                Sprite.GetData(TextureData);


            //textureDataGathered.WaitOne();
            //textureDataGathered.Reset();
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
            //var textureDataGathered = new ManualResetEvent(false);

            TextureData = reference.TextureData;
            Log.Info(Directory);

            //game.EnqueueAction(() =>
            //{
            //    Sprite.GetData(TextureData);
            //    DataReceived = true;

            //    //textureDataGathered.Set();
            //});

            //textureDataGathered.WaitOne();
            //textureDataGathered.Reset();
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
            //var textureDataGathered = new ManualResetEvent(false);

            TextureData = reference.TextureData;

            //game.EnqueueAction(() =>
            //{
            //    Sprite.GetData(TextureData);
            //    DataReceived = true;

            //    //textureDataGathered.Set();
            //});
            //textureDataGathered.WaitOne();
            //textureDataGathered.Reset();
            EvDevEngine.RegisterSprite(this);
        }
        public void DrawSelf()
        {
            if(FromRectangle)
            {
                sprites.Draw(Sprite, rectangle, SourceRectangle, Tint, MathHelper.ToRadians(Rotation), Vec2(Origin), Flipped, 0);
                return;
            }
            sprites.Draw(Sprite, Vec2(Position), SourceRectangle, Tint, MathHelper.ToRadians(Rotation), Vec2(Origin), floatScale, Flipped, 0);
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
        public virtual void OnCollide(Sprite2D sprite) { }
        public bool Intersects(Sprite2D sprite)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            var transformAToB = this.Transform * Matrix.Invert(sprite.Transform);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            var stepX = Microsoft.Xna.Framework.Vector2.TransformNormal(Microsoft.Xna.Framework.Vector2.UnitX, transformAToB);
            var stepY = Microsoft.Xna.Framework.Vector2.TransformNormal(Microsoft.Xna.Framework.Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            var yPosInB = Microsoft.Xna.Framework.Vector2.Transform(Microsoft.Xna.Framework.Vector2.Zero, transformAToB);

            for (int yA = 0; yA < this.rectangle.Height; yA++)
            {
                // Start at the beginning of the row
                var posInB = yPosInB;

                for (int xA = 0; xA < this.rectangle.Width; xA++)
                {
                    // Round to the nearest pixel
                    var xB = (int)Math.Round(posInB.X);
                    var yB = (int)Math.Round(posInB.Y);

                    if (0 <= xB && xB < sprite.rectangle.Width &&
                        0 <= yB && yB < sprite.rectangle.Height)
                    {
                        // Get the colors of the overlapping pixels
                        Color colourA = Color.Transparent, colourB = Color.White;
                        try
                        {
                            colourA = this.TextureData[xA + yA * this.rectangle.Width];
                            colourB = sprite.TextureData[xB + yB * sprite.rectangle.Width];
                        }
                        catch
                        {
                            Log.Info((this.TextureData.Length, xA + yA * this.rectangle.Width, xA, yA, this.rectangle.Width));
                            Log.Info((sprite.TextureData.Length, xB + yB * sprite.rectangle.Width, xB, yB, sprite.rectangle.Width));
                        }
                        // If both pixel are not completely transparent
                        if (colourA.A != 0 && colourB.A != 0)
                        {
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }
    }
}
