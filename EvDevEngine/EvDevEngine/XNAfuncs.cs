using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace EvDevEngine.EvDevEngine
{
    public class XNAfuncs
    {
        public static List<Microsoft.Xna.Framework.Rectangle> SpliceSpriteSheet(Texture2D spritesheet, int width, int height)
        {
            List<Microsoft.Xna.Framework.Rectangle> rectangles = new List<Microsoft.Xna.Framework.Rectangle>();
            Log.Info($"{spritesheet.Width}, {spritesheet.Height} - {width}, {height}");
            for(int x = 0; x < spritesheet.Width; x += width)
            {
                for(int y = 0; y < spritesheet.Height; y += height)
                {
                    rectangles.Add(new Microsoft.Xna.Framework.Rectangle(x, y, width, height));
                }
            }
            return rectangles;
        }
        /// <summary>
        /// Converts EvDevEngine vector2 to XNA vector2 for use in things like rectangles.
        /// </summary>
        /// <param name="vec">EvDevEngine vector2</param>
        /// <returns>XNA vector2</returns>
        public static Microsoft.Xna.Framework.Vector2 Vec2(Vector2 vec)
        {
            return new Microsoft.Xna.Framework.Vector2(vec.X, vec.Y);
        }
    } 
}
