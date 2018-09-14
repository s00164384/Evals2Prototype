using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evals2Prototype.Objects
{
   public class Wall :AnimatedSprite
    {
        Vector2 _dimensions;
        Rectangle _bounds;

        public Wall(Game g, Texture2D tx, Vector2 pos, Texture2D boundsTx, Vector2 dimen) : base(g, tx, pos, "wall", boundsTx, dimen)
        {
            _dimensions = dimen;
            _bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)_dimensions.X, (int)_dimensions.Y);
        }

        public override void Update(GameTime gameTime)
        {
            _bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)_dimensions.X, (int)_dimensions.Y);
            base.Update(gameTime);
        }
    }
}
