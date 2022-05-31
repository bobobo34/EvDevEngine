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
                                Parent.Sprite.Position.X = Parent.Sprite.LastPosition.X;
                                break;
                            case Direction.Right:
                                Parent.Sprite.Position.X = Parent.Sprite.LastPosition.X;
                                break;
                            case Direction.Up:
                                Parent.Sprite.Position.Y = Parent.Sprite.LastPosition.Y;
                                break;
                            case Direction.Down:
                                
                                Log.Warning($"player pos: {Parent.Sprite.Position.X}, {Parent.Sprite.Position.Y} lastpos: {Parent.Sprite.LastPosition.X}, {Parent.Sprite.LastPosition.Y}");
                                [WARNING] -player: min 0, 130 max 70, 200 b min 0, 200 max 50, 250
                                Parent.Sprite.Position.Y = Parent.Sprite.LastPosition.Y;
                                
                                break;

                        }
                    }
                }
                else
                {
                    Parent.Sprite.LastPosition.X = Parent.Sprite.Position.X;
                    Parent.Sprite.LastPosition.Y = Parent.Sprite.Position.Y;
                }
            }
        }
        public override void Destroy() { }
        public override void OnDraw() { }
        public override void OnLoad() { }



    }
}