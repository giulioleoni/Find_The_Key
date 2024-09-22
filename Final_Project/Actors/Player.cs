using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Final_Project
{
    class Player : Actor
    {
        public Vector2 targetPos;

        protected Animation walk;

        public bool HaveTheKey { get; set; }
        public bool HasAnswered { get; set; }


        public Player() : base("Walk_D", 1, 1)
        {
            maxSpeed = 4;
            IsActive = true;
            isClicked = false;

            HaveTheKey = false;
            HasAnswered = false;
            
            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);

            targetPos = Vector2.Zero;

            Agent = new Agent(this);

            walk = new Animation(this, 4, 16, 16, 6);
            walk.IsEnabled = true;
        }

        public void Input()
        {
            if (IsActive)
            {
                if (Game.Window.MouseLeft)
                {
                    if (!isClicked)
                    {
                        isClicked = true;
                        Vector2 fixedMousePosition = CameraMngr.MainCamera.position - CameraMngr.MainCamera.pivot + Game.Window.MousePosition;
                        targetPos = fixedMousePosition;
                    } 
                }
                else if (isClicked)
                {
                    isClicked = false;
                }
            }
        }

        public void HeadToPoint()
        {
            if (Agent.Target == null && targetPos != Vector2.Zero)
            {
                List<Node> path = actualPlayScene.PathfindingMap.GetPath((int)Position.X, (int)Position.Y, (int)targetPos.X, (int)targetPos.Y);
                Agent.SetPath(path);
            }

            Agent.Update(maxSpeed);
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (targetPos != Vector2.Zero)
                {
                    HeadToPoint();
                }

                if (RigidBody.Velocity != Vector2.Zero)
                {
                    walk.Play();
                    CheckWalkAnimation();
                    walk.Update();
                }
                else
                {
                    walk.Stop();
                }

            }
        }

       

        public void CheckWalkAnimation()
        {
            Vector2 dir = RigidBody.Velocity.Normalized();


            if (dir.X > 0.02 && dir.X > dir.Y)
            {
                texture = GfxMngr.GetTexture("Walk_R");
                sprite.FlipX = false;
            }
            else if (dir.X < -0.02 && dir.X < dir.Y)
            {
                texture = GfxMngr.GetTexture("Walk_R");
                sprite.FlipX = true; 
            }
            else if (dir.Y > 0)
            {
                texture = GfxMngr.GetTexture("Walk_D");
                sprite.FlipX = false;
            }
            else if (dir.Y < 0)
            {
                texture = GfxMngr.GetTexture("Walk_U");
                sprite.FlipX = false;
            }
        }


        public override void Draw()
        {
            if (IsActive)
            {
                //Agent.DrawPath();
                
                sprite.DrawTexture(texture, (int)walk.Offset.X, 0, walk.FrameWidth, walk.FrameHeight); 
            }
        }

    }
}
