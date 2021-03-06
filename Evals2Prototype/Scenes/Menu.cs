﻿using System;
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
        Texture2D logo;
        public bool selectionMade;

        int[] menuOptions = new int[2];
        string[] menu = { "Play", "Exit" };
        public int selection = 0;

        public Menu(Game g) :base(g)
        {
            _name = "Menu";
            bounds = new Rectangle(0,0,5000, 5000);
  
        }

        protected override void LoadContent()
        {

            bgm = game.Content.Load<Song>("Sounds/cloud");
            //MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(bgm);
            logo = game.Content.Load<Texture2D>("Sprites/logo");

            // Create a new SpriteBatch, which can be used to draw textures.
            _sf = game.Content.Load<SpriteFont>("Fonts/Score");


            testSprite = game.Content.Load<Texture2D>("Backgrounds/bg2");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!active) return;
            selectionMade = false;
            if (InputEngine.IsKeyPressed(Keys.Up))
            {
                selection--;
            }
            else if(InputEngine.IsKeyPressed(Keys.Down))
            {
                selection++;
            }

            if(InputEngine.IsKeyPressed(Keys.NumPad1))
            {
                selection = 0;
            }
            if (InputEngine.IsKeyPressed(Keys.NumPad2))
            {
                selection = 1;
            }


            if (selection >= menuOptions.Length)
            {
                selection = 0;
            }
            if(selection < 0)
            {
                selection = menuOptions.Length - 1;
            }

            if(InputEngine.IsKeyPressed(Keys.Enter))
            {
                selectionMade = true;
            }
          


            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if (!active) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin();
            Sb.Draw(testSprite, bounds, Color.White);
            Sb.Draw(logo,Vector2.Zero , Color.Red);
            Sb.DrawString(_sf, menu[selection], new Vector2(500, 500), Color.White);
            Sb.End();
            base.Draw(gameTime);
        }
    }
}
