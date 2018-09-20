using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evals2Prototype.Objects
{
    class Enemy : AnimatedSprite
    {
        int _Right;
        List<Wall> level;
        Rectangle BoundingBoxBot;
        public Enemy(Game g, Texture2D tx, Vector2 pos, Texture2D boundsTx, Vector2 dimen, int right , List<Wall> f,int frames) : base(g, tx, pos, "enemy", boundsTx, dimen,frames)
        {
            _Right = right;
            level = f;
            BoundingBoxBot = new Rectangle((int)Position.X + (int)Dimensions.X / 3, (int)Position.Y + (int)Dimensions.Y + 2, ((int)Dimensions.X / 3), 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Visible) return;
            bool grounded = false;

            BoundingBoxBot = new Rectangle((int)Position.X + (int)Dimensions.X / 3 , (int)Position.Y + (int)Dimensions.Y + 2, ((int)Dimensions.X / 3), 2);
            foreach(Wall w in level)
            {
                if(BoundingBoxBot.Intersects(w.BoundingBox))
                    {
                    grounded = true;
                }
            }

            if(!grounded)
            {
                _Right *= -1;
            }

            if (_Right == 1)
            {
                _dir = "right";
                Position.X += 1f;
            }
            else
            {
                _dir = "left";
                Position.X += -1f;
            }
            BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)Dimensions.X, (int)Dimensions.Y);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                                Camera.CurrentCameraTranslation);
            Sb.Draw(boundtx, BoundingBoxBot, Color.White);
            //Sb.Draw(boundtx, BoundingBox, InCollision);
            Sb.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
