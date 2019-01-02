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
        public Vector2 DoorLeft;
        public Vector2 DoorRight;
        public Rectangle BoundingBox2;


        public Door(Game g, Texture2D tx, Vector2 pos,Vector2 pos2, Texture2D boundsTx, Vector2 dimen,int frames) : base(g, tx, pos, "door", boundsTx, dimen,frames)
        {
            DoorLeft = pos;
            DoorRight = pos2;
            BoundingBox2 = new Rectangle((int)this.DoorRight.X, (int)this.DoorRight.Y, (int)dimen.X, (int)dimen.Y);
        }

        public override void Update(GameTime gameTime)
        {
            //BoundingBox2 = new Rectangle((int)this.DoorLeft.X, (int)this.DoorLeft.Y, (int)Dimensions.X, (int)Dimensions.Y);
            BoundingBox2 = new Rectangle((int)this.DoorRight.X, (int)this.DoorRight.Y, (int)Dimensions.X, (int)Dimensions.Y);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                                Camera.CurrentCameraTranslation);
            Sb.Draw(boundtx, BoundingBox, Color.White);
            Sb.Draw(boundtx, BoundingBox2, Color.White);
            Sb.Draw(Image, DoorRight, source, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            Sb.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
