using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    abstract class Actor : GameObject
    {
        public float maxSpeed;
        protected bool isClicked;
        protected PlayScene actualPlayScene;

        public Agent Agent { get; set; }

        public Actor(string texturePath, float w = 0, float h = 0) : base(texturePath, w:w ,h:h)
        {
            actualPlayScene = ((PlayScene)Game.CurrentScene);
        }

    }
}

