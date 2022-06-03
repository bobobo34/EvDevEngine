using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvDevEngine.EvDevEngine;
using EvDevEngine.EvDevEngine._Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine
{
    public class Player : Object2D
    {
        public Player(string ID, Sprite2D sprite, Game game) : base(ID, sprite)
        {
            var movement = new WASDMovement();
            var collider = new Collider2D("ground");
            Texture2D spritesheet = game.Content.Load<Texture2D>("playerrun");
            var animation = new Animation2D(spritesheet, 14, 22, 12, IsMoving, true);
            AddComponent(movement);
            AddComponent(collider);
            AddComponent(animation);
        }

        bool IsMoving()
        {
            return this.GetComponent<WASDMovement>().Moving;
        }

    }
}
