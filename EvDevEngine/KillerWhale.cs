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
using static EvDevEngine.EvDevEngine.Engine;

namespace EvDevEngine
{
    public class KillerWhale : Object2D
    {
        public Vector2 StartingPosition;

        public KillerWhale(string ID, Sprite2D sprite, EvDevEngine.EvDevEngine game, float speed) : base(ID, sprite)
        {
            Animation2D swim = new Animation2D(game.Content.Load<Texture2D>("KillerWhaleSwim"), 65, 30, 10, () => { return true; }, true);
            AddComponent(swim);
            var move = new MoveToCenter(game, sprite.Position, speed);
            AddComponent(move);
        }
        
    }
    public class MoveToCenter : Movement
    {
        public float frequency = 1f;
        public float magnitude = 0.25f;
        public float angle;
        public EvDevEngine.EvDevEngine game;
        private Vector2 originalpos;
        public MoveToCenter(EvDevEngine.EvDevEngine game, Vector2 position, float speed)
        {
            MovementSpeed = speed;
            originalpos = position;
            this.game = game;
            Log.Info(originalpos.X);
            Log.Info(originalpos.Y);
            var direction = game.ScreenCenter() - originalpos;
            angle = (float)(Math.Atan2(direction.Y, direction.X) * XNAfuncs.ANTIROTATION);
            Log.Info(angle);
        }                                                         
        int times = 1;
        public override void OnUpdate(GameTime gameTime)
        {
            //todo: sine wave movement
            //angle += (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * frequency) * magnitude;
            Parent.Sprite.Rotation = angle;
            Parent.Sprite.Position = XNAfuncs.Lerp(originalpos, game.ScreenCenter(), MovementSpeed * times);
            times++;


            if (Parent.Sprite.Position.Rounded().Equals(game.ScreenCenter())) { Parent.UnRegisterObject(); Parent = null; }
            //base.OnUpdate(gameTime);
        }
    }
}
