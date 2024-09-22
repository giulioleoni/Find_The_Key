using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Key : Item
    {
        public Key() : base("key")
        {
        }

        public override void OnCollide(Collision collisionInfo)
        {
            ((Player)collisionInfo.Collider).HaveTheKey = true;
            base.OnCollide(collisionInfo);
        }
    }
}
