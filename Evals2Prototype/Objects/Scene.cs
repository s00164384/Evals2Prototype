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
    class Scene : DrawableGameComponent
    {

        public Game game;
        public List<AnimatedSprite> Components = new List<AnimatedSprite>();
        public bool active;

       public Scene(Game g) : base(g)
        {
            g.Components.Add(this);
            game = g;
        }

       
        public override void Update(GameTime gameTime)
        {
            if (!active) return;
            foreach (AnimatedSprite a in Components)
            {
                a.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!active) return;
            foreach(AnimatedSprite a in Components)
            {
                a.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

    }
}
