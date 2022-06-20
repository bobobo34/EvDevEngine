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
namespace EvDevEngine
{
    public class GameState : State
    {
        public SpriteFont Font;
        public BackgroundOcean Ocean;
        //private Random random = new Random();
        public GameState(EvDevEngine.EvDevEngine game) : base("GameScreen", game)
        {

        }
        public override void Load()
        {
            game.BackgroundColor = Color.LightBlue;




            Sprite2D OceanSprite = new Sprite2D(game, Vector2.Zero(), new Vector2(960f, 1080f), "OceanSprite", "BCK");
            Ocean = new BackgroundOcean("BCK", OceanSprite, game, true);
            AddObject(Ocean);

            Sprite2D KWSprite = new Sprite2D(game, new Vector2(100, 0), 3f, "KillerWhale", "KW", true);
            var KW = new KillerWhale("TKW", KWSprite, game);
            AddObject(KW);
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
        }
        public override void ResizeAll(Vector2 oldScreenSize, Vector2 newScreenSize)
        {
            
            
        }
    }
}