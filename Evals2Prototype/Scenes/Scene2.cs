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
    class Scene2 :Scene
    {
        Tileset t,t2;
        Texture2D testSprite;
        SpriteFont _sf;
        Rectangle bounds;

        public Scene2(Game g) :base(g)
        {
            SetupRoom();
            t = new Tileset(g, new Vector2(0,0));
            t.Tiles = new int[][]
         {
                new int[] { 0, 0, 0,0,0,0,0,0,0,0,0,0 },
                new int[] { 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 1, 0, 1 },
                new int[] { 0, 1, 1, 0, 0, 1 },
                new int[] { 0, 0, 0, 1, 0, 1 },
                new int[] { 1, 0, 0, 1, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {0, 0, 0, 0, 0, 0 },
                new int[] {1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
         };
            t2 = new Tileset(g, new Vector2(1, 0));
            t2.Tiles = new int[][]
         {
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
         };
            _name = "Second Level";
        }

        protected override void LoadContent()
        {
            bounds = new Rectangle(0, 0, 5000, 5000);
            bgm = game.Content.Load<Song>("Sounds/dnb");
            List<Wall> floor = new List<Wall>();
            Texture2D wallTX = game.Content.Load<Texture2D>("Sprites/Scene2/wall");
            t.SetTiles(wallTX);
            t2.SetTiles(wallTX);
            // Create a new SpriteBatch, which can be used to draw textures.
            _sf = game.Content.Load<SpriteFont>("Fonts/Score");
            

            testSprite = game.Content.Load<Texture2D>("Backgrounds/xp");

            foreach(Wall w in t.floor)
            {
                Components.Add(w);
                floor.Add(w);
            }
            foreach (Wall w in t2.floor)
            {
                floor.Add(w);
                Components.Add(w);
            }
            List<Enemy> enemies = new List<Enemy>
            {
            new Enemy(game, game.Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(0, 136), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), 1, t.floor, 1),
            new Enemy(game, game.Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(1000, 136), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), -1, t.floor, 1),
            new Enemy(game, game.Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(450, 286), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), -1, t.floor, 1)
            };
            foreach (Enemy e in enemies)
            {
                Components.Add(e);
            }
            List<Door> doors = new List<Door>
            {
                //new Door(game, game.Content.Load<Texture2D>("Sprites/floor"),new Vector2(200,150),new Vector2(1200,150),game.Content.Load<Texture2D>("Sprites/hitbox"),new Vector2(32,64),1)
            };
            foreach (Door d in doors)
            {
                this.Components.Add(d);
            }
            CameraGuide _tager = new CameraGuide(game, game.Content.Load<Texture2D>("Sprites/tager"), Vector2.Zero, game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), 0, this);
            Components.Add(_tager);


            SoundEffect oof = game.Content.Load<SoundEffect>("Sounds/oof");
            

            Player testPlayer = new Player(game, game.Content.Load<Texture2D>("Sprites/evals"), new Vector2(608, 500), game.Content.Load<Texture2D>("Sprites/hitbox"), floor, new Vector2(64, 64), 4, new Texture2D[] { game.Content.Load<Texture2D>("Sprites/evals"), game.Content.Load<Texture2D>("Sprites/evalsRight"), game.Content.Load<Texture2D>("Sprites/evalsJump"), game.Content.Load<Texture2D>("Sprites/evalsFall") }, enemies, oof, _sf, _tager, doors);
            this.Components.Add(testPlayer);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!active) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
            Sb.DrawString(_sf,"test", new Vector2(0,0), Color.White);
            Sb.End();
            base.Draw(gameTime);
        }


        void SetupRoom()
        { }
    }
}
