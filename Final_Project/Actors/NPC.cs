using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace Final_Project
{
    class NPC : Actor
    {
        private TextObject[] enigma;
        private TextObject failText;
        private TextObject successText;


        public NPC() : base("NPC", 1, 1)
        {
            RigidBody = new RigidBody(this);

            actualPlayScene = ((PlayScene)Game.CurrentScene);

            enigma = new TextObject[4];
            
            enigma[0] = new TextObject(new Vector2(15, 28), "When was Super Mario first appeared?");
            enigma[1] = new TextObject(new Vector2(25, 30), "A)  1985");
            enigma[2] = new TextObject(new Vector2(25, 32), "S)  1981");
            enigma[3] = new TextObject(new Vector2(25, 34), "D)  1979");
            SetEnigma(false);

            failText = new TextObject(new Vector2(20, 28), "BOO WRONG, go away and retry");
            failText.IsActive = false;
            successText = new TextObject(new Vector2(19, 28), "CORRECT, the key is in the cave");
            successText.IsActive = false;
        }
        

        private void SetEnigma(bool active)
        {
            for (int i = 0; i < enigma.Length; i++)
            {
                enigma[i].IsActive = active;
            }
        }

        public override void Update()
        {
            if ((actualPlayScene.Player.Position - Position).LengthSquared >= 9)
            {
                if (failText.IsActive == true || successText.IsActive == true || enigma[0].IsActive == true)
                {
                    SetEnigma(false);
                    failText.IsActive = false;
                    successText.IsActive = false;
                    actualPlayScene.Player.HasAnswered = false; 
                }
            }
            else if ((actualPlayScene.Player.Position - Position).Length <= 1 && !actualPlayScene.Player.HasAnswered)
            {
                SetEnigma(true);
            }
        }

        public void CheckPlayerInput()
        {
            if (enigma[0].IsActive)
            {
                if (Game.Window.GetKey(KeyCode.A) || Game.Window.GetKey(KeyCode.D))
                {
                    if (!isClicked)
                    {
                        isClicked = true;
                    }
                    else if (isClicked)
                    {
                        isClicked = false;
                        actualPlayScene.Player.HasAnswered = true;
                        SetEnigma(false);
                        failText.IsActive = true;
                    }
                }
                else if (Game.Window.GetKey(KeyCode.S))
                {
                    if (!isClicked)
                    {
                        isClicked = true;
                    }
                    else if (isClicked)
                    {
                        isClicked = false;
                        actualPlayScene.Player.HasAnswered = true;
                        SetEnigma(false);
                        successText.IsActive = true;
                    }
                }
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, 0, 0, 16, 16);
            }
        }
    }
}
