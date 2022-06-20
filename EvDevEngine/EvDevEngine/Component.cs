using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace EvDevEngine.EvDevEngine
{
  
    public abstract class Component
    {
        public string Name;
        public Object2D Parent;
        public virtual void OnLoad() { }
        public virtual void OnDraw(GameTime gameTime) { }

        public virtual void OnUpdate(GameTime gameTime) { }
        public virtual void Destroy() 
        {
            Parent.Children.Remove(this);
        }

    }
}
