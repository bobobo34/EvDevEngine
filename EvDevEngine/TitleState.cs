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
    public class TitleState : State
    {
        public SpriteFont Font;
        private float FontRotationFactor = 0.1f;
        private float FontScaleFactor = 0.001f;
        public FontText TitleText;
        public TitleKillerWhale KW;

        public TitleState(EvDevEngine.EvDevEngine game) : base("StartScreen", game)
        {

        }
        public override void Load()
        {
            game.BackgroundColor = Color.LightBlue;
            Font = game.GetFont(Fonts.Pixelated);
            TitleText = new FontText(Font, "Killer Whale Mania!", new Vector2(game.ScreenCenter().X, game.ScreenCenter().Y - 200), game);
            TitleText.Scale = 1.5f;
            Sprite2D KWSprite = new Sprite2D(game, game.ScreenCenter(), new Vector2(1f, 1f), "KillerWhale", "KW");
            KW = new TitleKillerWhale("TKW", KWSprite, game);
            AddObject(KW);
            Log.Info("Loading");

        }


        public override void Update(GameTime gameTime)
        {
            if ((float)Math.Round(TitleText.Scale, 2) == 1.55f) FontScaleFactor = -0.001f;
            else if ((float)Math.Round(TitleText.Scale, 2) == 1.45f) FontScaleFactor = 0.001f;
            if (Math.Round(TitleText.Rotation) == 8f) FontRotationFactor = -0.1f;
            else if (Math.Round(TitleText.Rotation) == -8f) FontRotationFactor = 0.1f;

            TitleText.Rotation += FontRotationFactor;
            TitleText.Scale += FontScaleFactor;   
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            //Rectangle rec = new Rectangle((int)game.ScreenCenter().X, (int)game.ScreenCenter().Y, 10, 10);
            //game.sprites.Draw(game.Content.Load<Texture2D>("test"), rec, Color.Black);
            TitleText.DrawSelf();
            //game.sprites.DrawString(Font, "Killer Whale Mania!", Vec2(new Vector2(game.ScreenCenter().X, game.ScreenCenter().Y - 200)), Color.Black, GetRotation(FontRotation), Vec2(Origin), FontScale, SpriteEffects.None, 1);
            base.Draw(gameTime);
        }
        public override void ResizeAll(Vector2 oldScreenSize, Vector2 newScreenSize)
        {
            TitleText.ChangeSize(oldScreenSize, newScreenSize);
        }
    }
}