using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static EvDevEngine.EvDevEngine.XNAfuncs;
using Vector2 = EvDevEngine.EvDevEngine.Vector2;
using static EvDevEngine.EvDevEngine.Engine;
namespace EvDevEngine
{
    public class GameState : State
    {
        public SpriteFont Font;
        public BackgroundOcean Ocean;
        public KillerWhaleManager Manager;
        public Player player;
       
        //private Random random = new Random();
        public GameState(EvDevEngine.EvDevEngine game) : base("GameScreen", game)
        {

        }
        public override void Load()
        {
            BackgroundColor = Color.LightBlue;



            Log.Info("ocean");
            Sprite2D OceanSprite = new Sprite2D(game, Vector2.Zero(), new Vector2(960f, 540f), "OceanSprite", "BCK") { layerDepth = 0f };
            Ocean = new BackgroundOcean("BCK", OceanSprite, true);
            AddObject(Ocean);
            
            Log.Info("player");

            var playersprite = new Sprite2D(game, game.ScreenCenter(), 1f, "PlayerBody", "player", true);
            Log.Info("helmet");

            var helmetsprite = new Sprite2D(game, new Vector2(playersprite.Position.X, playersprite.Position.Y - 70f), 1f, "DefaultHelmet", "helmet", true) { layerDepth = 2f };
            Log.Info("gunj");

            var gunsprite = new Sprite2D(game, new Vector2(playersprite.Position.X, playersprite.Position.Y - 50), 1f, "DefaultGun", "gun", true) { layerDepth = 3f, NewOrigin = new Vector2(5, 5) };
            player = new Player("player", playersprite, game) { Gun = gunsprite, Helmet = helmetsprite };
            AddObject(player);
            Log.Info("manager");

            Manager = new KillerWhaleManager("KWM");
            AddObject(Manager);
            //Sprite2D KWSprite = new Sprite2D(game, new Vector2(100, 0), 3f, "KillerWhale", "KW", true);
            //var KW = new KillerWhale("TKW", KWSprite, game);
            //AddObject(KW);
            base.Load();
        }

        public override void Update(GameTime gameTime)
        {
            ///TODO: Add random kw jumps
            ///
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            player.Helmet.DrawSelf();
            player.Gun.DrawSelf();
        }
        public override void ResizeAll(Vector2 oldScreenSize, Vector2 newScreenSize)
        {
            
            
        }
    }
}