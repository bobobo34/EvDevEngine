using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace EvDevEngine.EvDevEngine._Components
{
    public class WASDMovement : Component
    {
        public float MovementSpeed = 5f;
        public WASDMovement(Object2D Parent, float MovementSpeed)
        {
            this.Parent = Parent;
            this.MovementSpeed = MovementSpeed;
        }
        public WASDMovement(Object2D Parent)
        {
            this.Parent=Parent;
        }
        public override void Destroy() { }


        public override void OnDraw() { }


        public override void OnLoad() { }


        public override void OnUpdate()
        {
            if (EvDevEngine.Input.IsKeyDown(Key.W)) { Parent.Sprite.Position.Y -= MovementSpeed; }
            if (EvDevEngine.Input.IsKeyDown(Key.D)) { Parent.Sprite.Position.X += MovementSpeed; }
            if (EvDevEngine.Input.IsKeyDown(Key.S)) { Parent.Sprite.Position.Y += MovementSpeed; }
            if (EvDevEngine.Input.IsKeyDown(Key.A)) { Parent.Sprite.Position.X -= MovementSpeed; }
        }
    }
}
