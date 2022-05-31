using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using EvDevEngine.EvDevEngine._Components;

namespace EvDevEngine
{
    public class Player : Object2D
    {
        public Player(string ID, Sprite2D sprite) : base(ID, sprite)
        {
            var movement = new WASDMovement(this);
            var collider = new Collider2D("ground", this);

            AddComponent(movement);
            AddComponent(collider);

        }


    }
}
