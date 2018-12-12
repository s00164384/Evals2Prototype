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
    class Scene2 :Scene
    {
        SpriteBatch spriteBatch;
        Texture2D testSprite;
        SpriteFont _sf;
        Rectangle bounds;

        public Scene2(Game g) :base(g)
        {
            _name = "Second Level";
        }

        protected override void LoadContent()
        {
            bounds = new Rectangle(0, 0, 5000, 5000);
            bgm = game.Content.Load<Song>("Sounds/bg");
            MediaPlayer.Play(bgm);

            // Create a new SpriteBatch, which can be used to draw textures.
            _sf = game.Content.Load<SpriteFont>("Fonts/Score");


            testSprite = game.Content.Load<Texture2D>("Backgrounds/xp");
            List<Wall> floor = new List<Wall>
            {
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 700) , game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(1280, 32),1),
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 550) , game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(500, 32),1),
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(780, 550), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(1000, 32),1),
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 400), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(300, 32),1),
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(980,400), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(300, 32),1),
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(390, 350), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(500,32),1),
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 200), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(575,32),1),
            new Wall(game, game.Content.Load<Texture2D>("Sprites/floor"), new Vector2(705, 200), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(575,32),1)
            };
            foreach(Wall w in floor)
            {
                Components.Add(w);
            }
            List<Enemy> enemies = new List<Enemy>
            {
            new Enemy(game, game.Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(0, 136), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), 1, floor, 1),
            new Enemy(game, game.Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(1000, 136), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), -1, floor, 1),
            new Enemy(game, game.Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(450, 286), game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), -1, floor, 1)
            };
            foreach (Enemy e in enemies)
            {
                Components.Add(e);
            }
            SoundEffect oof = game.Content.Load<SoundEffect>("Sounds/oof");
            
            List<Door> doors = new List<Door>
            {
                new Door(game, game.Content.Load<Texture2D>("Sprites/floor"),new Vector2(200,150),new Vector2(1200,150),game.Content.Load<Texture2D>("Sprites/hitbox"),new Vector2(32,64),1)
            };
            foreach (Door d in doors)
            {
                Components.Add(d);
            }
            CameraGuide _tager = new CameraGuide(game, game.Content.Load<Texture2D>("Sprites/tager"), Vector2.Zero, game.Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), 0,this);
            Components.Add(_tager);
            Player testPlayer = new Player(game, game.Content.Load<Texture2D>("Sprites/evals"), new Vector2(608, 500), game.Content.Load<Texture2D>("Sprites/hitbox"), floor, new Vector2(64, 64), 4, new Texture2D[] { game.Content.Load<Texture2D>("Sprites/evals"), game.Content.Load<Texture2D>("Sprites/evalsRight"), game.Content.Load<Texture2D>("Sprites/evalsJump"), game.Content.Load<Texture2D>("Sprites/evalsFall") }, enemies, oof, _sf, _tager, doors);
            Components.Add(testPlayer);
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
