﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        AnimatedSprite testSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            
            graphics.ApplyChanges();
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            AnimatedSprite testFloor = new AnimatedSprite(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(-200, 250), "floor",Content.Load<Texture2D>("Sprites/hitbox"));
            AnimatedSprite testFloor2 = new AnimatedSprite(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(300, 250), "floor", Content.Load<Texture2D>("Sprites/hitbox"));
            AnimatedSprite testFloor3 = new AnimatedSprite(this, Content.Load<Texture2D>("Sprites/floor"), new Vector2(500, 100), "floor", Content.Load<Texture2D>("Sprites/hitbox"));
            List<AnimatedSprite> floor = new List<AnimatedSprite>();
            floor.Add(testFloor);
            floor.Add(testFloor2);
            floor.Add(testFloor3);
            testSprite = new Player(this,Content.Load<Texture2D>("Sprites/tager"), new Vector2(0, 0), Content.Load<Texture2D>("Sprites/hitbox"),floor);
            

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
            
            //spriteBatch.Begin();
            //spriteBatch.Draw(testSprite.Image, testSprite.Position, Color.White);
            //spriteBatch.End();
            // TODO: Add your drawing code here
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
