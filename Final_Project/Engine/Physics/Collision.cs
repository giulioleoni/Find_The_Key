using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Final_Project
{
    enum CollisionType { None, RectsIntersection, CirclesIntersection, CircleRectIntersection }

    struct Collision
    {
        public GameObject Collider;
        public Vector2 Delta;
        public CollisionType Type;
    }
}
