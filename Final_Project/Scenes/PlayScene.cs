using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using System.Xml;

namespace Final_Project
{
    enum Maps { House_1, House_2, Cave, Final_House, World, LAST }

    class PlayScene : Scene
    {
        public Player Player;
        public NPC Sage;
        
        public TmxMap Map;
        public Map PathfindingMap;

        protected Maps ActualMap;
        protected Dictionary<Maps, Tuple<string, CameraLimits>> mapInfo;

        protected List<Vector2> triggers;
        protected List<Vector2> nextPos;

        protected Key key;
        protected Boots boots;

        protected SoundEmitter soundtrack;


        public PlayScene() : base()
        {

        }

        public override void Start()
        {
            LoadAssets();

            FontMngr.Init();
            Font stdFont = FontMngr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            Font comic = FontMngr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);

            ActualMap = Maps.House_1;

            LoadMapInfo();

            LoadMap("Assets/Maps/" + mapInfo[ActualMap].Item1 + ".tmx");
            Map.IsActive = true;
            LoadPathfindingMap(@".\Assets\Maps\" + mapInfo[ActualMap].Item1 + ".tmx");

            boots = new Boots();
            key = new Key();
            Sage = new NPC();

            Player = new Player();
            Player.Position = new Vector2(35, 5);

            soundtrack = new SoundEmitter(Player, "Soundtrack");
            soundtrack.Play(true);

            CameraLimits cameraLimits = mapInfo[ActualMap].Item2;

            CameraMngr.Init(Player, cameraLimits);
            CameraMngr.SetTarget(Player);


            base.Start();
        }

        private void LoadMapInfo()
        {
            mapInfo = new Dictionary<Maps, Tuple<string, CameraLimits>>();
            mapInfo[Maps.House_1] = new Tuple<string, CameraLimits>("House_1", new CameraLimits(Game.Window.OrthoWidth * 0.75f, Game.Window.OrthoWidth * 0.7f, Game.Window.OrthoHeight * 0.05f, Game.Window.OrthoHeight * 0.1f));
            mapInfo[Maps.House_2] = new Tuple<string, CameraLimits>("House_2", new CameraLimits(Game.Window.OrthoWidth * 0.25f, Game.Window.OrthoWidth * 0.2f, Game.Window.OrthoHeight * 0.2f, Game.Window.OrthoHeight * 0.15f));
            mapInfo[Maps.Cave] = new Tuple<string, CameraLimits>("Cave", new CameraLimits(Game.Window.OrthoWidth * 0.5f, Game.Window.OrthoWidth * 0.4f, Game.Window.OrthoHeight * 0.5f, Game.Window.OrthoHeight * 0.4f));
            mapInfo[Maps.Final_House] = new Tuple<string, CameraLimits>("House_1", new CameraLimits(Game.Window.OrthoWidth * 0.75f, Game.Window.OrthoWidth * 0.7f, Game.Window.OrthoHeight * 0.85f, Game.Window.OrthoHeight * 0.8f));
            mapInfo[Maps.World] = new Tuple<string, CameraLimits>("Final_Project_Map", new CameraLimits(Game.Window.OrthoWidth * 0.5f, Game.Window.OrthoWidth * 0.5f, Game.Window.OrthoHeight * 0.5f, Game.Window.OrthoHeight * 0.5f));

            triggers = new List<Vector2>();
            nextPos = new List<Vector2>();

            triggers.Add(new Vector2(23, 13));
            triggers.Add(new Vector2(15, 18));
            triggers.Add(new Vector2(8, 15));
            triggers.Add(new Vector2(15, 5));
            triggers.Add(new Vector2(5, 15));
            triggers.Add(new Vector2(33, 9));
            triggers.Add(new Vector2(37, 3));


            nextPos.Add(new Vector2(15, 6));
            nextPos.Add(new Vector2(5, 16));
            nextPos.Add(new Vector2(33, 8));
            nextPos.Add(new Vector2(23, 12));
            nextPos.Add(new Vector2(15, 17));
            nextPos.Add(new Vector2(8, 16));
            nextPos.Add(new Vector2(30, 40));
        }


        private static void LoadAssets()
        {
            GfxMngr.AddTexture("tileset", "Assets/TILESET/PixelPackTOPDOWN8BIT.png");
           
            GfxMngr.AddTexture("Walk_D", "Assets/SPRITES/HEROS/spritesheets/HEROS8bit_Adventurer Walk D.png");
            GfxMngr.AddTexture("Walk_R", "Assets/SPRITES/HEROS/spritesheets/HEROS8bit_Adventurer Walk R.png");
            GfxMngr.AddTexture("Walk_U", "Assets/SPRITES/HEROS/spritesheets/HEROS8bit_Adventurer Walk U.png");

            GfxMngr.AddTexture("NPC", "Assets/SPRITES/ENEMIES8bit_Sorcerer Idle D.png");

            GfxMngr.AddTexture("boots", "Assets/SPRITES/ITEMS/item8BIT_boots.png");
            GfxMngr.AddTexture("key", "Assets/SPRITES/ITEMS/item8BIT_key.png");

            GfxMngr.AddTexture("green", "Assets/green_rectangle.png");
            GfxMngr.AddTexture("red", "Assets/red_rectangle.png");

            GfxMngr.AddClip("Soundtrack", "Assets/MUSIC/1BITTopDownMusics - Track 03 (1BIT Eerie).wav");
            GfxMngr.AddClip("PickUp", "Assets/SFX/Pickup01.wav");
        }

        public void LoadMap(string filePath)
        {
            Map = new TmxMap(filePath);
        }

