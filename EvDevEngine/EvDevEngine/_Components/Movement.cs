using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine.EvDevEngine._Components
{
    public class Movement : Component
    {
        public bool Moving = false;
        public float MovementSpeed = 6.5f;
        public float TrueMovementSpeed(GameTime gameTime)
        {
            return MovementSpeed * 45 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public void BeginMove(Direction direction, GameTime gameTime)
        {
            var collider = Parent.GetComponent<Collider2D>();
            Moving = true;
            switch(direction)
            {
                case Direction.Up:
                    if (collider != null)
                    {
                        Vector2 newPos = new Vector2(Parent.Sprite.Position.X, Parent.Sprite.Position.Y - TrueMovementSpeed(gameTime));
                        Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                        if (collidewith != null)
                        {
                            Parent.Sprite.Position.Y = collidewith.Max.Y;
                        }
                        else { Parent.Sprite.Position.Y -= TrueMovementSpeed(gameTime); }
                    }
                    else { Parent.Sprite.Position.Y -= TrueMovementSpeed(gameTime); }
                    break;
                case Direction.Down:
                    if (collider != null)
                    {
                        Vector2 newPos = new Vector2(Parent.Sprite.Position.X, Parent.Sprite.Position.Y + TrueMovementSpeed(gameTime));
                        Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                        if (collidewith != null)
                        {
                            Parent.Sprite.Position.Y = collidewith.Min.Y - Parent.Sprite.Scale.Y;
                        }
                        else { Parent.Sprite.Position.Y += TrueMovementSpeed(gameTime); }
                    }
                    else { Parent.Sprite.Position.Y += TrueMovementSpeed(gameTime); }
                    break;
                case Direction.Left:
                    Parent.Sprite.Flipped = SpriteEffects.FlipHorizontally;
                    if (collider != null)
                    {

                        Vector2 newPos = new Vector2(Parent.Sprite.Position.X - TrueMovementSpeed(gameTime), Parent.Sprite.Position.Y);
                        Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                        if (collidewith != null)
                        {
                            Parent.Sprite.Position.X = collidewith.Max.X;
                        }
                        else { Parent.Sprite.Position.X -= TrueMovementSpeed(gameTime); }
                    }
                    else { Parent.Sprite.Position.X -= TrueMovementSpeed(gameTime); }
                    break;
                case Direction.Right:
                    Parent.Sprite.Flipped = SpriteEffects.None;
                    if (collider != null)
                    {

                        Vector2 newPos = new Vector2(Parent.Sprite.Position.X + TrueMovementSpeed(gameTime), Parent.Sprite.Position.Y);
                        Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                        if (collidewith != null)
                        {
                            Parent.Sprite.Position.X = collidewith.Min.X - Parent.Sprite.Scale.X;
                        }
                        else { Parent.Sprite.Position.X += TrueMovementSpeed(gameTime); }
                    }
                    else { Parent.Sprite.Position.X += TrueMovementSpeed(gameTime); }
                    break;             
            }
            
        }
        public void EndMove()
        {
            Moving = false;
        }
    }
}
