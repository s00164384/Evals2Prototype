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

        public SceneManager(Game g,List<Scene> scenes) :base(g)
        {
            activeScene = scenes.ElementAt(0);
            activeScene.active = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (activeScene.active)
                activeScene.Update(gameTime);
            else
                activeScene.active = true;
            base.Update(gameTime);
        }



    }
}
