using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Final_Project
{
    static class Game
    {
        public static Window Window;
        public static Scene CurrentScene { get; private set; }
        public static float DeltaTime { get { return Window.DeltaTime; } }

        public static float UnitSize { get; private set; }
        public static float OptimalScreenHeight { get; private set; }
        public static float OptimalUnitSize { get; private set; }
        public static Vector2 ScreenCenter { get; private set; }

        public static void Init()
        {
            Window = new Window(720, 720, "Find the key");
            Window.SetVSync(false);
            Window.SetDefaultViewportOrthographicSize(45);
            Window.Position = new Vector2(50, 20);

            OptimalScreenHeight = 1080; 
            UnitSize = Window.Height / Window.OrthoHeight;
            OptimalUnitSize = OptimalScreenHeight / Window.OrthoHeight;

            ScreenCenter = new Vector2(Window.OrthoWidth * 0.5f, Window.OrthoHeight * 0.5f);
            Console.WriteLine(ScreenCenter);

            PlayScene playScene = new PlayScene();
            playScene.NextScene = null;

            CurrentScene = playScene;
        }

        public static float PixelsToUnits(float pixelsSize)
        {
            return pixelsSize / OptimalUnitSize;
        }


        public static void Play()
        {
            CurrentScene.Start();

            while (Window.IsOpened)
            {

                // Exit when ESC is pressed
                if (Window.GetKey(KeyCode.Esc))
                {
                    break;
                }

                if (!CurrentScene.IsPlaying)
                {
                    Scene nextScene = CurrentScene.OnExit();

                    if(nextScene != null)
                    {
                        CurrentScene = nextScene;
                        CurrentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }

                
                CurrentScene.Input();

               
                CurrentScene.Update();

                
                CurrentScene.Draw();

                Window.Update();
            }
        }
    }
}
