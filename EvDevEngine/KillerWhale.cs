using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using EvDevEngine.EvDevEngine._Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = EvDevEngine.EvDevEngine.Vector2;

namespace EvDevEngine
{
    public class KillerWhale : Object2D
    {
        public Vector2 StartingPosition;
        public float Speed;

        public KillerWhale(string ID, Sprite2D sprite, Game game) : base(ID, sprite)
        {
            //Animation2D swim = new Animation2D(game.Content.Load<Texture2D>("KillerWhaleSwim"), 65, 30 10, , true);
        }
        //public bool IsSwimming()
        //{
        //    return true
        //}
    }
}
