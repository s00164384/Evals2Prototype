using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
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
        Tileset t,t2,t3,t4;
        Texture2D testSprite;
        SpriteFont _sf;
        Rectangle bounds;
        Tile test;
        Tiles jsonTileset;
        string json;
        Texture2D background;
        Player testPlayer;


        public Scene2(Game g) : base(g)
        {
 
            if (File.Exists("test.json"))
            {
                {
                    using (StreamReader sr = new StreamReader("test.json"))
                    {
                        json = sr.ReadToEnd();
                        jsonTileset = JsonConvert.DeserializeObject<Tiles>(json);

                    }
                }
            }


            t = new Tileset(g, new Vector2(0, 0));
            t.jsonObj = new Tile
            {
                layout = jsonTileset.tiles[0].layout,
                type = jsonTileset.tiles[0].type
            };
            t2 = new Tileset(g, new Vector2(1, 0));
            t2.jsonObj = new Tile
            {
                layout = jsonTileset.tiles[1].layout,
                type = jsonTileset.tiles[1].type
            };

            t3 = new Tileset(g, new Vector2(1, 1));
            t3.jsonObj = new Tile
            {
                layout = jsonTileset.tiles[2].layout,
                type = jsonTileset.tiles[2].type
            };
            t4 = new Tileset(g, new Vector2(0, 1));
            t4.jsonObj = new Tile
            {
                layout = jsonTileset.tiles[2].layout,
                type = jsonTileset.tiles[2].type
            };
            _name = "Second Level";
        }

        protected override void LoadContent()
        {
       
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!active) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
            Sb.Draw(background, bounds, Color.White);
            Sb.DrawString(_sf,json, new Vector2(0,0), Color.White);
            Sb.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            if(testPlayer.deaths == 3)
            {
                active = false;
                gotoMenu = true;
            }
            base.Update(gameTime);
        }

        public override void SetupRoom(Assets content)
        {
            bounds = new Rectangle(0, 0, 5000, 5000);
            background = content.Backgrounds[1];
            bgm = content.Songs[0];
            List<Enemy> enemies = new List<Enemy>();
            List<Wall> floor = new List<Wall>();
            walltx = content.Wall;
            Texture2D enemytx = content.Enemy;
            Texture2D debugBox = content.DebugBox;
            // Create a new SpriteBatch, which can be used to draw textures.
            _sf = content.Font;

            t.SetTiles(walltx, enemytx, debugBox);
            t2.SetTiles(walltx, enemytx, debugBox);
            t3.SetTiles(walltx, enemytx, debugBox);
            t4.SetTiles(walltx, enemytx, debugBox);
            testSprite = content.Backgrounds[0];

            foreach (Wall w in t.floor)
            {
                floor.Add(w);
                Components.Add(w);
            }
            foreach (Wall w in t2.floor)
            {
                floor.Add(w);
                Components.Add(w);
            }
            foreach (Wall w in t3.floor)
            {
                floor.Add(w);
                Components.Add(w);
            }
            foreach (Wall w in t4.floor)
            {
                floor.Add(w);
                Components.Add(w);
            }

            foreach (Enemy e in t.enemies)
            {
                enemies.Add(e);
                Components.Add(e);
            }
            foreach (Enemy e in t2.enemies)
            {
                enemies.Add(e);
                Components.Add(e);
            }

            foreach (Enemy e in t3.enemies)
            {
                enemies.Add(e);
                Components.Add(e);
            }
            foreach (Enemy e in t4.enemies)
            {
                enemies.Add(e);
                Components.Add(e);
            }

            testPlayer = new Player(game, new Vector2(608, 300), debugBox, new Vector2(46, 48), 4, content.Player, _sf);
            testPlayer.floors = floor;
            testPlayer.enemies = enemies;
            this.Components.Add(testPlayer);
        }
    }
}
