using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EvDevEngine.EvDevEngine
{
    public static class XNAfuncs
    {
        public static float Lerp(float min, float max, float by)
        {
            return min + (max - min) * by;
        }
        public static Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }
        public static Rectangle ScreenRectangle(this EvDevEngine game)
        {
            return new Rectangle(0, 0, game.graphics.PreferredBackBufferWidth, game.graphics.PreferredBackBufferHeight);
        }
        public static bool IsClicked(Rectangle rectangle, bool rightmouse, ref bool ButtonDown)
        {
            //if mouse button is clicked on button
            ButtonState but;
            if (rightmouse) but = EvDevEngine.MouseInput.RightButton;
            else but = EvDevEngine.MouseInput.LeftButton;

            if (but == ButtonState.Released)
            {
                ButtonDown = false;
            }
            if (but == ButtonState.Pressed && rectangle.Contains(EvDevEngine.MousePos.X, EvDevEngine.MousePos.Y) && !ButtonDown)
            {
                ButtonDown = true;
                return true;
            }
            return false;
        }
        public static List<Rectangle> SpliceSpriteSheet(Texture2D spritesheet, int width, int height)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            for(int x = 0; x < spritesheet.Width; x += width)
            {
                for(int y = 0; y < spritesheet.Height; y += height)
                {
                    rectangles.Add(new Rectangle(x, y, width, height));
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
        public static Vector2 Vec2(Microsoft.Xna.Framework.Vector2 vec)
        {
            return new Vector2(vec.X, vec.Y);
        }

        public static Rectangle ScaledRectangle(Rectangle original, Vector2 scale)
        {
            int height = (int)(original.Height * scale.Y);
            int width = (int)(original.Width * scale.X);
            return new Rectangle(original.X, original.Y, width, height);
        }
        public static Vector2 ScreenCenter(this EvDevEngine game)
        {
            return new Vector2(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 2);
        }
        public static Vector2 CenterDrawPoint(SpriteFont font, string str, Rectangle buttonrec)
        {
            Vector2 stringsize = Vec2(font.MeasureString(str));
            float X = (buttonrec.Width - stringsize.X) / 2 + buttonrec.X;
            float Y = (buttonrec.Height - stringsize.Y) / 2 + buttonrec.Y;
            return new Vector2(X, Y);
        }

        public static Vector2 CenterDrawPoint(SpriteFont font, string str, Rectangle buttonrec, float scale)
        {
            Vector2 stringsize = Vec2(font.MeasureString(str));
            float X = (buttonrec.Width - stringsize.X * scale) / 2 + buttonrec.X;
            float Y = (buttonrec.Height - stringsize.Y * scale) / 2 + buttonrec.Y;
            return new Vector2(X, Y);
        }
        public static float GetRotation(float degrees)
        {
            return (((float)Math.PI / 360) * degrees);
        }
        //change based on game
        public static SpriteFont GetFont(this EvDevEngine game, Fonts font)
        {
            switch(font)
            {
                case Fonts.Pixelated:
                    return game.Content.Load<SpriteFont>("PixelFont");
                default:
                    throw new Exception();
            }
        }
    } 
}
