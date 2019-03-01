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
        public Tileset(Game g,Vector2 pos): base(g)
        {
            game = g;
           setPosition = pos;
            jsonObj = new Tile();
        }

        public void SetTiles(Texture2D wallTX,Texture2D enemyTX,Texture2D debugBox)
        {      


            for (int i = 0; i < jsonObj.layout.Length;i++)
            {
                for(int j = 0; j < jsonObj.layout[i].Length;j++)
                {
                    switch(jsonObj.layout[i][j])
                    {
                        case 1:
                            floor.Add(new Wall(game, wallTX, new Vector2((64 * j) + (setPosition.X * 1536), (64 * i) + (setPosition.Y * 960)), debugBox, new Vector2(64, 64), 1));
                            break;
                        case 2:
                            enemies.Add(new Enemy(game,enemyTX, new Vector2((64 * j) + (setPosition.X * 1536), (64 * i) + (setPosition.Y * 960)), debugBox, new Vector2(64, 64),1,floor, 1));
                            break;
                    }

            }
            }
        }

    }

    public class Tile
    {
        public string type;
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
