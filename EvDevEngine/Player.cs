using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using EvDevEngine.EvDevEngine._Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine
{
    public class Player : Object2D
    {
        public Sprite2D Helmet;
        public Sprite2D Gun;
        public Player(string ID, Sprite2D sprite, EvDevEngine.EvDevEngine game) : base(ID, sprite)
        {
            
        }
    }
    internal class GunController
}
