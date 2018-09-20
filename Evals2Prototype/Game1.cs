using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Evals2Prototype.Objects;
using System.Collections.Generic;

namespace Evals2Prototype
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D testSprite;
        SpriteFont _sf;
        Song backingTrack;
        Rectangle bounds;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            bounds = new Rectangle(0, 0, 5000, 5000);
            backingTrack = Content.Load<Song>("Sounds/bg");
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(backingTrack);
            new Camera(this, Vector2.Zero, new Vector2(5000, 5000));
            // Create a new SpriteBatch, which can be used to draw textures.
            _sf = Content.Load<SpriteFont>("Fonts/Score");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            testSprite = Content.Load<Texture2D>("Backgrounds/xp");
            List<Wall> floor = new List<Wall>
            {
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 700) , Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(1280, 32),1),
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 550) , Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(500, 32),1),
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(780, 550), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(500, 32),1),
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 400), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(300, 32),1),
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(980,400), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(300, 32),1),
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(390, 350), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(500,32),1),
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(0, 200), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(575,32),1),
            new Wall(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(705, 200), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(575,32),1)
            };
            List<Enemy> enemies = new List<Enemy>
            {
            new Enemy(this, Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(0, 136), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), 1, floor, 1),
            new Enemy(this, Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(1000, 136), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), -1, floor, 1),
            new Enemy(this, Content.Load<Texture2D>("Sprites/Enemy"), new Vector2(450, 286), Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), -1, floor, 1)
            };
            SoundEffect oof = Content.Load<SoundEffect>("Sounds/oof");
            List<Door> doors = new List<Door>
            {
                new Door(this, Content.Load<Texture2D>("Sprites/floor"),new Vector2(200,150),new Vector2(1200,150),Content.Load<Texture2D>("Sprites/hitbox"),new Vector2(32,64),1)
            };
            CameraGuide _tager = new CameraGuide(this, Content.Load<Texture2D>("Sprites/tager"), Vector2.Zero, Content.Load<Texture2D>("Sprites/hitbox"), new Vector2(64, 64), 0);
            Player testPlayer = new Player(this, Content.Load<Texture2D>("Sprites/evals"), new Vector2(608, 500), Content.Load<Texture2D>("Sprites/hitbox"), floor, new Vector2(64, 64), 4, new Texture2D[] { Content.Load<Texture2D>("Sprites/evals"), Content.Load<Texture2D>("Sprites/evalsRight"), Content.Load<Texture2D>("Sprites/evalsJump"), Content.Load<Texture2D>("Sprites/evalsFall") },enemies,oof,_sf,_tager,doors);
          

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                                Camera.CurrentCameraTranslation);
            spriteBatch.Draw(testSprite, bounds, Color.White);

            spriteBatch.End();
            // TODO: Add your drawing code here
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
