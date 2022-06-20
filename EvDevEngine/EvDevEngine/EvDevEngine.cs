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
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace EvDevEngine.EvDevEngine
{

    public abstract class EvDevEngine : Game
    {
        private string Title;
        public Thread GameLoopThread = null;
        public static KeyboardState KeyboardInput;
        public static MouseState MouseInput;
        public bool UpdateObjectsBefore = true;

        public int Height
        {
            get
            {
                return graphics.PreferredBackBufferHeight;
            }
        }
        public int Width
        {
            get
            {
                return graphics.PreferredBackBufferWidth;
            }
        }

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Object2D> AllObjects = new List<Object2D>();
        public static List<State> States = new List<State>();
        
        public Microsoft.Xna.Framework.Color BackgroundColor = Microsoft.Xna.Framework.Color.White;

        public GraphicsDeviceManager graphics = null;
        public SpriteBatch sprites;
        public static int Updates = 0;

        public static OrthographicCamera Camera;
        public static Microsoft.Xna.Framework.Vector2 MousePos
        {
            get
            {
                return Camera.ScreenToWorld(XNAfuncs.Vec2(new Vector2(MouseInput.X, MouseInput.Y)));
            }
        }

        public State CurrentState;
        public bool DoneLoading = false;
        public EvDevEngine(string Title) : base()
        {
            Log.Info("Game is starting...");
            this.Title = Title;
            this.graphics = new GraphicsDeviceManager(this);

            if (GraphicsDevice == null) graphics.ApplyChanges();
            
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 540;
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
            foreach (var obj in AllObjects.ToList()) AllObjects.Remove(obj);
            Log.Info($"changing state to {States[index]}");
            this.CurrentState = States[index];
            CurrentState.Load();
        }
        public void SetState<T>()
        {
            foreach (var obj in AllObjects.ToList()) AllObjects.Remove(obj);
            int index = States.FindIndex(s => s.GetType() == typeof(T));
            Log.Info($"changing state to {States[index]}");
            this.CurrentState = States[index];
            CurrentState.Load();
        }
        
        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);
            Camera = new OrthographicCamera(viewportAdapter);
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
            var matrix = Camera.GetViewMatrix();
            this.GraphicsDevice.Clear(BackgroundColor);
            this.sprites.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            OnDraw(gameTime);
            this.sprites.End();
            base.Draw(gameTime);
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
