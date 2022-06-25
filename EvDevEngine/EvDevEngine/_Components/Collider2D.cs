using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static EvDevEngine.EvDevEngine.Engine;


namespace EvDevEngine.EvDevEngine._Components
{
    public class Collider2D : Component
    {
        public List<string> ColliderChecks = new List<string>();
        public Vector2 LastPosition = Vector2.Zero();
        
        public Collider2D(List<string> ObjectsToCollideWith)
        {
            Name = "Collider2D";
            foreach(string s in ObjectsToCollideWith)
            {
                ColliderChecks.Add(s);
            }
        }
        public Collider2D(string ObjectToCollideWith)
        {
            Name = "Collider2D";
            ColliderChecks.Add(ObjectToCollideWith);
        }
   
        public Sprite2D WillCollide(Vector2 NewPos, Vector2 Scale)
        {
            Rectangle spriteRectangle = new Rectangle((int)NewPos.X, (int)NewPos.Y, (int)Scale.X, (int)Scale.Y);
            foreach (Sprite2D sprite in AllSprites)
            {
                if (!ColliderChecks.Contains(sprite.Tag)) continue;
                Rectangle rectange = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, (int)sprite.Scale.X, (int)sprite.Scale.Y);
                if (spriteRectangle.Intersects(rectange)) return sprite;
            }
            return null;
        }



    }
}