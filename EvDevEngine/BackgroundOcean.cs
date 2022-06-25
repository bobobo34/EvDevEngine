using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework.Graphics;
using static EvDevEngine.EvDevEngine.Engine;

namespace EvDevEngine
{
    public class BackgroundOcean : Object2D
    {
        public BackgroundOcean(string ID, Sprite2D sprite, bool small = false) : base(ID, sprite)
        {
            sprite.layerDepth = LayerDepth.Background;
            Animation2D animation;
            if(small) animation = new Animation2D(game.Content.Load<Texture2D>("Ocean"), 128, 72, 100, () => { return true; }, true);
            else animation = new Animation2D(game.Content.Load<Texture2D>("FullOcean"), 128, 144, 100, () => { return true; }, true);
            AddComponent(animation);
        }
    }
}
