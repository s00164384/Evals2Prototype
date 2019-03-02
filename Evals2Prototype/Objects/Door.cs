using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evals2Prototype.Objects
{
    class Door : AnimatedSprite
    {

        public Door(Game g, Texture2D tx, Vector2 pos, Texture2D boundsTx, Vector2 dimen,int frames) : base(g, tx, pos, "door", boundsTx, dimen,frames)
        {

        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

    }
}
