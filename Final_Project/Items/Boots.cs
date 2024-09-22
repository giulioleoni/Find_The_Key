using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Boots : Item
    {
        public Boots() : base("boots")
        {
        }

        public override void OnCollide(Collision collisionInfo)
        {
            ((Player)collisionInfo.Collider).maxSpeed *= 2;
            base.OnCollide(collisionInfo);
        }
    }
}
