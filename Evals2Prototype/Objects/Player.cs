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
        enum _moveStates { UP,DOWN,LEFT,RIGHT,STOP,ATTACK };
        int _moveState;

        Vector2 movement;
        Texture2D[] _States;
        bool grounded;
        float gravity = 0f;
        Weapon weapon;
        Gun gun;
        public int score;

        Vector2 oldPosition;
        SpriteFont HUDtxt;
        public int deaths = 0;
        Vector2 originPoint;
        public List<Wall> floors;
        public List<Enemy> enemies;
        public int tempScore;


        Rectangle BoundingBoxBot;
        Rectangle BoundingBoxLeft;
        Rectangle BoundingBoxRight;
        Rectangle BoundingBoxTop;
        Rectangle BoundingBoxBot2;

        Color InRight = Color.Red;
        Color InLeft = Color.Red;


        public Player(Game g, Vector2 pos,Texture2D bounds,Vector2 dimen,int frames,Texture2D[] states,SpriteFont sf) : base(g,states[0],pos,"player",bounds,dimen,frames)
        {
            grounded = false;
            _States = states;
            originPoint = pos;
            HUDtxt = sf;
            _dir = "right";
            weapon = new Weapon(g, states[4], new Vector2(pos.X, pos.Y + dimen.Y/2 - 7),"sword",bounds,new Vector2(68,6),5);
            gun = new Gun(g, states[5], new Vector2(0,0), "gun", bounds, new Vector2(1,1), 1);

        }

        public override void Update(GameTime gameTime)
        {
            if (!Visible) return;
   
            MovePlayer();
            Collisions();

            weapon.Move(movement);
            this.Move(movement);

            if (InputEngine.IsKeyHeld(Keys.Space) && !weapon.Active)
            {
                weapon.Active = true;
                Frames = 4;
                _moveState = (int)_moveStates.ATTACK;
                Image = _States[5];
            }
            if(InputEngine.IsKeyPressed(Keys.B) && gun.projectiles.Count < 5)
            {
                if (_dir == "right")
                    gun.projectiles.Add(new Projectile(game, _States[6], new Vector2(Position.X + Dimensions.X/2,Position.Y + Dimensions.Y/3), "bullet", boundtx, new Vector2(8, 8), 1, 5, 1));
                else
                    gun.projectiles.Add(new Projectile(game, _States[6], new Vector2(Position.X + Dimensions.X / 2, Position.Y + Dimensions.Y / 3), "bullet", boundtx, new Vector2(8, 8), 1, 5, -1));
            }
            weapon._dir = _dir;
            weapon.Update(gameTime);
            gun.Update(gameTime);
            foreach(Projectile p in gun.projectiles)
            {
                p.Update(gameTime);
            }




            base.Update(gameTime);
        }

        void MovePlayer()
        {
            oldPosition = Position;
            int LastState = _moveState;

            bool jump = false;

            if (!grounded)
                movement.Y += 1f;
            else
            {
                if (!weapon.Active)
                {
                    _moveState = (int)_moveStates.STOP;
                    Frames = 4;
                }
                gravity = 0f;
            }

            if (InputEngine.IsKeyHeld(Keys.Left))
            {
                movement.X += -1;
                if (movement.X > 0)
                {
                    movement.X += -1;
                }
                if (grounded)
                    _moveState = (int)_moveStates.LEFT;
            }
            if (InputEngine.IsKeyHeld(Keys.Right))
            {
                movement.X += 1;
                if (movement.X < 0)
                {
                    movement.X += 1;
                }
                if (grounded)
                    _moveState = (int)_moveStates.RIGHT;
            }
            if (InputEngine.IsKeyHeld(Keys.Up) && grounded)
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
            if(!weapon.Active)
                if (LastState != _moveState)
                {
                    currentFrame = 0;
                    switch (_moveState)
                    {
                        case (int)_moveStates.RIGHT:
                            Image = _States[1];
                            Frames = 7;
                            break;
                        case (int)_moveStates.LEFT:
                            Image = _States[1];
                            Frames = 7;
                            break;
                        case (int)_moveStates.UP:
                            Image = _States[2];
                            Frames = 2;
                            break;
                        case (int)_moveStates.DOWN:
                            Image = _States[3];
                            Frames = 2;
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

            if (_dir == "right")
            {
                weapon.Position = new Vector2(this.Position.X, this.Position.Y + Dimensions.Y / 2);
                gun.Position = new Vector2(this.Position.X, this.Position.Y + Dimensions.Y / 2);
            }
            if (_dir == "left")
            {
                weapon.Position = new Vector2(this.Position.X - 24, this.Position.Y + Dimensions.Y / 2);
                gun.Position = new Vector2(this.Position.X, this.Position.Y + Dimensions.Y / 2);
            }
        }
        void Collisions()
        {
            this.BoundingBoxBot = new Rectangle(((int)Position.X + (int)Dimensions.X / 6) + (int)movement.X, (int)Position.Y + (int)Dimensions.Y - 2 + (int)movement.Y, ((int)Dimensions.Y / 3) * 2, 2);
            this.BoundingBoxBot2 = new Rectangle(((int)Position.X + (int)Dimensions.X / 6) + (int)movement.X, (int)Position.Y + (int)Dimensions.Y + 2 + (int)movement.Y, ((int)Dimensions.X / 3) * 2, 2);
            this.BoundingBoxLeft = new Rectangle((int)Position.X + (int)movement.X - 2, (int)Position.Y + (int)Dimensions.Y / 4 + (int)movement.Y, 2, (int)Dimensions.Y / 2);
            this.BoundingBoxRight = new Rectangle((int)Position.X + (int)Dimensions.X + (int)movement.X, ((int)Position.Y + (int)Dimensions.Y / 4) + (int)movement.Y, 2, (int)Dimensions.Y / 2);
            this.BoundingBoxTop = new Rectangle((int)Position.X + (int)Dimensions.X / 3 + (int)movement.X, (int)Position.Y + (int)movement.Y, (int)Dimensions.Y / 3, 2);

            this.BoundingBox = new Rectangle((int)Position.X + (int)movement.X, (int)Position.Y + (int)movement.Y, (int)Dimensions.X, (int)Dimensions.Y);


            bool stillGrounded = false;
    
            foreach (AnimatedSprite a in floors)
            {
                #region movement
                #region floorCollision
                if (BoundingBoxBot.Intersects(a.BoundingBox))
                {
                            Position.Y = a.BoundingBox.Top - Dimensions.Y - 2;
                            gravity = 0f;
                            movement.Y = 0;
                            grounded = true;
                            InCollision = Color.Green;

                }
     
                #endregion
                #region rightCollision
                if (BoundingBoxRight.Intersects(a.BoundingBox))
                {

                    Position.X -= 2f;
                    movement.X = 0f;
                    InRight = Color.Green;
                    
                }

             
                #endregion
                #region leftCollision
                if (BoundingBoxLeft.Intersects(a.BoundingBox))
                {
                    Position.X += 2f;
                            movement.X = 0f;
                            InLeft = Color.Green;
                }

        
                #endregion
                #region ceilingCollision
                if (BoundingBoxTop.Intersects(a.BoundingBox))
                {
                    Position.Y = a.BoundingBox.Bottom + 1f;

                    movement.Y = 0;
                    grounded = true;

                    InCollision = Color.Green;

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
                if (BoundingBoxBot2.Intersects(a.BoundingBox))
                {
                    stillGrounded = true;
                    //if (!grounded)
                    //{
                    //    Position = oldPosition;
                    //    movement.Y = 0;
                    //    grounded = true;
                    //}
                }
            }

            if (!stillGrounded)
            {
                grounded = false;
            }

            BoundingBoxBot = new Rectangle((int)Position.X + (int)Dimensions.X / 6 + (int)movement.X, (int)Position.Y + (int)Dimensions.Y - 2 + (int)movement.Y, ((int)Dimensions.Y / 3) * 2, 2);
            BoundingBoxBot2 = new Rectangle((int)Position.X + (int)movement.X, (int)Position.Y + (int)Dimensions.Y + 2 + (int)movement.Y, (int)Dimensions.X, 2);
            BoundingBoxLeft = new Rectangle((int)Position.X + (int)movement.X, (int)Position.Y + (int)Dimensions.Y / 4 + (int)movement.Y, 2, (int)Dimensions.Y / 2);
            BoundingBoxRight = new Rectangle((int)Position.X + (int)Dimensions.X + (int)movement.X - 2, (int)Position.Y + (int)Dimensions.Y / 4 + (int)movement.Y, 2, (int)Dimensions.Y / 2);
            BoundingBoxTop = new Rectangle((int)Position.X + (int)Dimensions.X / 3 + (int)movement.X, (int)Position.Y + (int)movement.Y, (int)Dimensions.Y / 3, 2);

            this.BoundingBox = new Rectangle((int)Position.X + (int)movement.X, (int)Position.Y + (int)movement.Y, (int)Dimensions.X, (int)Dimensions.Y);


            //Collision with enemy
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy e = enemies.ElementAt(i);
                if (weapon.BoundingBox.Intersects(e.BoundingBox) && e.tag == "enemy")
                {
                    movement = Vector2.Zero;
                    e.isDestroyed = true;
            
                    enemies.RemoveAt(i);
                    i--;
                    tempScore += 200;
                    return;
                }
             

                if (BoundingBox.Intersects(e.BoundingBox) && e.tag == "enemy")
                {
                    movement = Vector2.Zero;
                    Position = originPoint;
                    score -= 200;
                    deaths++;
                }
            }



        }




        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
            //Sb.Draw(boundtx, BoundingBoxBot2, InCollision);
            //Sb.Draw(boundtx, BoundingBoxLeft, InLeft);
            //Sb.Draw(boundtx, BoundingBoxRight, InRight);
            //Sb.Draw(boundtx, BoundingBoxTop, InCollision);
            Sb.End();
            Sb.Begin();
            //Sb.DrawString(HUDtxt, "X: " + Position.X, Vector2.Zero, Color.Red);
            //Sb.DrawString(HUDtxt, "Y: " + Position.Y, new Vector2(0,37), Color.Red);
            //Sb.DrawString(HUDtxt, "MoveX: " + movement.X.ToString(), new Vector2(0, 74), Color.Red);
            //Sb.DrawString(HUDtxt, "MoveY: " + movement.Y.ToString(), new Vector2(0, 110), Color.Red);
            Sb.DrawString(HUDtxt, "Score: " + score, new Vector2(64,32), Color.Red);
            Sb.DrawString(HUDtxt, "Enemies Remaining: " + enemies.Count, new Vector2(64, 64), Color.Red);
            if(deaths > 0)
            {
                Vector2 size = HUDtxt.MeasureString("Deaths:");
                Sb.DrawString(HUDtxt, "Deaths:", new Vector2(game.GraphicsDevice.Viewport.Bounds.Width-size.X, game.GraphicsDevice.Viewport.Bounds.Height - size.Y - 64 ), Color.Red);
                Sb.DrawString(HUDtxt, deaths.ToString(), new Vector2(game.GraphicsDevice.Viewport.Bounds.Width - size.X + size.X / 2, game.GraphicsDevice.Viewport.Bounds.Height - size.Y),Color.Red);
            }
            Sb.End();

            weapon.Draw(gameTime);
            gun.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}
