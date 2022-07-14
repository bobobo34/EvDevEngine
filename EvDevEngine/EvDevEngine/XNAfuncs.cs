using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static EvDevEngine.EvDevEngine.Engine;

namespace EvDevEngine.EvDevEngine
{
    public static class XNAfuncs
    {
        public static float ANTIROTATION = (float)(360 / Math.PI);
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
            return new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }
        public static bool IsClicked(Rectangle rectangle, bool rightmouse, ref bool ButtonDown)
        {
            //if mouse button is clicked on button
            ButtonState but;
            if (rightmouse) but = MouseInput.RightButton;
            else but = MouseInput.LeftButton;

            if (but == ButtonState.Released)
            {
                ButtonDown = false;
            }
            if (but == ButtonState.Pressed && rectangle.Contains(MousePos.X, MousePos.Y) && !ButtonDown)
            {
                ButtonDown = true;
                return true;
            }
            return false;
        }
        public static List<Texture2D> SpliceSpriteSheet(Texture2D spritesheet, int width, int height)
        {
            List<Texture2D> textures = new List<Texture2D>();
            for(int x = 0; x < spritesheet.Width; x += width)
            {
                for(int y = 0; y < spritesheet.Height; y += height)
                {
                    textures.Add(spritesheet.CreateTexture(new Rectangle(x, y, width, height)));
                }
            }
            return textures;
        }
        
            /// <summary>
            /// Creates a new texture from an area of the texture.
            /// </summary>
            /// <param name="graphics">The current GraphicsDevice</param>
            /// <param name="rect">The dimension you want to have</param>
            /// <returns>The partial Texture.</returns>
        public static Texture2D CreateTexture(this Texture2D src, Rectangle rect)
        {
            Texture2D tex = new Texture2D(Engine.GraphicsDevice, rect.Width, rect.Height);
            int count = rect.Width * rect.Height;
            Color[] data = new Color[count];
            src.GetData(0, rect, data, 0, count);
            tex.SetData(data);
            return tex;
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
            return new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
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
            return (float)Math.PI / 360 * degrees;
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
        public static float ClosestTo(float curr, float op1, float op2)
        {
            return (Math.Abs(op1 - curr) < Math.Abs(op2 - curr)) ? op1 : op2;
        }
        public static int ClosestMultiple(int val, int multiple)
        {
            int rem = val % multiple;
            int result = val - rem;
            if (rem >= (multiple / 2))
                result += multiple;
            return result;
        }
    } 
}
