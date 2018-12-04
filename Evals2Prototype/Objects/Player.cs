using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Evals2Prototype.Objects
{
    class Player :AnimatedSprite
    {
        enum _moveStates { UP,DOWN,LEFT,RIGHT,STOP };
        int _moveState;

        Vector2 movement;
        Texture2D[] _States;
        bool grounded;
        float gravity = 0f;
        CameraGuide _guide;
        
        Vector2 oldPosition;
        SoundEffect death;
        SpriteFont HUDtxt;
        int deaths = 0;
        Vector2 originPoint;
        List<Wall> floors;
        List<Enemy> enemies;
        List<Door> doors;

        Rectangle BoundingBoxBot;
        Rectangle BoundingBoxLeft;
        Rectangle BoundingBoxRight;
        Rectangle BoundingBoxTop;
        Rectangle BoundingBoxBot2;

        Color InRight = Color.Red;
        Color InLeft = Color.Red;


        public Player(Game g, Texture2D tx, Vector2 pos,Texture2D bounds,List<Wall> f,Vector2 dimen,int frames,Texture2D[] states,List<Enemy> e,SoundEffect oof,SpriteFont sf,CameraGuide guide, List<Door> door) : base(g,tx,pos,"player",bounds,dimen,frames)
        {
            floors = f;
            grounded = false;
            _States = states;
            originPoint = pos;
            enemies = e;
            doors = door;
            death = oof;
            HUDtxt = sf;
            _guide = guide;
            _guide.Visible = false;
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!Visible) return;
            oldPosition = Position;
            int LastState = _moveState;
           
            bool jump = false;

            if (!grounded)
                movement.Y += 1f;
            else
                _moveState = (int)_moveStates.STOP;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movement.X += -1;
                if(movement.X > 0)
                {
                  movement.X += -1;
                }
                if (grounded)
                    _moveState = (int)_moveStates.LEFT;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movement.X += 1;
                if (movement.X < 0)
                {
                    movement.X += 1;
                }
                if(grounded)
                _moveState = (int)_moveStates.RIGHT;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && grounded)
            {
                movement.Y -= 20;
                grounded = false;

                _moveState = (int)_moveStates.UP;
            }

            if (movement.X > 6)
                movement.X = 6;
            if (movement.X < -6)
                movement.X = -6;






            if (movement.Y > 0)
            {
                _moveState = (int)_moveStates.DOWN;
            }

            if (LastState != _moveState)
            {
                currentFrame = 0;
                switch (_moveState)
                {
                    case (int)_moveStates.RIGHT:
                        Image = _States[1];
                        break;
                    case (int)_moveStates.LEFT:
                        Image = _States[1];
                        break;
                    case (int)_moveStates.UP:
                        Image = _States[2];
                        break;
                    case (int)_moveStates.DOWN:
                        Image = _States[3];
                        break;
                    default:
                        Image = _States[0];
                        break;
                }
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
            BoundingBoxBot2 = new Rectangle((int)Position.X + (int)Dimensions.X/6 + (int)movement.X, (int)Position.Y + (int)Dimensions.Y + 2 + (int)movement.Y, ((int)Dimensions.X/3)*2, 2);
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
        
                #endregion
                #region ceilingCollision
                if (BoundingBoxTop.Intersects(a.BoundingBox))
                {
                    Position.Y = a.BoundingBox.Bottom + 1f;

                    movement.Y = 0;
                    grounded = true;

                    InCollision = Color.Green;
                    break;
                }
                #endregion

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
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);


            //Collision with enemy
            foreach (Enemy e in enemies)
            {
                if (BoundingBox.Intersects(e.BoundingBox) && e.tag == "enemy")
                {
                    movement = Vector2.Zero;
                    deaths++;
                    Position = originPoint;
                    death.Play();
                }
            }

            foreach(Door d in doors)
            {
                if(BoundingBox.Intersects(d.BoundingBox))
                {
                    _guide.Visible = true;
                    _guide.Position = d.DoorLeft;
                    _guide._destination = d.DoorRight;
                    _guide._dir = "right";
                    this.Visible = false;
                    movement = Vector2.Zero;
                }
                else if(BoundingBox.Intersects(d.BoundingBox2))
                {
                    _guide.Visible = true;
                    _guide.Position = d.DoorRight;
                    _guide._destination = d.DoorLeft;
                    _guide._dir = "left";
                    this.Visible = false;
                    movement = Vector2.Zero;
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
            Sb.Draw(boundtx, BoundingBoxBot2, InCollision);
            Sb.Draw(boundtx, BoundingBoxLeft, InLeft);
            Sb.Draw(boundtx, BoundingBoxRight, InRight);
            Sb.Draw(boundtx, BoundingBoxTop, InCollision);
            Sb.End();
            Sb.Begin();
            Sb.DrawString(HUDtxt, "Deaths: " + deaths, Vector2.Zero, Color.Red);
            Sb.End();

            base.Draw(gameTime);
        }

    }
}
