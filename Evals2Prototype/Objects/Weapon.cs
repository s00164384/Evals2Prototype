using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Evals2Prototype.Objects
{
    class Weapon : AnimatedSprite
    {
        public bool Active;
        public Weapon(Game g, Texture2D tx, Vector2 pos, string t, Texture2D bounds, Vector2 dimen, int framecount):base(g,tx,pos,t,bounds,dimen,framecount)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if(!Active)
            {
                Visible = false;
                currentFrame = 0;
                return;
            }
            Visible = true;
            if(currentFrame == Frames - 1)
            {
                Active = false;
                currentFrame = 0;
            }
            base.Update(gameTime);
        }
    }
}
