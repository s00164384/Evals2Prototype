using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evals2Prototype.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;



namespace Evals2Prototype.Scenes
{
    class Menu :Scene
    {
        SpriteBatch spriteBatch;
        Texture2D testSprite;
        SpriteFont _sf;
        Song backingTrack;
        Rectangle bounds;

        public Menu(Game g) :base(g)
        {
            _name = "Menu";
        }

        protected override void LoadContent()
        {

            backingTrack = game.Content.Load<Song>("Sounds/bg");
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(backingTrack);
       
            // Create a new SpriteBatch, which can be used to draw textures.
            _sf = game.Content.Load<SpriteFont>("Fonts/Score");


            testSprite = game.Content.Load<Texture2D>("Backgrounds/xp");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!active) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
            //Sb.Draw(testSprite, bounds, Color.White);
            Sb.End();
            base.Draw(gameTime);
        }
    }
}
