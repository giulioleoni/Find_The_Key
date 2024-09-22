using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aiv.Fast2D;
using OpenTK;

namespace Final_Project
{
    class TmxTileLayer
    {
        private Texture tilesetImage;
        public Sprite layerSprite;
        private Texture layerTexture;
        private TmxTileset tileset;
        private int rows, cols;
        private int tileW, tileH;
        public string[] IDs { get; }
        public DrawLayer Layer { get; protected set; }

        public TmxTileLayer(XmlNode layerNode, TmxTileset tileset, int cols, int rows, int tileW, int tileH)
        {
            XmlNode dataNode = layerNode.SelectSingleNode("data");
            string csvData = dataNode.InnerText;
            csvData = csvData.Replace("\r\n", "").Replace("\n", "").Replace(" ", "");

            string[] Ids = csvData.Split(',');
            IDs = Ids;

            this.cols = cols;
            this.rows = rows;
            this.tileW = tileW;
            this.tileH = tileH;
            this.tileset = tileset;

            tilesetImage = GfxMngr.GetTexture(tileset.TextureName);

            layerTexture = new Texture(Game.Window.Width, Game.Window.Height);
            byte[] mapBitmap = new byte[Game.Window.Width * Game.Window.Height * 4];
            Texture tilesetTexture = GfxMngr.GetTexture("tileset");
            byte[] tilesetBitmap = tilesetTexture.Bitmap;

            int bytesPerPixel = 4;
            int tilesetBitmapRowLength = tilesetTexture.Width * bytesPerPixel;
            int mapBitmapRowLength = Game.Window.Width * bytesPerPixel;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int tileId = int.Parse(IDs[r * cols + c]);

                    int tilesetXOff = tileset.GetAtIndex(tileId).X * bytesPerPixel;
               
                    int tilesetYOff = tileset.GetAtIndex(tileId).Y * tilesetBitmapRowLength;
            
                    int tilesetBitmapIndexInitial = tilesetYOff + tilesetXOff;

                    int mapXOff = c * tileW * bytesPerPixel;
                    int mapYOff = r * tileH * mapBitmapRowLength;

                    int mapBitmapIndexInitial = mapXOff + mapYOff;

                    for (int i = 0; i < tileH; i++)
                    {
                        int tilesetBitmapIndexUpdate = i * tilesetBitmapRowLength;

                        int mapBitmapIndexUpdate = i * mapBitmapRowLength;

                        Array.Copy(tilesetBitmap,                                           
                                   tilesetBitmapIndexInitial + tilesetBitmapIndexUpdate,    
                                   mapBitmap,                                               
                                   mapXOff + mapYOff + mapBitmapIndexUpdate,                
                                   tileW * bytesPerPixel);                                  
                    }
                }
            }

            layerTexture.Update(mapBitmap);

            layerSprite = new Sprite(Game.Window.OrthoWidth, Game.Window.OrthoHeight);
        }

        public void Draw()
        {
            layerSprite.DrawTexture(layerTexture);
        }
    }
}
