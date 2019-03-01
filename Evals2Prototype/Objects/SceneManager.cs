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
using Evals2Prototype.Scenes;

namespace Evals2Prototype.Objects
{
    class SceneManager : DrawableGameComponent
    {
        Scene activeScene;
        List<Scene> sceneList = new List<Scene>();
        Camera c;
        Menu menu;
        Game game;
        public Assets content;

        public SceneManager(Game g) :base(g)
        {
            game = g;
            MediaPlayer.IsRepeating = true;
            g.Components.Add(this);
            menu = new Menu(g);
            activeScene = menu;
            activeScene.active = true;
            sceneList = new List<Scene> { new Scene2(g), new Scene2(g) };
            c = new Camera(g, Vector2.Zero, new Vector2(5000, 5000), activeScene);
            
        }

        protected override void LoadContent()
        {
            content = new Assets
            {
                Player = new Texture2D[] { game.Content.Load<Texture2D>("Sprites/evals"), game.Content.Load<Texture2D>("Sprites/evalsRight"), game.Content.Load<Texture2D>("Sprites/evalsJump"), game.Content.Load<Texture2D>("Sprites/evalsFall"), game.Content.Load<Texture2D>("Sprites/dagger"),game.Content.Load<Texture2D>("Sprites/EvalsAtt"), game.Content.Load<Texture2D>("Sprites/projectile") },
                Enemy = game.Content.Load<Texture2D>("Sprites/Enemy"),
                Wall = game.Content.Load<Texture2D>("Sprites/Scene2/Wall"),
                DebugBox = game.Content.Load<Texture2D>("Sprites/hitbox"),
                Backgrounds = new Texture2D[] { game.Content.Load<Texture2D>("Backgrounds/xp"), game.Content.Load<Texture2D>("Backgrounds/bg2") },
                Songs = new Song[] { game.Content.Load<Song>("Sounds/cloud"), game.Content.Load<Song>("Sounds/desert") },
                Font = game.Content.Load<SpriteFont>("Fonts/Score")
            };

            foreach(Scene s in sceneList)
            {
                s.SetupRoom(content);
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!activeScene.active)
            {
                if(activeScene.gotoMenu)
                {
                    activeScene = menu;
                    menu.selectionMade = false;
                    sceneList.Remove(activeScene);
                }
                MediaPlayer.Stop();
                activeScene.active = true;
                MediaPlayer.Play(activeScene.bgm);
            }

            if(activeScene._name == "Menu")
            {
                if(menu.selectionMade)
                {
                    switch (menu.selection)
                    {
                        case 0:
                            activeScene.active = false;
                                Scene2 s = new Scene2(game);
                                s.SetupRoom(content);
                                activeScene = s;
                            break;
                        case 1:
                            Game.Exit();
                            break;
                        default:
                            break;
                    }
                }
            }
            //activeScene.Update(gameTime);


            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && activeScene._name != "Menu")
            {
                activeScene.active = false;
                activeScene = menu;
                menu.selectionMade = false;


            }
            //if (Keyboard.GetState().IsKeyDown(Keys.R) && activeScene._name != "First Level")
            //{

            //}
            c.scene = activeScene;

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            activeScene.Draw(gameTime);
        }

    }

    public class Assets
    {
        public Texture2D[] Player;
        public Texture2D Enemy;
        public Texture2D Wall;
        public Texture2D DebugBox;
        public Texture2D[] Backgrounds;
        public Song[] Songs;
        public SpriteFont Font;
    }


}
