using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using EvDevEngine.EvDevEngine._Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = EvDevEngine.EvDevEngine.Vector2;

namespace EvDevEngine
{
    public class KillerWhale : Object2D
    {
        public Vector2 StartingPosition;
        public float Speed;

        public KillerWhale(string ID, Sprite2D sprite, EvDevEngine.EvDevEngine game) : base(ID, sprite)
        {
            Animation2D swim = new Animation2D(game.Content.Load<Texture2D>("KillerWhaleSwim"), 65, 30, 10, () => { return true; }, true);
            AddComponent(swim);
            var move = new MoveToCenter(game, sprite.Position);
            AddComponent(move);
        }
        
    }
    public class MoveToCenter : Movement
    {
        public float frequency = 1f;
        public float magnitude = 0.25f;
        public float angle;
        new public float MovementSpeed = 3f;
        public EvDevEngine.EvDevEngine game;
        private Vector2 originalpos;
        public MoveToCenter(EvDevEngine.EvDevEngine game, Vector2 position)
        {
            originalpos = position;
            this.game = game;
            angle = (float)(Math.Atan((Math.Abs(game.ScreenCenter().Y - position.Y)) / (Math.Abs(game.ScreenCenter().X - position.X))) * 180 / Math.PI * 2);
        }                                                         
        int times = 1;
        public override void OnUpdate(GameTime gameTime)
        {
            //todo: sine wave movement
            angle += (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * frequency) * magnitude;
            Parent.Sprite.Rotation = angle;
            Parent.Sprite.Position = XNAfuncs.Lerp(originalpos, game.ScreenCenter(), 0.007f * times);
            times++;


            if (Parent.Sprite.Position.Rounded().Equals(game.ScreenCenter())) { Parent.UnRegisterObject(); Parent = null; }
            //base.OnUpdate(gameTime);
        }
    }
}
