using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine
{
    public class Sun : Object2D
    {
        public Sun(string ID, Sprite2D sprite, EvDevEngine.EvDevEngine game) : base(ID, sprite)
        {
            Animation2D anim = new Animation2D(game.Content.Load<Texture2D>("SunAnimation"), 14, 14, 300, () => { return true; }, true);
            AddComponent(anim);
        }
    }
}
