using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using EvDevEngine.EvDevEngine._Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static EvDevEngine.EvDevEngine.XNAfuncs;
using static EvDevEngine.EvDevEngine.Engine;

namespace EvDevEngine
{
    public class Player : Object2D
    {
        public Sprite2D Helmet;
        public Sprite2D Gun;
        public Player(string ID, Sprite2D sprite, EvDevEngine.EvDevEngine game) : base(ID, sprite)
        {
            var guncontroller = new GunController(this);
            AddComponent(guncontroller);
        }
    }
    internal class GunController : Component
    {
        new Player Parent;
        public GunController(Player parent) { this.Parent = parent; }
        public override void OnUpdate(GameTime gameTime)
        {
            var direction = MousePos - Parent.Sprite.Position;
            if (direction.X < 0) { Parent.Gun.Flipped = SpriteEffects.FlipVertically; Parent.Helmet.Flipped = SpriteEffects.FlipHorizontally; Parent.Sprite.Flipped = SpriteEffects.FlipHorizontally; }
            else { Parent.Gun.Flipped = SpriteEffects.None; Parent.Helmet.Flipped = SpriteEffects.None; Parent.Sprite.Flipped = SpriteEffects.None; }
            Parent.Gun.Rotation = (float)MathHelper.ToDegrees((float)Math.Atan2(direction.Y, direction.X));
            base.OnUpdate(gameTime);
        }
    }
}
