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
    public class AnimatedSprite
    {
        public Texture2D Image;
        public Texture2D boundtx;
        public Rectangle BoundingBox;
        public Rectangle source;
        public Vector2 oldPosition;
        public Vector2 Position;
        public Vector2 Dimensions;
        public Game game;
        public SpriteBatch Sb;
        float speed;
        public string _dir;
        public string tag;
        public Color InCollision = Color.White;
        public AnimatedSprite collidingWith;
        Vector2 movement = new Vector2(0, 0);
        SpriteEffects _effect;
        public bool Visible;
        public bool isDestroyed;

        //For animating
        int Frames = 1;
        public int currentFrame = 0;
        int timeBetweenFrames = 100;
        float timer = 0f;
        Rectangle sourceRectangle;

        public AnimatedSprite(Game g,Texture2D tx,Vector2 pos,string t,Texture2D bounds,Vector2 dimen,int framecount)
        {
            game = g;
            Visible = true;
            tag = t;
            boundtx = bounds;
            Image = tx;
            Position = new Vector2(pos.X,pos.Y);
            BoundingBox = new Rectangle((int)this.Position.X,(int)this.Position.Y,(int)dimen.X,(int)dimen.Y);
            speed = 5;
            Dimensions = dimen;
            Frames = framecount; 
            
            

        }

        public virtual void Update(GameTime gameTime)
        {
            oldPosition = Position;
            Frames = Image.Width / (int)Dimensions.X;

            timer += (float)gameTime.ElapsedGameTime.Milliseconds;
            if(_dir == "left")
            {
                _effect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                _effect = SpriteEffects.None;
            }
            //if the timer is greater then the time between frames, then animate
            if (timer > timeBetweenFrames)
            {
                //moce to the next frame
                currentFrame++;

                //if we have exceed the number of frames
                if (currentFrame > Frames - 1)
                {
                    currentFrame = 0;
                }
                //reset our timer
                timer = 0f;
            }
            //BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
            source = new Rectangle(currentFrame * (int)Dimensions.X, 0, (int)Dimensions.X, (int)Dimensions.Y);
            BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)Dimensions.X,(int)Dimensions.Y);




        }

        //public bool Collision(AnimatedSprite other)
        //{
        //    BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, 64, 64);
        //    Rectangle OtherBox = new Rectangle((int)other.Position.X, (int)other.Position.Y, other.Image.Width, other.Image.Height);
        //    if (BoundingBox.Intersects(OtherBox))
        //    {
        //        InCollision = Color.Green;
        //        collidingWith = other;
        //        return true;
        //    }
        //    else
        //    {
        //        InCollision = Color.White;
        //        collidingWith = null;
        //        return false;
        //    }
        //}

        public void Move(Vector2 movement)
        {
            this.Position += movement;
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
            Sb.Draw(Image,Position, source, Color.White,0f,Vector2.Zero,1.0f,_effect,0f);
            //Sb.Draw(boundtx, Position, BoundingBox, InCollision, 0f, Vector2.Zero, 1.0f, _effect, 0f);
            Sb.End();
            // TODO: Add your drawing code here

        }
    }
}
