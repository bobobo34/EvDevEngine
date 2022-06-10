using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine
{
    public class StartState : State
    {
        public StartState(EvDevEngine.EvDevEngine game) : base("StartScreen", game)
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            Log.Info("Loading");
            base.Draw(gameTime);
        }
        public override void Load()
        {

        }
    }
}
