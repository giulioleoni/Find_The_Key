using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    enum ItemType { Boots, Key, LAST }

    class Item : GameObject
    {
        public bool IsPicked { get; private set; }
        private SoundEmitter pickUpSound;

        public Item(string fileName) : base(fileName, DrawLayer.Middleground)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Item;
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
            RigidBody.AddCollisionType(RigidBodyType.Player);
            pickUpSound = new SoundEmitter(this, "PickUp");
        }

        public override void OnCollide(Collision collisionInfo)
        {
            pickUpSound.Play();
            IsActive = false;
            IsPicked = true;
        }

    }
}
