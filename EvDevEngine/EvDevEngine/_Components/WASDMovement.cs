using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine.EvDevEngine._Components
{
    public class WASDMovement : Movement
    {
        
        public WASDMovement(float MovementSpeed)
        {
            this.MovementSpeed = MovementSpeed;
        }
        public WASDMovement() { }

        public override void OnUpdate(GameTime gameTime)
        {
            if(!EvDevEngine.Input.IsKeyDown(Keys.A) && !EvDevEngine.Input.IsKeyDown(Keys.A) && !EvDevEngine.Input.IsKeyDown(Keys.A) && !EvDevEngine.Input.IsKeyDown(Keys.A))
            {
                EndMove();
            }
            if (EvDevEngine.Input.IsKeyDown(Keys.W))
            {
                BeginMove(Direction.Up, gameTime);
            }
            if (EvDevEngine.Input.IsKeyDown(Keys.D)) 
            {
                BeginMove(Direction.Right, gameTime);
            }
            if (EvDevEngine.Input.IsKeyDown(Keys.S)) 
            {
                BeginMove(Direction.Down, gameTime);
            }
            if (EvDevEngine.Input.IsKeyDown(Keys.A)) 
            {
                BeginMove(Direction.Left, gameTime);
            }
        }

        
    }
}
