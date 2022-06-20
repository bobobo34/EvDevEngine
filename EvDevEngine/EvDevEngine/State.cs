using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EvDevEngine.EvDevEngine.UIElements;
using Microsoft.Xna.Framework.Input;

namespace EvDevEngine.EvDevEngine
{
    public abstract class State
    {
        public readonly List<Object2D> AllObjects = new List<Object2D>();
        public readonly List<IUIElement> uIElements = new List<IUIElement>();
        public string ID;
        protected EvDevEngine game;
        
        public State(string ID, EvDevEngine game)
        {
            this.ID = ID;
            this.game = game;
        }
        public void AddObject(Object2D @object)
        {
            AllObjects.Add(@object);
            @object.game = game;
        }
        public void RemoveObject(int index)
        {
            AllObjects.RemoveAt(index);
        }
        public void RemoveObject(Object2D @object)
        {
            AllObjects.Remove(@object);
        }
        public void AddUI(IUIElement uielement)
        {
            uIElements.Add(uielement);
        }
        public void RemoveUI(IUIElement uielement)
        {
            uIElements.Remove(uielement);
        }
        public abstract void Load();
        public virtual void Draw(GameTime gameTime)
        {
            foreach(IUIElement element in uIElements)
            {
                element.Draw(gameTime);
            }
            foreach(Object2D @object in AllObjects)
            {
                Animation2D animation = @object.GetComponent<Animation2D>();
                if (animation != null)
                {
                    if (animation.condition() && !animation.Animating)
                    {
                        animation.BeginAnimation(EvDevEngine.Updates);
                    }
                    if (animation.Animating)
                    {
                        animation.StepAnimation(EvDevEngine.Updates);
                    }
                    else
                    {
                        @object.Sprite.DrawSelf();
                    }
                }
                else
                {
                    @object.Sprite.DrawSelf();
                }
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach(IUIElement element in uIElements.ToList())
            {
                element.Update(gameTime);
            }
            foreach(Object2D @object in AllObjects.ToList())
            {
                foreach(Component component in @object.Children.ToList())
                {
                    component.OnUpdate(gameTime);
                }
            }
        }
        public virtual void ResizeAll(Vector2 oldScreenSize, Vector2 newScreenSize)
        {
            foreach (Object2D obj in AllObjects)
            {
                obj.Sprite.ChangeSize(oldScreenSize, newScreenSize);
            }
            foreach (IUIElement element in uIElements)
            {
                element.ChangeSize(oldScreenSize, newScreenSize);
            }
        }
    }
}
