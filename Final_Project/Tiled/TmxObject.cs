using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Final_Project
{
    class TmxObject : GameObject
    {

        int xOff, yOff;
        int width, height;

        public TmxObject(int offsetX, int offsetY, int w, int h) : base("tileset", DrawLayer.Middleground, 1, 1)
        {
            xOff = offsetX;
            yOff = offsetY;

            width = w;
            height = h;

            IsActive = true;
        }

        public override void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture, xOff, yOff, width, height);
            }
        }
    }
}
