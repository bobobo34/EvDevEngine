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
    public static class Engine
    {
        public static KeyboardState KeyboardInput;
        public static MouseState MouseInput;
        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Object2D> AllObjects = new List<Object2D>();
        public static List<State> States = new List<State>();
        public static GraphicsDeviceManager graphics = null;
        public static SpriteBatch sprites;
        public static OrthographicCamera Camera;
        public static EvDevEngine game;

        public static Vector2 MousePos
        {
            get
            {
                return XNAfuncs.Vec2(Camera.ScreenToWorld(XNAfuncs.Vec2(new Vector2(MouseInput.X, MouseInput.Y))));
            }
        }
        public static int Height
        {
            get
            {
                return graphics.PreferredBackBufferHeight;
            }
        }
        public static int Width
        {
            get
            {
                return graphics.PreferredBackBufferWidth;
            }
        }
        public static Microsoft.Xna.Framework.Color BackgroundColor = Microsoft.Xna.Framework.Color.White;
        public static State CurrentState;
        public static int Updates = 0;
    }
    public abstract class EvDevEngine : Game
    {
        private string Title;
        public bool UpdateObjectsBefore = true;

        

        
        

        

        

        public bool DoneLoading = false;
        public EvDevEngine(string Title) : base()
        {
            Log.Info("Game is starting...");
            this.Title = Title;
            Engine.graphics = new GraphicsDeviceManager(this);

            if (GraphicsDevice == null) Engine.graphics.ApplyChanges();
            
            Engine.graphics.PreferredBackBufferWidth = 960;
            Engine.graphics.PreferredBackBufferHeight = 540;
            Engine.graphics.ApplyChanges();

            

            this.Content.RootDirectory = "Content";
            this.IsFixedTimeStep = true;
            this.IsMouseVisible = true;
            Window.Title = this.Title;
            Engine.CurrentState = new LoadingState(this);
            AddState(Engine.CurrentState);

            Engine.game = this;

            Run();
        }
        public void SetState(int index)
        {
            foreach (var obj in Engine.AllObjects.ToList()) Engine.AllObjects.Remove(obj);
            Log.Info($"changing state to {Engine.States[index]}");
            Engine.CurrentState = Engine.States[index];
            Engine.CurrentState.Load();                       
        }
        public void SetState<T>()
        {
            foreach (var obj in Engine.AllObjects.ToList()) Engine.AllObjects.Remove(obj);
            int index = Engine.States.FindIndex(s => s.GetType() == typeof(T));
            Log.Info($"changing state to {Engine.States[index]}");
            Engine.CurrentState = Engine.States[index];
            Engine.CurrentState.Load();
        }
        
        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);
            Engine.Camera = new OrthographicCamera(viewportAdapter);
            OnInit();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Engine.sprites = new SpriteBatch(this.GraphicsDevice);
            
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
            Engine.Updates++;
            if (Engine.Updates == int.MaxValue) Engine.Updates = 7;
            UpdateAll(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            var matrix = Engine.Camera.GetViewMatrix();
            this.GraphicsDevice.Clear(Engine.BackgroundColor);
            Engine.sprites.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            OnDraw(gameTime);
            Engine.sprites.End();
            base.Draw(gameTime);
        }

        public static void RegisterShape(Shape2D shape)
        {
            Engine.AllShapes.Add(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            Engine.AllSprites.Add(sprite);
        }

        public static void UnregisterShape(Shape2D shape)
        {
            Engine.AllShapes.Remove(shape);
        }

        public static void UnregisterSprite(Sprite2D sprite)
        {
            Engine.AllSprites.Remove(sprite);
        }
        
        private void UpdateAll(GameTime gameTime)
        {
            Engine.KeyboardInput = Keyboard.GetState();
            Engine.MouseInput = Mouse.GetState();
            OnUpdate(gameTime);
        }

        public void AddState(State state)
        {
            Engine.States.Add(state);
        }
        
        public void RemoveState(State state)
        {
           Engine.States.Remove(state);
        }
        
        public abstract void OnInit();
        public virtual void Load()
        {
            Task.Run(() => { AddStates(); });

            Engine.CurrentState.Load();
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
            if(Engine.CurrentState == Engine.States[0])
            {
                Log.Info($"{Engine.CurrentState.GetType()}, {Engine.States[0].GetType()}");
                if (DoneLoading)
                {
                    SetState(1);
                }
            }
            Engine.CurrentState.Update(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime)
        {

            Engine.CurrentState.Draw(gameTime);

        }

        public virtual void AddStates()
        {
            DoneLoading = true;
        }

        public abstract void Unload();


    }
}
