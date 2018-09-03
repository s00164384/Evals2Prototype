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
    class Player :AnimatedSprite
    {
        Vector2 movement;
        bool grounded;
        bool onSide;
        Vector2 oldPosition;
        List<AnimatedSprite> floors;
        public Player(Game g, Texture2D tx, Vector2 pos,Texture2D bounds,List<AnimatedSprite> f) : base(g,tx,pos,"player",bounds)
        {
            floors = f;
            grounded = false;
        }

        public override void Update(GameTime gameTime)
        {
            oldPosition = Position;
            bool jump = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !onSide)
            {
                movement.X += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && !onSide)
            {
                movement.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && grounded)
            {
                movement.Y -= 10;
                grounded = false;
                jump = true;
            }

            if (movement.X > 5)
                movement.X = 5;
            if (movement.X < -5)
                movement.X = -5;

            if (movement.Y < -15)
                movement.Y = -15;
        





            if (movement.X > 0)
                movement.X -= 0.2f;
            if (movement.X < 0)
                movement.X += 0.2f;

            if(!grounded)
                movement.Y += .5f;

            Rectangle predictiveMovementX = new Rectangle((int)Position.X + (int)movement.X, (int)Position.Y, Image.Width, Image.Height);
            Rectangle predictiveMovementY = new Rectangle((int)Position.X, (int)Position.Y + (int)movement.Y, Image.Width, Image.Height);

            foreach (AnimatedSprite a in floors)
            {
                if (predictiveMovementY.Intersects(a.BoundingBox))
                {
                    Position.Y = oldPosition.Y;
                    movement.Y = 0;
                    grounded = true;
                }
                if(predictiveMovementX.Intersects(a.BoundingBox))
                {
                    Position.X = oldPosition.X;
                    movement.X = 0;
                }
                else
                {
                    grounded = false;
                }
            }

            Move(movement);


            //Position.Y = collidingWith.BoundingBox.Top - (Image.Height); 
            //}
            //else
            //{
            //    onSide = false;
            //    grounded = false;
            //}




            base.Update(gameTime);
        }


    }
}
