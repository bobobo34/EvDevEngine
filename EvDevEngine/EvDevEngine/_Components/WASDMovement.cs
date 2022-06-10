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
            if(!EvDevEngine.KeyboardInput.IsKeyDown(Keys.A) && !EvDevEngine.KeyboardInput.IsKeyDown(Keys.A) && !EvDevEngine.KeyboardInput.IsKeyDown(Keys.A) && !EvDevEngine.KeyboardInput.IsKeyDown(Keys.A))
            {
                EndMove();
            }
            if (EvDevEngine.KeyboardInput.IsKeyDown(Keys.W))
            {
                BeginMove(Direction.Up, gameTime);
            }
            if (EvDevEngine.KeyboardInput.IsKeyDown(Keys.D)) 
            {
                BeginMove(Direction.Right, gameTime);
            }
            if (EvDevEngine.KeyboardInput.IsKeyDown(Keys.S)) 
            {
                BeginMove(Direction.Down, gameTime);
            }
            if (EvDevEngine.KeyboardInput.IsKeyDown(Keys.A)) 
            {
                BeginMove(Direction.Left, gameTime);
            }
        }

        
    }
}
