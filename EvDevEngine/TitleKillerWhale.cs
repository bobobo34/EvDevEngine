using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EvDevEngine.EvDevEngine._Components;


namespace EvDevEngine
{
    public class TitleKillerWhale : Object2D
    {
        public TitleKillerWhale(string ID, Sprite2D sprite, EvDevEngine.EvDevEngine game) : base(ID, sprite)
        {
            //TKWMove movement = new TKWMove();

            //AddComponent(movement);
            //Animation2D swim = new Animation2D(game.Content.Load<Texture2D>("KillerWhaleSwim"), 65, 30, 10, IsSwimming, true);
            //AddComponent(swim);
        }
        bool IsSwimming()
        {
            return GetComponent<TKWMove>().Moving;
        }
    }
    internal class TKWMove : Movement
    {
        public TKWMove()
        {
            Name = "TKWMovement";
        }
        public override void OnUpdate(GameTime gameTime)
        {
            //Random random = new Random();
            //MovementSpeed = random.Next(-100, 100) / 20;
            //BeginMove(Direction.Up, gameTime);
            //MovementSpeed = 5f;
            //BeginMove(Direction.Right, gameTime);
        }
    }
}
