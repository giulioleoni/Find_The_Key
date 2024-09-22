using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Final_Project
{
    struct TileOffset
    {
        public int X;
        public int Y;

        public TileOffset(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class TmxTileset
    {
        private TileOffset[] tiles;
        public string TextureName { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        private int cols;
        private int rows;

        public TmxTileset(string textureName, int cols, int rows, int tileW, int tileH)
        {
            tiles = new TileOffset[cols * rows];

            TextureName = textureName;
            TileWidth = tileW;
            TileHeight = tileH;
            this.cols = cols;
            this.rows = rows;


            int xOff = 0;
            int yOff = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    tiles[i * cols + j] = new TileOffset(xOff, yOff);

                    xOff += TileWidth;
                }

                xOff =  0;
                yOff += TileHeight;
            }
        }

        public TileOffset GetAtIndex(int index)
        {
            if (index == 0)
            {
                return tiles[index]; 
            }
            else
            {
                return tiles[index - 1];
            }
        }
    }
}
