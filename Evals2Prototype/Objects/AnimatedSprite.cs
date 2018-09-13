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
    public class AnimatedSprite : DrawableGameComponent
    {
        public Texture2D Image;
        public Texture2D boundtx;
        public Rectangle BoundingBox;
        public Vector2 oldPosition;
        public Vector2 Position;
        public Game game;
        public SpriteBatch Sb;
        float speed;
        public string tag;
        public Color InCollision = Color.White;
        public AnimatedSprite collidingWith;
        Vector2 movement = new Vector2(0, 0);

        //For animating
        int Frames = 0;
        int currentFrame = 0;
        int timeBetweenFrames = 100;
        float timer = 0f;
        Rectangle sourceRectangle;

        public AnimatedSprite(Game g,Texture2D tx,Vector2 pos,string t,Texture2D bounds):base(g)
        {
            game = g;
            Visible = true;
            tag = t;
            boundtx = bounds;
            Image = tx;
            Position = new Vector2(pos.X,pos.Y);
            BoundingBox = new Rectangle((int)this.Position.X,(int)this.Position.Y,Image.Width,Image.Height);
            speed = 5;
            g.Components.Add(this);

        }

        public override void Update(GameTime gameTime)
        {
            oldPosition = Position;
            BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, Image.Width, Image.Height);


            base.Update(gameTime);
        }

        public bool Collision(AnimatedSprite other)
        {
            BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, Image.Width, Image.Height);
            Rectangle OtherBox = new Rectangle((int)other.Position.X, (int)other.Position.Y, other.Image.Width, other.Image.Height);
            if (BoundingBox.Intersects(OtherBox))
            {
                InCollision = Color.Green;
                collidingWith = other;
                return true;
            }
            else
            {
                InCollision = Color.White;
                collidingWith = null;
                return false;
            }
        }

        public void Move(Vector2 movement)
        {
            Position += movement;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate);
            Sb.Draw(Image, Position, Color.White);
            //Sb.Draw(boundtx, BoundingBox, InCollision);
            Sb.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
