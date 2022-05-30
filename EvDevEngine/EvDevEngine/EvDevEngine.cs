using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using OpenTK.Input;

namespace EvDevEngine.EvDevEngine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }

    }
    public abstract class EvDevEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title;
        private Canvas Window = null;
        public Thread GameLoopThread = null;
        public KeyboardState Input;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        
        public System.Drawing.Color BackgroundColor = System.Drawing.Color.White;

        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;
        public Vector2 CameraZoom = new Vector2(1, 1);

        //AABB worldAABB = new AABB
        //{
        //    UpperBound = new Vec2(2000, 2000),
        //    LowerBound = new Vec2(-2000, -2000)
        //};
        //Vec2 gravity = new Vec2(0.0f, 10.0f);
        
        public EvDevEngine(Vector2 ScreenSize, string Title)
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas
            {
                Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y),
                Text = this.Title,
                FormBorderStyle = FormBorderStyle.FixedToolWindow
            };      
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.FormClosing += Window_FormClosing;
            
            
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            //world = new World(worldAABB, gravity, false);

            Application.Run(Window);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormClosing(e); 
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
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
        //float timeStep = 1.0f / 60.0f;
        //int velocityIterations = 15;
        //int positionIterations = 3;
        void GameLoop()
        {
            Load();

            while (GameLoopThread.IsAlive)
            {
                try
                {
                    Draw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    //rld.Step(timeStep, velocityIterations, positionIterations);
                    Input = Keyboard.GetState();
                    Update();
                    Thread.Sleep(2);
                } catch
                {
                    Log.Error("Window has not been found...");
                }
            }
        }

        
        private void Renderer(object sender, PaintEventArgs e)
        {

            

            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);
            g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
            g.RotateTransform(CameraAngle);
            g.ScaleTransform(CameraZoom.X, CameraZoom.Y);
            try
            {
                foreach(Shape2D shape in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(System.Drawing.Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
                }
                foreach(Sprite2D sprite in AllSprites)
                {
                    g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
                }
            } catch { Log.Warning("Images are still being processed..."); }

        }
        
        public abstract void Load();

        public abstract void Update();

        public abstract void Draw();

        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
        public abstract void FormClosing(FormClosingEventArgs e);


    }
}
