using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine.EvDevEngine.UIElements
{
    public interface IAnimatedUIButton : IUIButton
    {
        Rectangle AfterClickRectange { get; set; }
        Texture2D AfterClickTexture { get; set; }
    }
}
