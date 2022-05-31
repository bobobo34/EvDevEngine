﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDevEngine.EvDevEngine
{
    public class Object2D
    {
        public string ID;
        public Sprite2D Sprite = null;
        public List<Component> Children = new List<Component>();
        public Vector2 Position = Vector2.Zero;

        public Object2D(string ID, Sprite2D sprite)
        {
            this.ID = ID;
            this.Sprite = sprite;
            RegisterObject();
        }

        public void AddComponent(Component Component)
        {
            Children.Add(Component);
        }

        public void RemoveComponent<T>()
        {
            Component component = Children.Find(x => x.GetType() == typeof(T));
            component.Destroy();
            Children = Children.Where(x => x != component).ToList();
        }

        public Component GetComponent<T>()
        {
            return Children.Find(x => x.GetType() == typeof(T));
        }
        public void RegisterObject()
        {
            EvDevEngine.AllObjects.Add(this);
        }
        public void UnRegisterObject()
        {
            EvDevEngine.AllObjects.Remove(this);
        }
    }
}