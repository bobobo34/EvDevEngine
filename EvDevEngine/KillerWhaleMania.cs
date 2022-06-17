using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EvDevEngine
{
    class KillerWhaleMania : EvDevEngine.EvDevEngine
    {

        bool f11down = false;


        public KillerWhaleMania() : base("Killer Whale Mania!") { }


        public override void OnInit()
        {
            
        }

        public override void AddStates()
        {

            AddState(new TitleState(this));

            base.AddStates();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!KeyboardInput.IsKeyDown(Keys.F11)) f11down = false;
            if (KeyboardInput.IsKeyDown(Keys.F11) && !f11down)
            {
                f11down = true;
                if (!graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    graphics.IsFullScreen = true;
                    graphics.ApplyChanges();
                }
                else if(graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
                    graphics.IsFullScreen = false;
                    graphics.ApplyChanges();
                }
            }
            base.OnUpdate(gameTime);
        }
        public override void Unload()
        {
            
        }
        
    }
}
///TODO: FIX MOUSE POS