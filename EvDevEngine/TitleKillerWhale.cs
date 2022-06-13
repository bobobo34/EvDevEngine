using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EvDevEngine.EvDevEngine._Components;
using Vector2 = EvDevEngine.EvDevEngine.Vector2;

namespace EvDevEngine
{
    public class TitleKillerWhale : Object2D
    {
        public TitleKillerWhale(string ID, Sprite2D sprite, EvDevEngine.EvDevEngine game) : base(ID, sprite)
        {
            TKWMove movement = new TKWMove(new Vector2(sprite.Position), game);

            AddComponent(movement);
            Animation2D swim = new Animation2D(game.Content.Load<Texture2D>("KillerWhaleSwim"), 65, 30, 20, IsSwimming, true);
            AddComponent(swim);
        }
        bool IsSwimming()
        {
            return GetComponent<TKWMove>().Moving;
        }
    }
    internal class TKWMove : Movement
    {
        public float frequency = 2f;
        public float magnitude = 0.5f;
        EvDevEngine.EvDevEngine game;
        public TKWMove(Vector2 original, EvDevEngine.EvDevEngine game)
        {
            Name = "TKWMovement";
            MovementSpeed = 3f;
            this.game = game;
            Moving = true;
        }
        public override void OnUpdate(GameTime gameTime)
        {
            if (Parent.Sprite.Min.X >= game.ScreenRectangle().Right)
            {
                Parent.Sprite.Position.X = game.ScreenRectangle().Left;
            }
            MoveRight(gameTime);
            Parent.Sprite.Rotation = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * frequency) * magnitude * 10;
        }
        public void MoveRight(GameTime gameTime)
        {
            
            Parent.Sprite.Position.X += TrueMovementSpeed(gameTime);
            Parent.Sprite.Position.Y += (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * frequency) * magnitude;
        }
    }
}
