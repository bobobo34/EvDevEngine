using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDevEngine.EvDevEngine
{
    public class Vector2
    {
        public float X;
        public float Y;

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
        /// <summary>
        /// Returns X & Y as 0
        /// </summary>
        /// <returns></returns>
        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }

        
    }
}
