using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static EvDevEngine.EvDevEngine.XNAfuncs;
using static EvDevEngine.EvDevEngine.Engine;
using Vector2 = EvDevEngine.EvDevEngine.Vector2;
using MonoGame.Extended.Tweening;
namespace EvDevEngine
{
    public class TitleState : State
    {
        public SpriteFont Font;

        public BouncingFont TitleText;
        public TitleKillerWhale KW;
        public Sun sun;
        public BackgroundOcean Ocean;
        bool MovingDown = false;
        //private Random random = new Random();
        public TitleState(EvDevEngine.EvDevEngine game) : base("StartScreen", game)
        {

        }
        private void StartClick()
        {
            MovingDown = true;
        }
        private void OptionsClick()
        {

        }
        public override void Load()
        {
            BackgroundColor = Color.LightBlue;
            Font = game.GetFont(Fonts.Pixelated);
            TitleText = new BouncingFont(Font, "Killer Whale Mania!", new Vector2(game.ScreenCenter().X, game.ScreenCenter().Y - 200), game, 0.001f, 0.05f, 0.1f, 8f) { Scale = 1.5f };

            Sprite2D SunSprite = new(game, Vector2.Zero(), new Vector2(140, 140), "SunSprite", "Sun");
            sun = new Sun("Sun", SunSprite, game);
            AddObject(sun);

            Sprite2D KWSprite = new Sprite2D(game, new Vector2(game.ScreenCenter().X, game.ScreenCenter().Y + 150), new Vector2(227.5f, 105f), "KillerWhale", "KW", true) { layerDepth = 1f };
            KW = new TitleKillerWhale("TKW", KWSprite, game);      
            AddObject(KW);

            Sprite2D OceanSprite = new Sprite2D(game, Vector2.Zero(), new Vector2(960f, 1080f), "FullOceanSprite", "BCK");
            Ocean = new BackgroundOcean("BCK", OceanSprite);
            AddObject(Ocean);

            BouncingFont StartButtonFont = new BouncingFont(Font, "Start", game, 0.001f, 0.05f, 0.1f, 5f);
            NormalButton StartButton = new NormalButton(game, StartButtonFont, new Vector2(8, 8), new Vector2(game.ScreenCenter().X - 250, game.ScreenCenter().Y - 75), true);
            StartButtonFont.Position = new Vector2(StartButton.Center);
            StartButton.OnClick = StartClick;
            AddUI(StartButton);

            BouncingFont OptionsButtonFont = new BouncingFont(Font, "Options", game, 0.001f, 0.05f, 0.1f, 5f);
            NormalButton OptionsButton = new NormalButton(game, OptionsButtonFont, new Vector2(8, 8), new Vector2(game.ScreenCenter().X + 250, game.ScreenCenter().Y - 75), true);
            OptionsButtonFont.Position = new Vector2(OptionsButton.Center);
            OptionsButton.OnClick = OptionsClick;
            AddUI(OptionsButton);

            base.Load();
        }

        public override void Update(GameTime gameTime)
        {
            ///TODO: Add random kw jumps
            ///

            if (Camera.Position.Y >= 540) { game.SetState<GameState>(); return; }
            if (MovingDown)
            {
                float i = (-0.01f * (Math.Abs(Camera.Position.Y - 270)) / 270 + 0.02f) * 1.25f;
                Vector2 vec = new Vector2(0, Lerp(0, 540, i));

                if (Camera.Position.Y + vec.Y >= 540) vec.Y = 540 - Camera.Position.Y;
                Camera.Move(Vec2(vec));
            }
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            TitleText.DrawSelf();
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