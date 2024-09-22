using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace Final_Project
{
    class Node
    {
        public int X { get; }
        public int Y { get; }
        public int Cost { get; private set; }

        public List<Node> Neighbours { get; }


        protected Sprite sprite;
        protected Texture texture;
        protected Vector2 position;


        public Node(int x, int y, int cost)
        {
            X = x;
            Y = y;
            position = new Vector2(X, Y);
            Cost = cost;
            Neighbours = new List<Node>();
            sprite = new Sprite(1, 1);
            sprite.position = position;
            if (cost == int.MaxValue)
            { 
                texture = GfxMngr.GetTexture("red");
            }
            else
            {
                texture = GfxMngr.GetTexture("green");
            }

        }

        public void AddNeighbour(Node node)
        {
            Neighbours.Add(node);
        }

        public void RemoveNeighbour(Node node)
        {
            Neighbours.Remove(node);
        }

        public void SetCost(int cost)
        {
            Cost = cost;
        }

        public void Draw()
        {
            sprite.DrawTexture(texture);
        }
        
    }
}
