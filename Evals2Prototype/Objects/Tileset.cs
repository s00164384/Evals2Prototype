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
        Vector2 setPosition;
        Game game;
        public List<Wall> floor = new List<Wall>();
        public Tileset(Game g,Vector2 pos): base(g)
        {
            game = g;
           setPosition = pos;
        }

        public void SetTiles(Texture2D wallTX)
        {      


            for (int i = 0; i < Tiles.Length;i++)
            {
                for(int j = 0; j < Tiles[i].Length;j++)
                {
                    if(Tiles[i][j] == 1)
                    {
                        floor.Add(new Wall(game, wallTX, new Vector2((64 * j) + (setPosition.X * 2048), (64 * i)+(setPosition.Y * 1024)), wallTX, new Vector2(64, 64), 1));
                    }
                }
            }
        }
    }
}
