using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace EvDevEngine.EvDevEngine.UIElements
{
    public interface IUIElement
    {
        Texture2D BackgroundImage { get; set; }
        Rectangle BackgroundRectangle { get; } 
        EvDevEngine game { get; set; }
        Vector2 Position { get; set; }
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}
