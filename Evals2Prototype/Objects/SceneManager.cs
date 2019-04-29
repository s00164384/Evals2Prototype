using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Evals2Prototype.Scenes;
using System.IO;
using Newtonsoft.Json;

namespace Evals2Prototype.Objects
{
    class SceneManager : DrawableGameComponent
    {
        Scene activeScene;
        List<Scene> sceneList = new List<Scene>();
        Camera c;
        Menu menu;
        Game game;
        string json;
        public Scores jsonScores;
        public Assets content;
        bool Paused,victory,newScore;

        public SceneManager(Game g) :base(g)
        {
            game = g;
            MediaPlayer.IsRepeating = true;
            g.Components.Add(this);
            menu = new Menu(g);
            activeScene = menu;
            activeScene.active = true;
            sceneList = new List<Scene> { new Scene2(g), new Scene2(g) };
            c = new Camera(g, Vector2.Zero, new Vector2(3392, 2368), activeScene);
            LoadLeaderboards();
            jsonScores.scoresList.Sort();

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
                Font = game.Content.Load<SpriteFont>("Fonts/Score"),
                Collectable = game.Content.Load<Texture2D>("Sprites/collectable")
            };





            foreach(Scene s in sceneList)
            {
                s.SetupRoom(content);
            }

            base.LoadContent();
        }

        

        public override void Update(GameTime gameTime)
        {
            if(!Paused && !victory)
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
            if(activeScene.testPlayer != null)
            if(activeScene.testPlayer.enemies.Count <= 0)
            {
                victory = true;
                activeScene.active = false;
                    if (!newScore)
                    {
                        jsonScores.scoresList.Add(new Score { time = DateTime.Now.ToLongTimeString(), value = activeScene.testPlayer.score });
                        newScore = true;
                    }
                }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && activeScene._name != "Menu" && !Paused)
            {
                activeScene.active = false;
                activeScene = menu;
                menu.selectionMade = false;

            }
            if (InputEngine.IsKeyPressed(Keys.Enter) && activeScene._name != "Menu")
            {
                if (!victory)
                {
                    if (!Paused)
                    {
                        activeScene.active = false;
                        Paused = true;
                    }
                    else
                        Paused = false;
                }
                else
                {
                    activeScene.gotoMenu = true;
                    victory = false;
                }

            }

            c.scene = activeScene;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch Sb = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            if (Sb == null) return;
            Sb.Begin();
            Sb.Draw(content.Backgrounds[1],Game.GraphicsDevice.Viewport.Bounds, Color.White);
            if(victory)
            {
                Vector2 size = content.Font.MeasureString("You slayed 'em all!");
                Sb.DrawString(content.Font, "You slayed 'em all!", new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - size.X/2, 0+Game.GraphicsDevice.Viewport.Height/6), Color.White);
                size = content.Font.MeasureString("Press Enter To Return to the menu");
                Sb.DrawString(content.Font, "Press Enter To Return to the menu", new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - size.X/2, size.Y + Game.GraphicsDevice.Viewport.Height / 6), Color.White);
                Vector2 size2 = content.Font.MeasureString("Top Scores:");
                Sb.DrawString(content.Font, "Top Scores:", new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - size2.X / 2, size2.Y*2 + Game.GraphicsDevice.Viewport.Height / 6), Color.White);

                if(jsonScores.scoresList.Count < 5)
                for (int i = 0; i < jsonScores.scoresList.Count; i++)
                {
                    size = content.Font.MeasureString(jsonScores.scoresList.ElementAt(i).time.ToString());
                    Sb.DrawString(content.Font, jsonScores.scoresList.ElementAt(i).time.ToString(), new Vector2((Game.GraphicsDevice.Viewport.Width / 8) * 5, Game.GraphicsDevice.Viewport.Height / 2 + 64 * i), Color.White);
                    size2 = content.Font.MeasureString(jsonScores.scoresList.ElementAt(i).value.ToString());
                    Sb.DrawString(content.Font, jsonScores.scoresList.ElementAt(i).value.ToString(), new Vector2((Game.GraphicsDevice.Viewport.Width / 8) * 3, Game.GraphicsDevice.Viewport.Height / 2 + 64 * i), Color.White);
                }
                else
                for (int i = 0; i < 5;i++)
                {
                    size = content.Font.MeasureString(jsonScores.scoresList.ElementAt(i).time.ToString());
                    Sb.DrawString(content.Font, jsonScores.scoresList.ElementAt(i).time.ToString(), new Vector2((Game.GraphicsDevice.Viewport.Width / 8) * 5, Game.GraphicsDevice.Viewport.Height / 2 + 64 * i), Color.White);
                    size2 = content.Font.MeasureString(jsonScores.scoresList.ElementAt(i).value.ToString());
                    Sb.DrawString(content.Font, jsonScores.scoresList.ElementAt(i).value.ToString(), new Vector2((Game.GraphicsDevice.Viewport.Width / 8) * 3, Game.GraphicsDevice.Viewport.Height / 2 + 64 * i), Color.White);
                }
            }
            Sb.End();
            base.Draw(gameTime);
        }

        public void LoadLeaderboards()
        {
            if (File.Exists("leaderboards.json"))
            {
                {
                    using (StreamReader sr = new StreamReader("leaderboards.json"))
                    {
                        json = sr.ReadToEnd();
                        jsonScores = JsonConvert.DeserializeObject<Scores>(json);

                    }
                }
            }
        }
    }

    public class Assets
    {
        public Texture2D[] Player;
        public Texture2D Enemy;
        public Texture2D Wall;
        public Texture2D Collectable;
        public Texture2D DebugBox;
        public Texture2D[] Backgrounds;
        public Song[] Songs;
        public SpriteFont Font;
    }

    public class Scores
    {
        public List<Score> scoresList;

        public Scores()
        {
            scoresList = new List<Score>();

        }
    }
    public class Score : IComparable
    {
        public int value;
        public string time;

        public int CompareTo(object sc)
        {
            if (sc == null) return 1;

            Score temp = sc as Score;
            if (temp != null)
                return this.value.CompareTo(temp.value);
            else return 1;
        }
    }



}
