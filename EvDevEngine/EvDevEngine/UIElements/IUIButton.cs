using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EvDevEngine.EvDevEngine.UIElements
{
    public interface IUIButton : IUIElement
    {
        string Text { get; set; }
        Vector2 Scale { get; set; }
        SpriteFont Font { get; set; }
        void OnClick();
    }
}
