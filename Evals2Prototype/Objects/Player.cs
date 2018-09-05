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
        string _dir;
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
            movement = Vector2.Zero;
            bool jump = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movement.X += -5;
                _moveState = (int)_moveStates.LEFT;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movement.X += 5;
                _moveState = (int)_moveStates.RIGHT;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && grounded)
            {
                movement.Y -= 10;
                grounded = false;

                _moveState = (int)_moveStates.UP;
            }

            if (movement.X > 5)
                movement.X = 5;
            if (movement.X < -5)
                movement.X = -5;

            if (movement.Y < -15)
                movement.Y = -15;

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

            if(movement.X <= 0.2f && movement.X >= -0.2f)
            {
                movement.X = 0;
            }
            




            
            Rectangle predictiveMovementY = new Rectangle((int)Position.X, (int)Position.Y - 1, Image.Width, Image.Height);

            foreach (AnimatedSprite a in floors)
            {

                if (BoundingBox.Intersects(a.BoundingBox))
                {
                    switch (_moveState)
                    {
                        case (int)_moveStates.DOWN:
                            Position.Y = oldPosition.Y  - 0.5f;
                            movement = Vector2.Zero;
                            grounded = true;
                            break;
                        case (int)_moveStates.UP:
                            Position.Y = oldPosition.Y + 0.5f;
                            movement = Vector2.Zero;
                            break;
                        case (int)_moveStates.LEFT:
                            Position.X = oldPosition.X + 0.5f;
                            movement = Vector2.Zero;
                            break;
                        case (int)_moveStates.RIGHT:
                            Position.X = oldPosition.X - 0.5f;
                            movement = Vector2.Zero;
                            break;


                    }
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
