using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Microsoft.Xna.Framework.Point;
namespace EvDevEngine.EvDevEngine
{
    public class Vector2
    {
        public float X;
        public float Y;

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);

        public override string ToString() => $"({X}, {Y})";

        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator /(Vector2 a, float b) => new Vector2(a.X / b, a.Y / b);
        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.X * b, a.Y * b);

        public Vector2(Point p)
        {
            X = p.X;
            Y = p.Y;
        }
        public Vector2()
        {
            X = Zero().X;
            Y = Zero().Y;
        }
        public Vector2(Vector2 vec)
        {
            this.X = vec.X;
            this.Y = vec.Y;
        }
        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        public bool IsFloatingPoint(List<Sprite2D> exceptions)
        {
            foreach(Sprite2D sprite in EvDevEngine.AllSprites)
            {
                if (exceptions.Contains(sprite)) continue;
                if(sprite.Min.X <= X && X <= sprite.Max.X &&
                    sprite.Min.Y <= Y && Y <= sprite.Max.Y)
                {
                    
                    return false;
                }
            }
            
            return true;
        }
    
        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
        
    }
}
