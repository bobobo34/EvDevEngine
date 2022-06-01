using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace EvDevEngine.EvDevEngine
{
    public enum Components
    {
        Collider2D,
        WASDMovement
    }
    public abstract class Component
    {
        public string Name;
        public Object2D Parent;
        public abstract void OnLoad();
        public abstract void OnDraw(GameTime gameTime);

        public abstract void OnUpdate(GameTime gameTime);
        public abstract void Destroy();

    }
}
