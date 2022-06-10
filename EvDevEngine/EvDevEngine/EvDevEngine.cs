using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using OpenTK.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EvDevEngine.EvDevEngine
{
    public abstract class EvDevEngine : Game
    {
        private string Title;
        public Thread GameLoopThread = null;
        public static KeyboardState KeyboardInput;
        public static MouseState MouseInput;
        public bool UpdateObjectsBefore = true;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Object2D> AllObjects = new List<Object2D>();
        public static List<State> States = new List<State>();
        
        public Microsoft.Xna.Framework.Color BackgroundColor = Microsoft.Xna.Framework.Color.White;

        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;
        public Vector2 CameraZoom = new Vector2(1, 1);

        public GraphicsDeviceManager graphics = null;
        public SpriteBatch sprites;
        public static int Updates = 0;

        public State CurrentState;
        public bool DoneLoading = false;

        public EvDevEngine(string Title) : base()
        {
            Log.Info("Game is starting...");
            this.Title = Title;
            this.graphics = new GraphicsDeviceManager(this);

            if (GraphicsDevice == null) graphics.ApplyChanges();
            
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            graphics.ApplyChanges();

    

            this.Content.RootDirectory = "Content";
            this.IsFixedTimeStep = true;
            this.IsMouseVisible = true;
            Window.Title = this.Title;
            this.CurrentState = new LoadingState(this);
            AddState(CurrentState);
            Run();
        }
        public void SetState(int index)
        {
            Log.Info($"changing state to {States[index]}");
            this.CurrentState = States[index];
            CurrentState.Load();
        }
        
        protected override void Initialize()
        {
            OnInit();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            this.sprites = new SpriteBatch(this.GraphicsDevice);
            
            Load();
            base.LoadContent();
        }
        protected override void UnloadContent()
        {
            Unload();
            base.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Updates++;
            if (Updates == int.MaxValue) Updates = 7;
            UpdateAll(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            RenderTarget2D target = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            GraphicsDevice.SetRenderTarget(target);
            this.GraphicsDevice.Clear(BackgroundColor);
            this.sprites.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, null);
            OnDraw(gameTime);
            this.sprites.End();
            base.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);
            this.sprites.Begin();
            this.sprites.Draw(target, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
            this.sprites.End();
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
            KeyboardInput = Keyboard.GetState();
            MouseInput = Mouse.GetState();
            OnUpdate(gameTime);
        }

        public void AddState(State state)
        {
            States.Add(state);
        }
        
        public void RemoveState(State state)
        {
            States.Remove(state);
        }
        
        public abstract void OnInit();
        public virtual void Load()
        {
            Task.Run(() => { AddStates(); });

            CurrentState.Load();
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
            if(CurrentState == States[0])
            {
                Log.Info($"{CurrentState.GetType()}, {States[0].GetType()}");
                if (DoneLoading)
                {
                    SetState(1);
                }
            }
            CurrentState.Update(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime)
        {
            CurrentState.Draw(gameTime);
        }

        public virtual void AddStates()
        {
            DoneLoading = true;
        }

        public abstract void Unload();


    }
}
