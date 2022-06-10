using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDevEngine.EvDevEngine
{
    public class LoadingState : State
    {
        public LoadingState(EvDevEngine game) : base("Loading State", game) { }

        public override void Load()
        {
            //Change this to fit your game.
            Log.Info("Loading game...");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
