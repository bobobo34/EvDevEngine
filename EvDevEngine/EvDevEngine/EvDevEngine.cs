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
            DoubleBuffered = true;
        }

    }
    public abstract class EvDevEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title;
        private Canvas Window = null;
        public Thread GameLoopThread = null;
        public static KeyboardState Input;
        public bool UpdateObjectsBefore = true;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Object2D> AllObjects = new List<Object2D>();
        
        public Color BackgroundColor = Color.White;

        public Vector2 CameraPosition = Vector2.Zero;
        public float CameraAngle = 0f;
        public Vector2 CameraZoom = new Vector2(1, 1);
        
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
            Window.FormClosing += Window_FormClosing;
            
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormClosing(e); 
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
        void GameLoop()
        {
            Load();

            while (GameLoopThread.IsAlive)
            {
                try
                {
                    Draw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    Input = Keyboard.GetState();
                    UpdateAll();
                    Thread.Sleep(2);
                } catch
                {
                    Log.Error("Window has not been found...");
                }
            }
        }

        private void UpdateAll()
        {
            if (UpdateObjectsBefore)
            {
                foreach (Object2D obj in AllObjects)
                {
                    foreach (Component child in obj.Children)
                    {   
                        child.OnUpdate();
                    }
                }
                Update();
            }
            else
            {
                Update();
                foreach (Object2D obj in AllObjects)
                {
                    foreach (Component child in obj.Children)
                    {
                        child.OnUpdate();
                    }
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
                    g.FillRectangle(new SolidBrush(Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
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

        public abstract void FormClosing(FormClosingEventArgs e);


    }
}
