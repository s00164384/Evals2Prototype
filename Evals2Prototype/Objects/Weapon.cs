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

    class Gun  : Weapon
    {
        public List<Projectile> projectiles = new List<Projectile>();
        Texture2D prjTx;
        public Gun(Game g, Texture2D tx, Vector2 pos, string t, Texture2D bounds, Vector2 dimen, int framecount) : base(g, tx, pos, t, bounds, dimen, framecount)
        {
            prjTx = tx;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(Projectile p in projectiles)
            {
                p.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

    }


    class Projectile : AnimatedSprite
    {
        public bool Active;
        float speed;
        int _direction;
        public Projectile(Game g, Texture2D tx, Vector2 pos, string t, Texture2D bounds, Vector2 dimen, int framecount,float sp,int dir) : base(g, tx, pos, t, bounds, dimen, framecount)
        {
            speed = sp;
            _direction = dir;
        }

        public override void Update(GameTime gameTime)
        {
            Position.X += speed * _direction;
            base.Update(gameTime);
        }
    }
}
