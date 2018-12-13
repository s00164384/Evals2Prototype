using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Evals2Prototype.Scenes;

namespace Evals2Prototype.Objects
{
    class SceneManager : GameComponent
    {
        Scene activeScene;
        List<Scene> sceneList = new List<Scene>();
        Camera c;
        Menu menu;

        public SceneManager(Game g,List<Scene> scenes) :base(g)
        {
    
            MediaPlayer.IsRepeating = true;
            g.Components.Add(this);
            menu = new Menu(g);
            activeScene = menu;
            activeScene.active = true;
            sceneList = scenes;
            c = new Camera(g, Vector2.Zero, new Vector2(5000, 5000), activeScene);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!activeScene.active)
            {
                MediaPlayer.Stop();
                activeScene.active = true;
                MediaPlayer.Play(activeScene.bgm);
            }

            if(activeScene._name == "Menu")
            {
                if(menu.selectionMade)
                {
                    switch (menu.selection)
                    {
                        case 0:
                            activeScene.active = false;
                            activeScene = sceneList.ElementAt(0);
                            break;
                        case 1:
                            activeScene.active = false;
                            activeScene = sceneList.ElementAt(1);
                            break;
                        case 2:
                            Game.Exit();
                            break;
                        default:
                            break;
                    }
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && activeScene._name != "Menu")
            {
                activeScene.active = false;
                activeScene = menu;
                menu.selectionMade = false;


            }
            //if (Keyboard.GetState().IsKeyDown(Keys.R) && activeScene._name != "First Level")
            //{

            //}
            c.scene = activeScene;

            base.Update(gameTime);
        }



    }
}
