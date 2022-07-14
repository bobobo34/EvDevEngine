using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EvDevEngine.EvDevEngine.Engine;
using MonoGame.Extended;

namespace EvDevEngine.EvDevEngine
{
    public class Object2D
    {
        public string ID;
            #nullable enable
        public Sprite2D? Sprite = null;
            #nullable disable
        public List<Component> Children = new List<Component>();
        public EvDevEngine Game;
        public Object2D(string ID, Sprite2D sprite)
        {
            this.ID = ID;
            this.Sprite = sprite;
            RegisterObject();
        }
        public Object2D(string ID)
        {
            this.ID = ID;
            RegisterObject();
        }
        public void AddComponent(Component Component)
        {
            Component.Parent = this;
            Children.Add(Component);
            Component.OnLoad();
        }

        public void RemoveComponent<T>()
        {
            Component component = Children.Find(x => x.GetType() == typeof(T));
            component.Destroy();
            Children = Children.Where(x => x != component).ToList();
        }

        public T GetComponent<T>()
        {
            return (T)Convert.ChangeType(Children.Find(x => x.GetType() == typeof(T)), typeof(T));
        }
        public T GetComponent<T>(int index)
        {
            return (T)Convert.ChangeType(Children.Where(x => x.GetType() == typeof(T)).ToList()[index], typeof(T));
        }
        public void RegisterObject()
        {
            AllObjects.Add(this);
        }
        public void UnRegisterObject()
        {
            foreach(var obj in Children.ToList())
            {
                obj.Destroy();
            }
            CurrentState.RemoveObject(this);
            AllObjects.Remove(this);
            EvDevEngine.UnregisterSprite(Sprite);
        }


    }
}
