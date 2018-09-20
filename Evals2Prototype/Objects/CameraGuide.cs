using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Evals2Prototype.Objects
{
    class CameraGuide : AnimatedSprite
    {
        public string _dir;
        public Vector2 _destination;
        public CameraGuide(Game g, Texture2D tx, Vector2 pos, Texture2D boundsTx, Vector2 dimen, int frames) : base(g, tx, pos, "camera", boundsTx, dimen, frames)
        {
            Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Visible) return;
            switch(_dir)
            {
                case "left":
                    Position.X += -20;
                    break;
                case "right":
                    Position.X += 20;
                    break;
                case "up":
                    Position.Y += -20;
                    break;
                case "down":
                    Position.Y += 20;
                    break;
            }





            Player p;
            if (Position == _destination)
            {
                p = (Player)Game.Components
                  .FirstOrDefault(pl => pl.GetType() == typeof(Player));
                if (p != null)
                {
                    switch (_dir)
                    {
                        case "left":
                            p.Position = new Vector2(_destination.X - p.Dimensions.X, _destination.Y);
                            break;
                        case "right":
                            p.Position = new Vector2(_destination.X + p.Dimensions.X, _destination.Y);
                            break;
                        case "up":
                            p.Position = new Vector2(_destination.X , _destination.Y - p.Dimensions.Y);
                            break;
                        case "down":
                            p.Position = new Vector2(_destination.X, _destination.Y + p.Dimensions.Y);
                            break;
                    }
                    this.Visible = false;
                    p.Visible = true;

                }
                    
            }
            base.Update(gameTime);
        }
    }
}