        public void LoadPathfindingMap(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(filePath);
            }
            catch (XmlException e)
            {
                Console.WriteLine("XML Exception: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }

            XmlNode mapNode = xmlDoc.SelectSingleNode("map");
            XmlNodeList layersNodes = mapNode.SelectNodes("layer");
            XmlNode dataNode = null;

            for (int i = 0; i < layersNodes.Count; i++)
            {
                string layerName = GetStringAttribute(layersNodes[i], "name");

                if (layerName == "Tile_NonWalkable_Layer")
                {
                    dataNode = layersNodes[i].SelectSingleNode("data");
                }
            }

            string csvData = dataNode.InnerText;
            csvData = csvData.Replace("\r\n", "").Replace("\n", "").Replace(" ", "");

            string[] Ids = csvData.Split(',');
            int[] cells = new int[Ids.Length];

            for (int i = 0; i < Ids.Length; i++)
            {
                if (Ids[i] == "0" || Ids[i] == "164" || Ids[i] == "182")
                {
                    cells[i] = 1;
                }
                else
                {
                    cells[i] = int.MaxValue;
                }
            }

            PathfindingMap = new Map(45, 45, cells);
        }

        private void LoadNextMap(CameraLimits limits, string fileName, Vector2 pos, Maps nextMap)
        {
            if (nextMap != Maps.Final_House || Player.HaveTheKey)
            {
                Map.Clear();
                LoadMap("Assets/Maps/" + fileName + ".tmx");
                Map.IsActive = true;
                LoadPathfindingMap(@".\Assets\Maps\" + fileName + ".tmx");
                ActualMap = nextMap;
                CameraMngr.CameraLimits = limits;
                Player.Position = pos;
                Player.Agent.ResetPath();
                Player.Agent.Update(0);
                Player.targetPos = Vector2.Zero;

                CheckItemSpawn();
                CheckMapEvents();
                
            }
            
        }

        private void CheckMapEvents()
        {
            if (ActualMap == Maps.World)
            {
                Sage.IsActive = true;
                Sage.Position = new Vector2(35, 35);
                PathfindingMap.RemoveNode(35, 35);
                return;
            }
            else if (Sage.IsActive == true)
            {
                Sage.IsActive = false;
            }

            PathfindingMap.AddNode(35, 35);


            if (ActualMap == Maps.Final_House)
            {
                TextObject text = new TextObject(new Vector2(20, 25), "Game end, press ESC to exit");
                text.IsActive = true;
            }
        }

        private void CheckItemSpawn()
        {
            if (ActualMap == Maps.House_2 && !boots.IsPicked)
            {
                boots.IsActive = true;
                boots.Position = new Vector2(13, 7);
                return;
            }
            else if (ActualMap != Maps.House_2 && boots.IsActive)
            {
                boots.IsActive = false;
            }

            if (ActualMap == Maps.Cave && !key.IsPicked)
            {
                key.IsActive = true;
                key.Position = new Vector2(15, 22);
                return;
            }
            else if (ActualMap != Maps.Cave && key.IsActive)
            {
                key.IsActive = false;
            }
        }

        public int GetIntAttribute(XmlNode node, string attrName)
        {
            return int.Parse(GetStringAttribute(node, attrName));
        }

        public bool GetBoolAttribute(XmlNode node, string attrName)
        {
            return bool.Parse(GetStringAttribute(node, attrName));
        }

        public string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }

        public override void Input()
        {
            Player.Input();

            if (Sage.IsActive)
            {
                Sage.CheckPlayerInput();
            }
            
        }

        public override void Update()
        {
            PhysicsMngr.Update();
            UpdateMngr.Update();
            CameraMngr.Update();

            PhysicsMngr.CheckCollisions();

            CheckMap();
        }

        private void CheckMap()
        {
            switch (ActualMap) 
            {
                case Maps.House_1:

                    if (Player.Position == triggers[(int)ActualMap])
                    {
                        LoadNextMap(mapInfo[Maps.World].Item2, mapInfo[Maps.World].Item1, nextPos[(int)ActualMap], Maps.World);
                    }

                    break;

                case Maps.House_2:

                    if (Player.Position == triggers[(int)ActualMap])
                    {
                        LoadNextMap(mapInfo[Maps.World].Item2, mapInfo[Maps.World].Item1, nextPos[(int)ActualMap], Maps.World);
                    }

                    break;

                case Maps.Cave:

                    if (Player.Position == triggers[(int)ActualMap])
                    {
                        LoadNextMap(mapInfo[Maps.World].Item2, mapInfo[Maps.World].Item1, nextPos[(int)ActualMap], Maps.World);
                    }

                    break;

                case Maps.World:

                    for (int i = 3; i < triggers.Count; i++)
                    {
                        if (Player.Position == triggers[i])
                        {
                            LoadNextMap(mapInfo[(Maps)i - 3].Item2, mapInfo[(Maps)i - 3].Item1, nextPos[i], (Maps)i - 3);
                        }
                    }

                    break;
            }
        }

        public override Scene OnExit()
        {
            UpdateMngr.ClearAll();
            PhysicsMngr.ClearAll();
            DrawMngr.ClearAll();
            GfxMngr.ClearAll();
            FontMngr.ClearAll();


            return base.OnExit();
        }

        public override void Draw()
        {
            DrawMngr.Draw();
            Vector2 fixedMousePosition = CameraMngr.MainCamera.position - CameraMngr.MainCamera.pivot + Game.Window.MousePosition;
            Node node = PathfindingMap.GetNode((int)fixedMousePosition.X, (int)fixedMousePosition.Y);
            if (node != null)
            {
                node.Draw(); 
            }
        }

    }
}
