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
    class SceneManager : DrawableGameComponent
    {
        Scene activeScene;

        public SceneManager(Game g,List<Scene> scenes) :base(g)
        {
            activeScene = scenes.ElementAt(0);
        }

        public override void Update(GameTime gameTime)
        {
            activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            activeScene.Draw(gameTime);
            base.Draw(gameTime);
        }



    }
}
