using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Input;

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
        public DemoGame() : base(new Vector2(615, 515), "EvDevEngine Demo") { }

        public override void Load()
        {
            BackgroundColor = Color.Black;
            
            Sprite2D groundRef = new Sprite2D("player");

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), groundRef, "ground");
                    }
                }
            }
            
            Sprite2D PlayerSprite = new Sprite2D(new Vector2(0, 10), new Vector2(70, 70), "player", "Player");
            player = new Player("MainPlayer", PlayerSprite);
            
           

        }



        public override void Draw()
        {
            
        }

        public override void Update()
        {
            Log.Info(Vector2.Zero().X.ToString());

        }

        
        public override void FormClosing(FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }
    }
}
