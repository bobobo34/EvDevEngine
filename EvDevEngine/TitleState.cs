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

        public BouncingFont TitleText;
        public TitleKillerWhale KW;
        public Sun sun;
        public BackgroundOcean Ocean;
        //private Random random = new Random();
        public TitleState(EvDevEngine.EvDevEngine game) : base("StartScreen", game)
        {

        }
        public override void Load()
        {
            game.BackgroundColor = Color.LightBlue;
            Font = game.GetFont(Fonts.Pixelated);
            TitleText = new BouncingFont(Font, "Killer Whale Mania!", new Vector2(game.ScreenCenter().X, game.ScreenCenter().Y - 200), game, 0.001f, 0.05f, 0.1f, 8f) { Scale = 1.5f };

            Sprite2D SunSprite = new Sprite2D(game, Vector2.Zero(), new Vector2(140, 140), "SunSprite", "Sun");
            sun = new Sun("Sun", SunSprite, game);
            AddObject(sun);

            Sprite2D KWSprite = new Sprite2D(game, new Vector2(game.ScreenCenter().X, game.ScreenCenter().Y + 275), new Vector2(227.5f, 105f), "KillerWhale", "KW", true);
            KW = new TitleKillerWhale("TKW", KWSprite, game);      
            AddObject(KW);

            Sprite2D OceanSprite = new Sprite2D(game, Vector2.Zero(), new Vector2(960f, 540f), "OceanSprite", "BCK");
            Ocean = new BackgroundOcean("BCK", OceanSprite, game);
            AddObject(Ocean);

            BouncingFont StartButtonFont = new BouncingFont(Font, "Start", game, 0.001f, 0.05f, 0.1f, 5f);
            NormalButton StartButton = new NormalButton(game, StartButtonFont, new Vector2(8, 8), new Vector2(game.ScreenCenter().X - 250, game.ScreenCenter().Y - 75), true);
            StartButtonFont.Position = new Vector2(StartButton.Center);
            AddUI(StartButton);

            BouncingFont OptionsButtonFont = new BouncingFont(Font, "Options", game, 0.001f, 0.05f, 0.1f, 5f);
            NormalButton OptionsButton = new NormalButton(game, OptionsButtonFont, new Vector2(8, 8), new Vector2(game.ScreenCenter().X + 250, game.ScreenCenter().Y - 75), true);
            OptionsButtonFont.Position = new Vector2(OptionsButton.Center);
            AddUI(OptionsButton);
        }


        public override void Update(GameTime gameTime)
        {
            ///TODO: Add random kw jumps
            ///

            //if ((float)Math.Round(TitleText.Scale, 2) == 1.55f) FontScaleFactor = -0.001f;
            //else if ((float)Math.Round(TitleText.Scale, 2) == 1.45f) FontScaleFactor = 0.001f;
            //if (Math.Round(TitleText.Rotation) == 8f) FontRotationFactor = -0.1f;
            //else if (Math.Round(TitleText.Rotation) == -8f) FontRotationFactor = 0.1f;

            //TitleText.Rotation += FontRotationFactor;
            //TitleText.Scale += FontScaleFactor;   
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
            
            KW.GetComponent<TKWMove>().MovementSpeed *= newScreenSize.X / oldScreenSize.X;
            KW.GetComponent<TKWMove>().frequency /= newScreenSize.X / oldScreenSize.X;
            KW.GetComponent<TKWMove>().magnitude *= newScreenSize.X / oldScreenSize.X;
        }
    }
}