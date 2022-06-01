using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
//using OpenTK.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EvDevEngine.EvDevEngine
{
    public class Canvas : Game
    {
        public GraphicsDeviceManager graphics = null;
        public Action InitFunc = null;
        public Action LoadFunc = null;
        public Action UnloadFunc = null;
        public Action<GameTime> UpdateFunc = null;
        public Action<GameTime> DrawFunc = null;
        public SpriteBatch sprites;
        public Canvas(Action InitFunc, Action LoadFunc, Action UnloadFunc, Action<GameTime> UpdateFunc, Action<GameTime> DrawFunc) : base() 
        {
            this.InitFunc = InitFunc;
            this.LoadFunc = LoadFunc;
            this.UnloadFunc = UnloadFunc;
            this.UpdateFunc = UpdateFunc;
            this.DrawFunc = DrawFunc;
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsFixedTimeStep = true;
            this.IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            InitFunc?.Invoke();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            this.sprites = new SpriteBatch(this.GraphicsDevice);
            LoadFunc?.Invoke();
            base.LoadContent();
        }
        protected override void UnloadContent()
        {
            UnloadFunc?.Invoke();
            base.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            UpdateFunc?.Invoke(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(EvDevEngine.BackgroundColor);
            this.sprites.Begin();
            foreach(Sprite2D sprite in EvDevEngine.AllSprites)
            {
                Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, (int)sprite.Scale.X, (int)sprite.Scale.Y);
                this.sprites.Draw(sprite.Sprite, rectangle, Microsoft.Xna.Framework.Color.White);
            }
            DrawFunc?.Invoke(gameTime);
            this.sprites.End();
            base.Draw(gameTime);
        }
    }
    public abstract class EvDevEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title;
        public Canvas Window = null;
        public Thread GameLoopThread = null;
        public static KeyboardState Input;
        public bool UpdateObjectsBefore = true;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Object2D> AllObjects = new List<Object2D>();
        
        public static Microsoft.Xna.Framework.Color BackgroundColor = Microsoft.Xna.Framework.Color.White;

        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;
        public Vector2 CameraZoom = new Vector2(1, 1);
        
        public EvDevEngine(Vector2 ScreenSize, string Title)
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas(null, Load, Unload, UpdateAll, Draw);

            Window.Run();
            //GameLoopThread = new Thread(GameLoop);
            //GameLoopThread.Start();

        }


     
        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        public static void UnregisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void UnregisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }
        //void GameLoop()
        //{
        //    Load();

        //    while (GameLoopThread.IsAlive)
        //    {
        //        try
        //        {
        //            Draw();
        //            Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
        //            Input = Keyboard.GetState();
        //            UpdateAll();
        //            Thread.Sleep(2);
        //        } catch
        //        {
        //            Log.Error("Window has not been found...");
        //        }
        //    }
        //}

        private void UpdateAll(GameTime gameTime)
        {
            Input = Keyboard.GetState();
            if (UpdateObjectsBefore)
            {
                foreach (Object2D obj in AllObjects)
                {
                    foreach (Component child in obj.Children)
                    {   
                        child.OnUpdate(gameTime);
                    }
                }
                Update(gameTime);
            }
            else
            {
                Update(gameTime);
                foreach (Object2D obj in AllObjects)
                {
                    foreach (Component child in obj.Children)
                    {
                        child.OnUpdate(gameTime);
                    }
                }
            }
        }
        
        //private void Renderer(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    g.Clear(BackgroundColor);
        //    g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
        //    g.RotateTransform(CameraAngle);
        //    g.ScaleTransform(CameraZoom.X, CameraZoom.Y);
        //    try
        //    {
        //        foreach(Shape2D shape in AllShapes)
        //        {
        //            g.FillRectangle(new SolidBrush(System.Drawing.Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
        //        }
        //        foreach(Sprite2D sprite in AllSprites)
        //        {
        //            g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
        //        }
        //    } catch { Log.Warning("Images are still being processed..."); }

        //}
        
        public abstract void Load();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public abstract void Unload();


    }
}
