using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Final_Project
{
    class Agent
    {
        public Node Target { get { return target; } set { target = value; } }

        Node current;
        Node target;
        List<Node> path;

        public List<Node> Path { get { return path; } }

        Actor owner;


        public Agent(Actor owner)
        {
            this.owner = owner;
            target = null;
            
        }

        public virtual void SetPath(List<Node> newPath)
        {
            path = newPath;

            if(target == null && path.Count > 0)
            {
                target = path[0];
                path.RemoveAt(0);

               
            }
            else if(path.Count > 0)
            {
                int dist = Math.Abs(path[0].X - target.X) + Math.Abs(path[0].Y - target.Y);

                if(dist > 1)
                {
                    path.Insert(0, current);
                }
            }
        }

        public void ResetPath()
        {
            if(path != null)
            {
                path.Clear();
            }

            target = null;
        }

        public Node GetLastNode()
        {
            if(path.Count > 0)
            {
                return path.Last();
            }

            return null;
        }

        public virtual void Update(float speed)
        {
            if(target != null)
            {
                Vector2 destination = new Vector2(target.X, target.Y);
                Vector2 direction = (destination - owner.Position);
                float distance = direction.Length;

                if (distance < 0.01f)
                {
                    current = target;
                    owner.Position = destination;

                    if (path.Count == 0)
                    {
                        target = null;
                        owner.RigidBody.Velocity = Vector2.Zero;
                    }
                    else
                    {
                        target = path[0];
                        path.RemoveAt(0);
                    }
                }
                else
                {
                    owner.RigidBody.Velocity = direction.Normalized() * speed;
                }
            }
            else
            {
                owner.RigidBody.Velocity = Vector2.Zero;
            }
        }


    }
}
