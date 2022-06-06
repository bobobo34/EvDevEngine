using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace EvDevEngine.EvDevEngine
{
    public abstract class State
    {
        public readonly List<Object2D> AllObjects = new List<Object2D>();
        public string ID;
        
        public State(string ID)
        {
            this.ID = ID;
        }
        public void AddObject(Object2D @object)
        {
            AllObjects.Add(@object);
        }
        public void RemoveObject(int index)
        {
            AllObjects.RemoveAt(index);
        }
        public void RemoveObject(Object2D @object)
        {
            AllObjects.Remove(@object);
        }
        public virtual void Draw(GameTime gameTime)
        {
            foreach(Object2D @object in AllObjects)
            {
                Animation2D animation = @object.GetComponent<Animation2D>();
                if (animation != null)
                {
                    if (animation.condition() && !animation.Animating)
                    {
                        animation.BeginAnimation(EvDevEngine.Window.Updates);
                    }
                    if (animation.Animating)
                    {
                        animation.StepAnimation(EvDevEngine.Window.sprites, EvDevEngine.Window.Updates);
                    }
                    else
                    {
                        EvDevEngine.Window.sprites.Draw(layerDepth: 0f, rotation: 0f, origin: XNAfuncs.Vec2(Vector2.Zero()), texture: @object.Sprite.Sprite, destinationRectangle: @object.Sprite.rectangle, sourceRectangle: null, color: Microsoft.Xna.Framework.Color.White, effects: @object.Sprite.Flipped);
                    }
                }
                else
                {
                    EvDevEngine.Window.sprites.Draw(@object.Sprite.Sprite, @object.Sprite.rectangle, Microsoft.Xna.Framework.Color.White);
                }
            }
        }
    }
}
