using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine
{
    class DemoGame : EvDevEngine.EvDevEngine
    {
        public Player player;

        readonly string[,] Map =
        {
            { ".",".",".",".",".",".","." },
            { ".",".",".",".",".",".","." },
            { ".",".",".",".",".",".","." },
            { ".",".",".",".",".",".","." },
            { "g","g","g","g","g","g","g" },
            { ".",".",".",".",".",".","." }
        };
        public DemoGame() : base(new EvDevEngine.Vector2(615, 515), "EvDevEngine Demo") { }

        public override void Load()
        {
            BackgroundColor = Microsoft.Xna.Framework.Color.Black;
            
            Sprite2D groundRef = new Sprite2D(Window, "player");

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {

                    if (Map[j, i] == "g")
                    {
                        new Object2D("ground", new Sprite2D(new EvDevEngine.Vector2(i * 50 + 50, j * 50), new EvDevEngine.Vector2(50, 50), groundRef, "ground"));
                    }
                }
            }
            
            Sprite2D PlayerSprite = new Sprite2D(Window, new EvDevEngine.Vector2(0, 10), new EvDevEngine.Vector2(24, 66), "player", "Player");
            player = new Player("MainPlayer", PlayerSprite, Window);
            
           

        }


        public override void Initialize()
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            
            
            
        }

        public override void Update(GameTime gameTime)
        {
            //Log.Info(Window.Updates);
        }

        public override void Unload()
        {
            
        }
    }
}
