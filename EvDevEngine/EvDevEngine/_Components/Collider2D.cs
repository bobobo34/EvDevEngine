using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDevEngine.EvDevEngine._Components
{
    public class Collider2D : Component
    {
        public List<string> ColliderChecks = new List<string>();
        public Vector2 LastPosition = Vector2.Zero();
        
        public Collider2D(List<string> ObjectsToCollideWith, Object2D Parent)
        {
            this.Parent = Parent;
            Name = "Collider2D";
            foreach(string s in ObjectsToCollideWith)
            {
                ColliderChecks.Add(s);
            }
        }
        public Collider2D(string ObjectToCollideWith, Object2D Parent)
        {
            this.Parent = Parent;
            Name = "Collider2D";
            ColliderChecks.Add(ObjectToCollideWith);
        }
        public override void OnUpdate()
        {
            foreach(string s in ColliderChecks)
            {
                Sprite2D sprite = Parent.Sprite.IsColliding(s);
                if(sprite != null)
                {
                    
                    List<Direction> directions = Parent.Sprite.GetCollisionDirections(sprite);
                    
                    foreach (Direction direction in directions)
                    {
                        
                        switch (direction)
                        {
                            case Direction.Left:
                                Parent.Sprite.Position.X = LastPosition.X;
                                break;
                            case Direction.Right:
                                Parent.Sprite.Position.X = LastPosition.X;
                                break;
                            case Direction.Up:
                                Parent.Sprite.Position.Y = LastPosition.Y;
                                break;
                            case Direction.Down:                                                       
                                Parent.Sprite.Position.Y = LastPosition.Y;                              
                                break;

                        }
                    }
                }
                else
                {
                    LastPosition.X = Parent.Sprite.Position.X;
                    LastPosition.Y = Parent.Sprite.Position.Y;
                }
            }
        }
        public override void Destroy() { }
        public override void OnDraw() { }
        public override void OnLoad() { }



    }
}