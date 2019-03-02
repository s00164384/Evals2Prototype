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
    class Collectable : AnimatedSprite
    {
        public int scoreValue;
        public Collectable(Game g, Texture2D tx, Vector2 pos, Texture2D bounds, Vector2 dimen, int framecount,int score):base(g,tx,pos,"Collectable",bounds,dimen,framecount)
        {
            scoreValue = score;
        }

    }
}
