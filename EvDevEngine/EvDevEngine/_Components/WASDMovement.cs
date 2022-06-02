using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EvDevEngine.EvDevEngine._Components
{
    public class WASDMovement : Component
    {
        public float MovementSpeed = 6.5f;
        public float TrueMovementSpeed(GameTime gameTime)
        {
            return (float)Math.Pow(MovementSpeed, 3) * (float)gameTime.ElapsedGameTime.TotalSeconds;          
        }
        public WASDMovement(Object2D Parent, float MovementSpeed)
        {
            this.Parent = Parent;
            this.MovementSpeed = MovementSpeed;
        }
        public WASDMovement(Object2D Parent)
        {
            this.Parent=Parent;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Collider2D collider = Parent.GetComponent<Collider2D>();
            
            if (EvDevEngine.Input.IsKeyDown(Keys.W))
            {
                if (collider != null)
                {
                    Vector2 newPos = new Vector2(Parent.Sprite.Position.X, Parent.Sprite.Position.Y - TrueMovementSpeed(gameTime));
                    Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                    if (collidewith != null)
                    {
                        Log.Info("colliding with up");
                        Parent.Sprite.Position.Y = collidewith.Max.Y;
                    }
                    else { Parent.Sprite.Position.Y -= TrueMovementSpeed(gameTime); }
                }
                else { Parent.Sprite.Position.Y -= TrueMovementSpeed(gameTime); }
            }
            if (EvDevEngine.Input.IsKeyDown(Keys.D)) 
            {
                if (collider != null)
                {

                    Vector2 newPos = new Vector2(Parent.Sprite.Position.X + TrueMovementSpeed(gameTime), Parent.Sprite.Position.Y);
                    Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                    if (collidewith != null)
                    {
                        Log.Info("colliding with right");
                        Parent.Sprite.Position.X = collidewith.Min.X - Parent.Sprite.Scale.X;
                    }
                    else { Parent.Sprite.Position.X += TrueMovementSpeed(gameTime); }
                }
                else { Parent.Sprite.Position.X += TrueMovementSpeed(gameTime); }
            }
            if (EvDevEngine.Input.IsKeyDown(Keys.S)) 
            {
                if (collider != null)
                {
                    Vector2 newPos = new Vector2(Parent.Sprite.Position.X, Parent.Sprite.Position.Y + TrueMovementSpeed(gameTime));
                    Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                    if (collidewith != null)
                    {
                        Log.Info("colliding with down");
                        Parent.Sprite.Position.Y = collidewith.Min.Y - Parent.Sprite.Scale.Y;
                    }
                    else { Parent.Sprite.Position.Y += TrueMovementSpeed(gameTime); }
                }
                else { Parent.Sprite.Position.Y += TrueMovementSpeed(gameTime); }
            }
            if (EvDevEngine.Input.IsKeyDown(Keys.A)) 
            {
                if (collider != null)
                {
                    
                    Vector2 newPos = new Vector2(Parent.Sprite.Position.X - TrueMovementSpeed(gameTime), Parent.Sprite.Position.Y);
                    Sprite2D collidewith = collider.WillCollide(newPos, Parent.Sprite.Scale);
                    if (collidewith != null)
                    {
                        Log.Info("colliding with left");
                        Parent.Sprite.Position.X = collidewith.Max.X;
                    }
                    else { Parent.Sprite.Position.X -= TrueMovementSpeed(gameTime); }
                }
                else { Parent.Sprite.Position.X -= TrueMovementSpeed(gameTime); }
            }
        }

        
    }
}
