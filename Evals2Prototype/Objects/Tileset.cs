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
        Vector2 setPosition;
        Game game;
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
     

            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    layout[i][j] = new Room { entrance = 0, exit = 0 };
                }
            }
            int next = 0;



            int start = r.Next(0, 4);
            int end = r.Next(0, 4);
            bool wall;
            Vector2 location = new Vector2(start, 0);
            Vector2 finish = new Vector2(end, 3);
            layout[(int)location.Y][(int)location.X] = new Room { entrance = 0, exit = 0 };
            layout[(int)finish.Y][(int)finish.X] = new Room { entrance = 0, exit = 0 };
            next = r.Next(1, 6);
            while (!(location == finish) && location.Y < 4)
            {
                Vector2 tempLocation = location;
                wall = false;


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

        public void SetTiles(Assets content, ref Player testPlayer)
        {      


            for (int i = 0; i < jsonObj.layout.Length;i++)
            {
                for(int j = 0; j < jsonObj.layout[i].Length;j++)
                {
                    switch(jsonObj.layout[i][j])
                    {
                        case 1:
                            floor.Add(new Wall(game, content.Wall, new Vector2((64 * j) + (setPosition.X * 768), (64 * i) + (setPosition.Y * 512)), content.DebugBox, new Vector2(64, 64), 1));
                            break;
                        case 2:
                            int x = Utility.NextRandom(1, 6);
                            switch(x)
                            {
                                case 1:
                                case 2:
                                case 3:
                                    enemies.Add(new Enemy(game, content.Enemy, new Vector2((64 * j) + (setPosition.X * 768), (64 * i) + (setPosition.Y * 512)), content.DebugBox, new Vector2(64, 64), 1, floor, 1));
                                    break;
                                case 4:
                                case 5:
                                    collectables.Add(new Collectable(game, content.Collectable, new Vector2((64 * j) + (setPosition.X * 768), (64 * i) + (setPosition.Y * 512)),content.DebugBox,new Vector2(64,64),32,200));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 3:
                            testPlayer = new Player(game, new Vector2((64 * j) + (setPosition.X * 768), (64 * i) + (setPosition.Y * 512)), content.DebugBox, new Vector2(46, 48), 4, content.Player,content.Font);
                            break;
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

    }
}
