using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Evals2Prototype.Objects
{
    class Tileset : DrawableGameComponent
    {
        public enum TileType { wall,ground}
        public int[][] Tiles;
        public Room[][] layout;
        public Tile jsonObj;
        public Tiles fromFile;
        Vector2 setPosition;
        Game game;
        int start, end;
        public List<Wall> floor = new List<Wall>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Collectable> collectables = new List<Collectable>();
        public Tileset(Game g,Vector2 pos): base(g)
        {
            game = g;
           setPosition = pos;
            jsonObj = new Tile();
       
        }

        public void CreateMap()
        {
            Random r = new Random();
            int reset = 0;

            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    layout[i][j] = new Room { entrance = 0,
                        exit = 0,
                        layout = new Tile {entrance="cheese",
                            exit ="pasta",
                            layout = new int[][]
                            {
                                new int[]{1,1,1,1,1},
                                new int[]{1,1,1,1,1},
                                new int[]{1,1,1,1,1},
                                new int[]{1,1,1,1,1},
                                new int[]{1,1,1,1,1}
                            }
                            } };
                }
            }
            int next = 0;

            start = r.Next(0, 4);
            end = r.Next(0, 4);


            bool wall;
            Vector2 location = new Vector2(start, 0);
            Vector2 finish = new Vector2(end, 3);
            next = r.Next(1, 6);
            while (!(location == finish) && location.Y < 4 && reset < 5)
            {
                Vector2 tempLocation = location;
                wall = false;
                reset++;

                switch (next)
                {
                    case 1:
                    case 2:
                        Debug.WriteLine("moving right");
                        if (location.X + (int)1 < 4)
                            if (layout[(int)location.Y][(int)location.X + 1].entrance == 0)
                            {
                                layout[(int)location.Y][(int)location.X].exit = 2;
                                location.X += (int)1;
                                layout[(int)location.Y][(int)location.X].entrance = 1;
                                reset = 0;
                            }
                            else
                                wall = true;
                        break;
                    case 3:
                    case 4:
                        Debug.WriteLine("moving left");
                        if (location.X - (int)1 >= 0)
                            if (layout[(int)location.Y][(int)location.X - 1].entrance == 0)
                            {
                                layout[(int)location.Y][(int)location.X].exit = 1;
                                location.X -= (int)1;
                                layout[(int)location.Y][(int)location.X].entrance = 2;
                                reset = 0;
                            }
                            else
                                wall = true;
                        break;
                    case 5:
                        Debug.WriteLine("going down");
                        if (location.Y + (int)1 < 4)
                            if (layout[(int)location.Y + 1][(int)location.X].entrance == 0)
                            {
                                layout[(int)location.Y][(int)location.X].exit = 3;
                                location.Y += (int)1;
                                layout[(int)location.Y][(int)location.X].entrance = 3;
                                reset = 0;
                            }
                        break;
                }
                Debug.WriteLine("Current coord: ({0},{1})", location.X, location.Y);
                if (wall)
                {
                    if (next != 5)
                        next = 5;

                }
                else
                    next = r.Next(1, 6);

                if(location == finish)
                {
                    break;
                }

            }

        }
        public void createRooms()
        {
            Random r = new Random();
            int rand;
            for (int i = 0; i < layout.Length;i++)
            {
                for(int j=0;j<layout[i].Length;j++)
                {
                    if(layout[i][j].entrance == 1 && layout[i][j].exit == 2)
                    {
                        var test = (from t in fromFile.tiles
                                    where t.entrance == "left" && t.exit == "right"
                                    select t.layout).ToList();
                        rand = r.Next(0, test.Count);
                        layout[i][j].layout.layout = test.ElementAt(rand);
                    }
                    if (layout[i][j].entrance == 2 && layout[i][j].exit == 1)
                    {
                        var test = (from t in fromFile.tiles
                                                      where t.entrance == "left" && t.exit == "right"
                                                      select t.layout).ToList();
                        rand = r.Next(0, test.Count);
                        layout[i][j].layout.layout = test.ElementAt(rand);
                    }
                    if (layout[i][j].entrance == 3 && layout[i][j].exit == 3)
                    {
                        var test = (from t in fromFile.tiles
                                                      where t.entrance == "up" && t.exit == "down"
                                                      select t.layout).ToList();
                        rand = r.Next(0, test.Count);
                        layout[i][j].layout.layout = test.ElementAt(rand);
                    }
                    if (layout[i][j].entrance == 3 && layout[i][j].exit == 1)
                    {
                        var test = (from t in fromFile.tiles
                                                      where t.entrance == "up" && t.exit == "left"
                                                      select t.layout).ToList();
                        rand = r.Next(0, test.Count);
                        layout[i][j].layout.layout = test.ElementAt(rand);
                    }
                    if (layout[i][j].entrance == 3 && layout[i][j].exit == 2)
                    {
                        var test = (from t in fromFile.tiles
                                                      where t.entrance == "up" && t.exit == "right"
                                                      select t.layout).ToList();
                        rand = r.Next(0, test.Count);
                        layout[i][j].layout.layout = test.ElementAt(rand);
                    }
                    if (layout[i][j].entrance == 2 && layout[i][j].exit == 3)
                    {
                        var test = (from t in fromFile.tiles
                                                      where t.entrance == "right" && t.exit == "down"
                                                      select t.layout).ToList();
                        rand = r.Next(0, test.Count);
                        layout[i][j].layout.layout = test.ElementAt(rand);
                    }
                    if (layout[i][j].entrance == 1 && layout[i][j].exit == 3)
                    {
                        var test = (from t in fromFile.tiles
                                                      where t.entrance == "left" && t.exit == "down"
                                                      select t.layout).ToList();
                        rand = r.Next(0, test.Count);
                        layout[i][j].layout.layout = test.ElementAt(rand);
                    }
                }
            }
            layout[0][start].layout.layout = new int[][]
            {
                new int[]{0,0,0,0,0},
                new int[]{0,0,0,0,0},
                new int[]{0,0,0,0,0},
                new int[]{0,0,3,0,0},
                new int[]{0,0,1,0,0},
            };
        }

        public void SetTiles(Assets content, ref Player testPlayer)
        {      
            for(int room = 0; room < layout.Length;room++)
            {
                for (int roomTile = 0; roomTile < layout[room].Length;roomTile++)
                {
                    jsonObj = layout[room][roomTile].layout;

                    setPosition = new Vector2(roomTile, room);
                    for (int i = 0; i < jsonObj.layout.Length;i++)
                    {
                        for(int j = 0; j < jsonObj.layout[i].Length;j++)
                        {
                            switch(jsonObj.layout[i][j])
                            {
                                case 1:
                                    floor.Add(new Wall(game, content.Wall, new Vector2((64 * j) + (setPosition.X * 320), (64 * i) + (setPosition.Y * 320)), content.DebugBox, new Vector2(64, 64), 1));
                                    break;
                                case 2:
                                    int x = Utility.NextRandom(1, 3);
                                    switch(x)
                                    {
                                        case 1:
                                        case 2:
                                        case 3:
                                            enemies.Add(new Enemy(game, content.Enemy, new Vector2((64 * j) + (setPosition.X * 320), (64 * i) + (setPosition.Y * 320)), content.DebugBox, new Vector2(64, 64), 1, floor, 1));
                                            break;
                                        case 4:
                                        case 5:
                                            collectables.Add(new Collectable(game, content.Collectable, new Vector2((64 * j) + (setPosition.X * 320), (64 * i) + (setPosition.Y * 320)),content.DebugBox,new Vector2(64,64),32,200));
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case 3:
                                    testPlayer = new Player(game, new Vector2((64 * j) + (setPosition.X * 320), (64 * i) + (setPosition.Y * 320)), content.DebugBox, new Vector2(46, 48), 4, content.Player,content.Font);
                                    break;
                            }

                        }
                    }
            }
                }
        }

    }


    public class Tile
    {
        public string entrance;
        public string exit;
        public int[][] layout;
    }

    public class Tiles
    {
        public List<Tile> tiles;

        public Tiles()
        {
            tiles = new List<Tile>();
        }
    }

    public class Room
    {
        public int entrance;
        public int exit;
        public Vector2 simple;
        public Tile layout;

    }
}
