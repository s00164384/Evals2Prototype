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
        enum _moveStates { UP,DOWN,LEFT,RIGHT,STOP };
        int _moveState;

        Vector2 movement;
        
        bool grounded;
        float gravity = 0f;
        string _dir;
        Vector2 oldPosition;
        List<Wall> floors;

        Rectangle BoundingBoxBot;
        Rectangle BoundingBoxLeft;
        Rectangle BoundingBoxRight;
        Rectangle BoundingBoxTop;

        Color InRight = Color.Red;
        Color InLeft = Color.Red;


        public Player(Game g, Texture2D tx, Vector2 pos,Texture2D bounds,List<Wall> f,Vector2 dimen) : base(g,tx,pos,"player",bounds,dimen)
        {
            floors = f;
            grounded = false;
        }

        public override void Update(GameTime gameTime)
        {
            oldPosition = Position;
           
            bool jump = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movement.X += -1;
                if(movement.X > 0)
                {
                  movement.X += -1;
                }
                _moveState = (int)_moveStates.LEFT;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movement.X += 1;
                if (movement.X < 0)
                {
                    movement.X += 1;
                }
                _moveState = (int)_moveStates.RIGHT;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && grounded)
            {
                movement.Y -= 20;
                grounded = false;

                _moveState = (int)_moveStates.UP;
            }

            if (movement.X > 8)
                movement.X = 8;
            if (movement.X < -8)
                movement.X = -8;



            if (!grounded)
                movement.Y += 1f;


            if (movement.Y > 0)
            {
                _moveState = (int)_moveStates.DOWN;
            }






            if (movement.X > 0)
            {
                _dir = "right";
                movement.X -= 0.2f;
            }
            if (movement.X < 0)
            {
                _dir = "left";
                movement.X += 0.2f;
            }

            if (movement.X <= 0.2f && movement.X >= -0.2f)
            {
                movement.X = 0;
            }




            bool stillGrounded = false;
            
            BoundingBoxBot = new Rectangle((int)Position.X + (int)Dimensions.X/6 + (int)movement.X, (int)Position.Y + (int)Dimensions.Y - 2 + (int)movement.Y, ((int)Dimensions.Y/3) *2, 2);
            Rectangle BoundingBoxBot2 = new Rectangle((int)Position.X + (int)Dimensions.X/6 + (int)movement.X, (int)Position.Y + (int)Dimensions.Y + 2 + (int)movement.Y, ((int)Dimensions.X/3)*2, 2);
            BoundingBoxLeft = new Rectangle((int)Position.X + (int)movement.X, (int)Position.Y + (int)Dimensions.Y / 4 + (int)movement.Y, 2, (int)Dimensions.Y / 2);
            BoundingBoxRight = new Rectangle((int)Position.X + (int)Dimensions.X + (int)movement.X, (int)Position.Y + (int)Dimensions.Y/4 + (int)movement.Y, 2, (int)Dimensions.Y/2);
            BoundingBoxTop = new Rectangle((int)Position.X + (int)Dimensions.X / 3 + (int)movement.X, (int)Position.Y + (int)movement.Y, (int)Dimensions.Y / 3, 2);

            foreach (AnimatedSprite a in floors)
            {
                #region movement
                #region floorCollision
                if (BoundingBoxBot.Intersects(a.BoundingBox))
                {           
                            Position.Y = a.BoundingBox.Y - 64 - 1f;
                            gravity = 0f;
                    movement.Y = 0;
                            grounded = true;

                    InCollision = Color.Green;
                    break;
                }
                //else
                //{
                //    if (Position.Y != a.BoundingBox.Y - 64 - 1f)
                //    {
                //        grounded = false;
                //        InCollision = Color.Red;
                //    }
                //    else
                //    {
                //        if(movement.Y > 0)
                //        movement.Y = 0f;
                //        grounded = true;
                        
                //    }
                //}
                #endregion
                #region rightCollision
                if (BoundingBoxRight.Intersects(a.BoundingBox))
                {

                            Position.X = a.BoundingBox.X - 64 - 2f;
                            movement.X = 0f;
                    InRight = Color.Green;
                    break;
                    
                }
                else
                {
                    InRight = Color.Red;
                }
                //else
                //{
                //    if (Position.X != a.BoundingBox.X - 64 - 1f)
                //    {
                //        grounded = false;
                //        InCollision = Color.Red;
                //    }
                //    else
                //    {
                //        if (movement.Y > 0)
                //            movement.Y = 0f;
                //        grounded = true;
                //        break;
                //    }
                //}
                #endregion
                #region leftCollision
                if (BoundingBoxLeft.Intersects(a.BoundingBox))
                {

                            Position.X = a.BoundingBox.Right +1f;
                            gravity = 0f;
                            movement.X = 0f;
                    InLeft = Color.Green;
                    break;
                }
                else
                {
                    InLeft = Color.Red;
                }
                //else
                //{
                //    if (Position.Y != a.BoundingBox.Y - 64 - 1f)
                //    {
                //        grounded = false;
                //        InCollision = Color.Red;
                //    }
                //    else
                //    {
                //        if (movement.Y > 0)
                //            movement.Y = 0f;
                //        grounded = true;
                //        break;
                //    }
                //}
                #endregion
                if (BoundingBoxTop.Intersects(a.BoundingBox))
                {
                    Position.Y = a.BoundingBox.Bottom + 1f;

                    movement.Y = 0;
                    grounded = true;

                    InCollision = Color.Green;
                    break;
                }

                if (grounded)
                {
                    InCollision = Color.Blue;
                }
                #endregion

                //if (!BoundingBoxBot2.Intersects(a.BoundingBox) && InRight == Color.Red && InLeft == Color.Red)
                //    grounded = false;

            }
            foreach (Wall a in floors)
            {
                if(BoundingBoxBot2.Intersects(a.BoundingBox))
                {
                    stillGrounded = true;
                }
            }

            if(!stillGrounded)
            {
                grounded = false;
            }
                Move(movement);


            //Position.Y = collidingWith.BoundingBox.Top - (64); 
            //}
            //else
            //{
            //    onSide = false;
            //    grounded = false;
            //}




            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate);
            Sb.Draw(boundtx, BoundingBoxBot, InCollision);
            Sb.Draw(boundtx, BoundingBoxLeft, InLeft);
            Sb.Draw(boundtx, BoundingBoxRight, InRight);
            Sb.Draw(boundtx, BoundingBoxTop, InCollision);
            Sb.End();
            base.Draw(gameTime);
        }

    }
}
