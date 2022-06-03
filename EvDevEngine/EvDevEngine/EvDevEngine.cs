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
        public int Updates = 0;
        public Canvas(Action InitFunc, Action LoadFunc, Action UnloadFunc, Action<GameTime> UpdateFunc, Action<GameTime> DrawFunc) : base() 
        {
            this.InitFunc = InitFunc;
            this.LoadFunc = LoadFunc;
            this.UnloadFunc = UnloadFunc;
            this.UpdateFunc = UpdateFunc;
            this.DrawFunc = DrawFunc;
            this.graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
            graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
            
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
            Updates++;
            if (Updates == int.MaxValue) Updates = 7;
            UpdateFunc?.Invoke(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(EvDevEngine.BackgroundColor);
            this.sprites.Begin();
            foreach(Object2D @object in EvDevEngine.AllObjects)
            {
                Animation2D animation = @object.GetComponent<Animation2D>();
                if (animation != null)
                {
                    if (animation.condition() && !animation.Animating)
                    {
                        animation.BeginAnimation(Updates);
                    }
                    if (animation.Animating)
                    {
                        animation.StepAnimation(sprites, Updates);
                    }
                    else
                    {

                        this.sprites.Draw(layerDepth: 0f, rotation: 0f, origin: XNAfuncs.Vec2(Vector2.Zero()), texture: @object.Sprite.Sprite, destinationRectangle: @object.Sprite.rectangle, sourceRectangle: null, color: Microsoft.Xna.Framework.Color.White, effects: @object.Sprite.Flipped);
                    }
                }
                else
                {
                    this.sprites.Draw(@object.Sprite.Sprite, @object.Sprite.rectangle, Microsoft.Xna.Framework.Color.White);
                }
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

            Window = new Canvas(Initialize, Load, Unload, UpdateAll, Draw);

            Window.Run();
    

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

        
        public abstract void Initialize();
        public abstract void Load();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public abstract void Unload();


    }
}
