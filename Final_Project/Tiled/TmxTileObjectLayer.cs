using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OpenTK;

namespace Final_Project
{
    class TmxTileObjectLayer
    {
        public TmxObject[] objects;
        public string[] IDs { get; }

        public TmxTileObjectLayer(XmlNode tileObjectLayerNode, XmlNode tilesetNode, TmxTileset tileset)
        {
            XmlNodeList tilesNodes = tilesetNode.SelectNodes("tile");

            XmlNode dataNode = tileObjectLayerNode.SelectSingleNode("data");
            string csvData = dataNode.InnerText;
            csvData = csvData.Replace("\r\n", "").Replace("\n", "").Replace(" ", "");

            string[] Ids = csvData.Split(',');
            IDs = Ids;

            int cols = TmxMap.GetIntAttribute(tileObjectLayerNode, "width");
            int rows = TmxMap.GetIntAttribute(tileObjectLayerNode, "height");
            int width = TmxMap.GetIntAttribute(tilesetNode, "tilewidth");
            int height = TmxMap.GetIntAttribute(tilesetNode, "tileheight");

            objects = new TmxObject[cols * rows];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int tileId = int.Parse(IDs[r * cols + c]);

                    if(tileId > 0)
                    {
                        int tilesetXOff = tileset.GetAtIndex(tileId).X;
                        int tilesetYOff = tileset.GetAtIndex(tileId).Y;
                        

                        objects[r * cols + c] = new TmxObject(tilesetXOff, tilesetYOff, width, height);
                        objects[r * cols + c].Position = new Vector2(c, r);
                    }
                }
            }
        }
    }
}
