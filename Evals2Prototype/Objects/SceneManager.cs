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

namespace Evals2Prototype.Objects
{
    class SceneManager : GameComponent
    {
        Scene activeScene;
        List<Scene> sceneList = new List<Scene>();
        Camera c;

        public SceneManager(Game g,List<Scene> scenes) :base(g)
        {
            MediaPlayer.IsRepeating = true;
            g.Components.Add(this);
            activeScene = scenes.ElementAt(0);
            activeScene.active = true;
            sceneList = scenes;
            c = new Camera(g, Vector2.Zero, new Vector2(5000, 5000), activeScene);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!activeScene.active)
            {
                activeScene.active = true;

            }



                if (Keyboard.GetState().IsKeyDown(Keys.T) && activeScene._name != "Second Level")
            {
                activeScene.active = false;
                activeScene = sceneList.ElementAt(1);


            }
            if (Keyboard.GetState().IsKeyDown(Keys.R) && activeScene._name != "First Level")
            {
                activeScene.active = false;
                activeScene = sceneList.ElementAt(0);
            }
            c.scene = activeScene;

            base.Update(gameTime);
        }



    }
}
