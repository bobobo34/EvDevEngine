using EvDevEngine.EvDevEngine;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDevEngine
{
    public class KillerWhaleManager : Object2D
    {
        public KillerWhaleManager(string ID, EvDevEngine.EvDevEngine game) : base(ID)
        {
            var spawner = new WhaleSpawner(game);
            AddComponent(spawner);
        }
        
    }
    internal class WhaleSpawner : Component
    {
        EvDevEngine.EvDevEngine game;
        float difficulty = 0;
        public WhaleSpawner(EvDevEngine.EvDevEngine game) 
        {
            this.game = game;
        }
    }
}