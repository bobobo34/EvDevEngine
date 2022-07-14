using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EvDevEngine.EvDevEngine.Engine;
using Vector2 = EvDevEngine.EvDevEngine.Vector2;
using System.Timers;
using System.Threading;

namespace EvDevEngine
{
    public class KillerWhaleManager : Object2D
    {
        public KillerWhaleManager(string ID) : base(ID)
        {
            var spawner = new WhaleSpawner();
            AddComponent(spawner);
        }
        
    }
    internal class WhaleSpawner : Component
    {
        float difficulty = 0;
        Random random = new Random();
        Sprite2D KWSprite = new Sprite2D(Engine.game, "KillerWhale");
        System.Timers.Timer timer = new System.Timers.Timer(2000f);
        ManualResetEvent spritecreated = new ManualResetEvent(false);
        
        public WhaleSpawner() { }

        public override void OnLoad()
        {
            Task.Run(() => Spawner());
            base.OnLoad();
        }
        private void Spawner()
        {
            timer.Elapsed += (s, e) =>
            {
                SpawnKW();
            };
            timer.Start();
        }
        private void SpawnKW()
        {
            var x = random.Next(-400, Width + 400);
            var y = random.Next(-400, Height + 400);
            if (x > -200 && x < Width + 200)
            {
                var miny = random.Next(-400, -200);
                var maxy = random.Next(Height + 200, Height + 400);
                y = (int)XNAfuncs.ClosestTo(y, miny, maxy);
            }
            Sprite2D sprite = null;
            game.EnqueueAction(() =>
            {
                sprite = new Sprite2D(new Vector2(x, y), 1f, KWSprite, "kw", true) { layerDepth = 5 };
                if (x > game.ScreenCenter().X) sprite.Flipped = SpriteEffects.FlipVertically;
                CurrentState.AddObject(new KillerWhale("kw", sprite, game, 0.005f + difficulty / 100000));
                spritecreated.Set();
            });
            spritecreated.WaitOne();
            spritecreated.Reset();
            
        }
        public override void OnUpdate(GameTime gameTime)
        {
            if (Updates % 1000 == 0)
            {
                difficulty += 50f;
                if(!(timer.Interval - difficulty < 0)) timer.Interval -= difficulty;
                
            }
            
            base.OnUpdate(gameTime);
        }
    }
}