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
                //Log.Info(GlobalScale);
                if (!graphics.IsFullScreen)
                {
                    //GlobalScale = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 960;
                    EvDevEngine.Vector2 oldsize = new EvDevEngine.Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    graphics.IsFullScreen = true;
                    //Window.IsBorderless = true;
                    EvDevEngine.Vector2 newsize = new EvDevEngine.Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    CurrentState.ResizeAll(oldsize, newsize);
                    graphics.ApplyChanges();
                }
                else if(graphics.IsFullScreen)
                {
                    //GlobalScale = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 960 / 2;
                    EvDevEngine.Vector2 oldsize = new EvDevEngine.Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
                    graphics.IsFullScreen = false;
                    //Window.IsBorderless = false;
                    EvDevEngine.Vector2 newsize = new EvDevEngine.Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    CurrentState.ResizeAll(oldsize, newsize);
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
