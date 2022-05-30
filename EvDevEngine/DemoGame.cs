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
        Sprite2D player;

        Vector2 lastPos = Vector2.Zero();

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
            
            player = new Sprite2D(new Vector2(0, 10), new Vector2(70, 70), "player", "Player");
            //player.CreateDynamic();
           

        }



        public override void Draw()
        {
            
        }

        float MovementSpeed = 5f;
        public override void Update()
        {
            //player.UpdatePosition();
            if (Input.IsKeyDown(Key.W)) { player.Position.Y -= MovementSpeed; }
            if (Input.IsKeyDown(Key.D)) { player.Position.X += MovementSpeed; }
            if (Input.IsKeyDown(Key.S)) { player.Position.Y += MovementSpeed; }
            if (Input.IsKeyDown(Key.A)) { player.Position.X -= MovementSpeed; }

            Sprite2D ground = player.IsColliding("ground");
            if(ground != null)
            {
                List<Sprite2D.Direction> directions = player.GetCollisionDirections(ground);
                foreach(Sprite2D.Direction direction in directions)
                {
                    switch (direction)
                    {
                        case Sprite2D.Direction.Left:
                            player.Position.X = lastPos.X;
                            break;
                        case Sprite2D.Direction.Right:
                            player.Position.X = lastPos.X;
                            break;
                        case Sprite2D.Direction.Up:
                            player.Position.Y = lastPos.Y;
                            break;
                        case Sprite2D.Direction.Down:
                            player.Position.Y = lastPos.Y;
                            break;

                    }
                }
            }
            else
            {
                lastPos.X = player.Position.X;
                lastPos.Y = player.Position.Y;
            }


        }

        public override void GetKeyDown(KeyEventArgs e)
        {
           
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
  
        }
        public override void FormClosing(FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }
    }
}
