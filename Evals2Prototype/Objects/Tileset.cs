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

namespace Evals2Prototype.Objects
{
    class Tileset : DrawableGameComponent
    {
        public enum TileType { wall,ground}
        public int[][] Tiles;
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
}
