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

        List<Collectable> collectables = new List<Collectable>();



        public Scene2(Game g) : base(g)
        {
            
            if (File.Exists("././test.json"))
            {
                {
                    using (StreamReader sr = new StreamReader("././test.json"))
                    {
                        json = sr.ReadToEnd();
                        jsonTileset = JsonConvert.DeserializeObject<Tiles>(json);

                    }
                }
            }

            t = new Tileset(g, new Vector2(0, 0));
            t.fromFile = jsonTileset;
            t.layout = new Room[4][];
            for (int i = 0; i < t.layout.Length; i++)
            {
                t.layout[i] = new Room[4];
            }

            t.CreateMap();
            t.createRooms();
            //t.jsonObj = new Tile
            //{
            //    layout = jsonTileset.tiles[0].layout,
            //    entrance = jsonTileset.tiles[0].entrance,
            //    exit = jsonTileset.tiles[0].exit
            //};
            //t2 = new Tileset(g, new Vector2(2, 1));
            //t2.jsonObj = new Tile
            //{
            //    layout = jsonTileset.tiles[1].layout,
            //    entrance = jsonTileset.tiles[1].entrance,
            //    exit = jsonTileset.tiles[1].exit
            //};

            //t3 = new Tileset(g, new Vector2(1, 1));
            //t3.jsonObj = new Tile
            //{
            //    layout = jsonTileset.tiles[2].layout,
            //    entrance = jsonTileset.tiles[2].entrance,
            //    exit = jsonTileset.tiles[2].exit
            //};
            //t4 = new Tileset(g, new Vector2(0, 1));
            //t4.jsonObj = new Tile
            //{
            //    layout = jsonTileset.tiles[3].layout,
            //    entrance = jsonTileset.tiles[3].entrance,
            //    exit = jsonTileset.tiles[3].exit
            //};
            _name = "Second Level";
        }

        public override void SetupRoom(Assets content)
        {
            bounds = new Rectangle(0, 0, 5000, 5000);
            background = content.Backgrounds[1];
            bgm = content.Songs[1];
            List<Enemy> enemies = new List<Enemy>();
            List<Wall> floor = new List<Wall>();

            walltx = content.Wall;
            Texture2D enemytx = content.Enemy;
            Texture2D debugBox = content.DebugBox;
            // Create a new SpriteBatch, which can be used to draw textures.
            _sf = content.Font;

            t.SetTiles(content,ref testPlayer);
            //t2.SetTiles(walltx, enemytx, debugBox, content.Collectable);
            //t3.SetTiles(walltx, enemytx, debugBox, content.Collectable);
            //t4.SetTiles(walltx, enemytx, debugBox, content.Collectable);
            testSprite = content.Backgrounds[0];

            foreach (Wall w in t.floor)
            {
            
                floor.Add(w);
                Components.Add(w);
            }
            //foreach (Wall w in t2.floor)
            //{
            //    floor.Add(w);
            //    Components.Add(w);
            //}
            //foreach (Wall w in t3.floor)
            //{
            //    floor.Add(w);
            //    Components.Add(w);
            //}
            //foreach (Wall w in t4.floor)
            //{
            //    floor.Add(w);
            //    Components.Add(w);
            //}

            foreach (Enemy e in t.enemies)
            {
                enemies.Add(e);
                Components.Add(e);
            }
            //foreach (Enemy e in t2.enemies)
            //{
            //    enemies.Add(e);
            //    Components.Add(e);
            //}

            //foreach (Enemy e in t3.enemies)
            //{
            //    enemies.Add(e);
            //    Components.Add(e);
            //}
            //foreach (Enemy e in t4.enemies)
            //{
            //    enemies.Add(e);
            //    Components.Add(e);
            //}
            foreach(Collectable c in t.collectables)
            {
                collectables.Add(c);
                Components.Add(c);
            }
            //foreach (Collectable c in t2.collectables)
            //{
            //    collectables.Add(c);
            //    Components.Add(c);
            //}
            //foreach (Collectable c in t3.collectables)
            //{
            //    collectables.Add(c);
            //    Components.Add(c);
            //}
            //foreach (Collectable c in t4.collectables)
            //{
            //    collectables.Add(c);
            //    Components.Add(c);
            //}

           
            testPlayer.floors = floor;
            testPlayer.enemies = enemies;
            this.Components.Add(testPlayer);
        }

        //void CreateRoom()
        //{
        //    Random r = new Random();
        //    Vector2 location;


        //    int start = r.Next(0, 4);
        //    layout[0][start] = "start";
        //    location = new Vector2(start, 0);
        //    int next = r.Next(0, 6);
        //    while(next != 5 && location.Y != 3)
        //    {
        //        if(next == 1 || next ==2)
        //        {
        //            if ((int)location.X - 1 < 0 || layout[(int)location.Y][(int)location.X-1] == "right")
        //                next = 5;
        //        }
        //        if (next == 3|| next == 4)
        //        {
        //            if ((int)location.X + 1 > 3 || layout[(int)location.Y][(int)location.X+1] == "left")
        //                next = 5;
        //        }
        //        switch (next)
        //        {
        //            case 1:
        //            case 2:
        //                location.X -= 1;
        //                layout[(int)location.Y][(int)location.X] = "left";
        //                break;
        //            case 3:
        //            case 4:
        //                location.X += 1;
        //                layout[(int)location.Y][(int)location.X] = "right";
        //                break;
        //            case 5:
        //                location.Y += 1;
        //                layout[(int)location.Y][(int)location.X] = "down";     
        //                break;
        //        }
        //        next = r.Next(0, 6);
        //    }


        //}





        public override void Update(GameTime gameTime)
        {
            for(int i = 0;i<collectables.Count;i++)
            {
                if(collectables[i].BoundingBox.Intersects(testPlayer.BoundingBox))
                {
                    collectables[i].Visible = false;
                    testPlayer.tempScore += collectables[i].scoreValue;
                    collectables.RemoveAt(i);
                    i--;
                }
            }

            if(testPlayer.tempScore > 0)
            {
                testPlayer.score += 5;
                testPlayer.tempScore -= 5;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!active) return;
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Camera.CurrentCameraTranslation);
            Sb.Draw(background, bounds, Color.White);
  
            Sb.End();
            //Sb.Begin();
            //for (int i = 0; i < layout.Length; i++)
            //{
            //    for (int j = 0; j < layout[i].Length; j++)
            //    {
            //        if(layout[i][j] != null)
            //        Sb.DrawString(_sf, layout[i][j], new Vector2(108 * j + 64, 40 * i + 500), Color.Beige);
            //    }
            //}
            //Sb.End();
            base.Draw(gameTime);
        }

   
    }
}
