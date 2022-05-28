using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;

namespace EvDevEngine.EvDevEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Image Sprite = null;
        public bool IsReference = false;
        //BodyDef bodyDef = new BodyDef();
        //Body body;
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
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
        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[SHAPE2D]({Tag}) - Has been registered!");
            EvDevEngine.RegisterSprite(this);
        }
        public Sprite2D(string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;
        }

        //public void CreateDynamic()
        //{
        //    bodyDef = new BodyDef();
        //    bodyDef.Position = new Vec2(this.Position.X, this.Position.Y);
        //    body = EvDevEngine.world.CreateBody(bodyDef);

        //    // Define another box shape for our dynamic body.
        //    PolygonDef shapeDef = new PolygonDef();
        //    shapeDef.SetAsBox(Scale.X / 2, Scale.Y / 2);

        //    // Set the box density to be non-zero, so it will be dynamic.
        //    shapeDef.Density = 1.0f;

        //    // Override the default friction.
        //    shapeDef.Friction = 0.3f;

        //    // Add the shape to the body.
        //    body.CreateShape(shapeDef);

        //    // Now tell the dynamic body to compute it's mass properties base
        //    // on its shape.
        //    body.SetMassFromShapes();
        //}
        //public void AddForce(Vector2 force, Vector2 point)
        //{
        //    body.ApplyImpulse(new Vec2(force.X * 1000, force.Y * 1000), new Vec2(point.X, point.Y));
        //}
        //public void UpdatePosition()
        //{
        //    this.Position.X = body.GetPosition().X;
        //    this.Position.Y = body.GetPosition().Y;
        //}

        //public void CreateStatic()
        //{
        //    bodyDef = new BodyDef();
        //    bodyDef.Position = new Vec2(this.Position.X, this.Position.Y);
        //    body = EvDevEngine.world.CreateBody(bodyDef);

        //    // Define the ground box shape.
        //    PolygonDef groundShapeDef = new PolygonDef();

        //    // The extents are the half-widths of the box.
        //    groundShapeDef.SetAsBox(Scale.X / 2, Scale.Y / 2);

        //    // Add the ground shape to the ground body.
        //    body.CreateShape(groundShapeDef);

        //}
        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;

            Log.Info($"[SHAPE2D]({Tag}) - Has been registered!");
            EvDevEngine.RegisterSprite(this);
        }

        public bool IsColliding(Sprite2D a, Sprite2D b)
        {
            if(a.Min.X < b.Max.X &&
                a.Max.X > b.Min.X && 
                a.Min.Y < b.Max.Y &&
                a.Max.Y > b.Min.Y)
            {
                return true;
            }
            return false;
        }

        public List<Direction> GetCollisionDirections(Sprite2D b)
        {
            //Corner Matching
            //bottom right corner
            if (Min.Y == b.Max.Y && Min.X == b.Max.X)
            {
                //if the corner is floating, we don't need to worry about anything
                if (b.Max.IsFloatingPoint(new List<Sprite2D> { this, b }))
                {
                    return new List<Direction> { };
                }
            }
            //bottom left corner
            if (Min.Y == b.Max.Y && Max.X == b.Min.X)
            {
                if (new Vector2(b.Min.X, b.Max.Y).IsFloatingPoint(new List<Sprite2D> { this, b }))
                {
                    return new List<Direction> { };
                }
            }
            //top right corner
            if (Max.Y == b.Min.Y && Min.X == b.Max.X)
            {
                if (new Vector2(b.Max.X, b.Min.Y).IsFloatingPoint(new List<Sprite2D> { this, b }))
                {
                    return new List<Direction> { };
                }
            }
            //top left corner
            if (Max.Y == b.Min.Y && Max.X == b.Min.X)
            {
                if (b.Min.IsFloatingPoint(new List<Sprite2D> { this, b }))
                {
                    return new List<Direction> { };
                }
            }
            //Regular Directions
            if (Min.Y == b.Max.Y) return new List<Direction> { Direction.Up };
            if (Max.Y == b.Min.Y) return new List<Direction> { Direction.Down };
            if (Min.X == b.Max.X) return new List<Direction> { Direction.Left };
            if (Max.X == b.Min.X) return new List<Direction> { Direction.Right };

            //if the weird corner glitch happens (i dont know how to fix it)
            return new List<Direction> { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
        }
        public Sprite2D IsColliding(string tag)
        {
            foreach(Sprite2D b in EvDevEngine.AllSprites)
            {
                if(b.Tag == tag)
                {
                    if (Min.X <= b.Max.X &&
                       Max.X >= b.Min.X &&
                       Min.Y <= b.Max.Y &&
                       Max.Y >= b.Min.Y)
                    {
                        return b;
                    }
                }
            }
            return null;
        }
        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has been destroyed!");
            EvDevEngine.UnregisterSprite(this);
        }
    }
}
