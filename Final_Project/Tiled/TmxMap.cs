using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aiv.Fast2D;

namespace Final_Project
{
    class TmxMap : IDrawable
    {
        private string tmxFilePath;
        public DrawLayer Layer { get; }
       
        TmxTileset tileset;
        
        TmxTileLayer tileLayer;
        
        TmxTileObjectLayer tileObjectLayer;

        public bool IsActive;

        public TmxMap(string filePath)
        {
            Layer = DrawLayer.Background;
            DrawMngr.AddItem(this);

            IsActive = false;

            tmxFilePath = filePath;

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(tmxFilePath);
            }
            catch(XmlException e)
            {
                Console.WriteLine("XML Exception: " + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }

            
            XmlNode mapNode = xmlDoc.SelectSingleNode("map");
            int mapCols = GetIntAttribute(mapNode, "width");
            int mapRows = GetIntAttribute(mapNode, "height");
            int mapTileW = GetIntAttribute(mapNode, "tilewidth");
            int mapTileH = GetIntAttribute(mapNode, "tileheight");

            
            XmlNode tilesetNode = mapNode.SelectSingleNode("tileset");
            int tilesetTileW = GetIntAttribute(tilesetNode, "tilewidth");
            int tilesetTileH = GetIntAttribute(tilesetNode, "tileheight");
            int tileCount = GetIntAttribute(tilesetNode, "tilecount");
            int tilesetCols = GetIntAttribute(tilesetNode, "columns");
            int tilesetRows = tileCount / tilesetCols;
            
            tileset = new TmxTileset("tileset", tilesetCols, tilesetRows, tilesetTileW, tilesetTileH);
            
            XmlNodeList layersNodes = mapNode.SelectNodes("layer");

            for (int i = 0; i < layersNodes.Count; i++)
            {
                string layerName = GetStringAttribute(layersNodes[i], "name");

                if (layerName == "Tile_Layer")
                {
                    tileLayer = new TmxTileLayer(layersNodes[i], tileset, mapCols, mapRows, mapTileW, mapTileH);
                }
                else if (layerName == "Tile_NonWalkable_Layer")
                {
                    tileObjectLayer = new TmxTileObjectLayer(layersNodes[i], tilesetNode, tileset);
                }
            }

        }

        public static int GetIntAttribute(XmlNode node, string attrName)
        {
            return int.Parse(GetStringAttribute(node, attrName));
        }

        public static bool GetBoolAttribute(XmlNode node, string attrName)
        {
            return bool.Parse(GetStringAttribute(node, attrName));
        }

        public static string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }

        public void Draw()
        {
            if (IsActive)
            {
                tileLayer.Draw();
            }
        }

        public void Clear()
        {
            IsActive = false;

            foreach (TmxObject item in tileObjectLayer.objects)
            {
                if (item != null && item.IsActive)
                {
                    item.IsActive = false;
                }
            }
        }
    }
}
