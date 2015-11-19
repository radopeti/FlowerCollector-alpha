using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlowerCollector1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Board gameBoard;
        MouseState mouse;
        KeyboardState keyboard;

        public static GameState gameState = GameState.Menu;

        //menubuttons
        Button playButton;
        Button quitButton;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            // TODO: use this.Content to load your game content here
            //board
            gameBoard = new Board(Content, new Vector2(0,0));
            //menubuttons
            playButton = new Button(Content, "play_button", GameState.Play, 20, 20);
            quitButton = new Button(Content, "quit_button", GameState.Quit, 20, 90);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
            
            switch (gameState) 
            {
                case GameState.Menu:
                    //menubuttons
                    playButton.Update(gameTime, mouse);
                    quitButton.Update(gameTime, mouse);
                break;

                case GameState.Play:
                    //board
                    if (gameBoard.HideLandMines(gameTime)) 
                    {
                        gameBoard.Update(gameTime, mouse, keyboard);
                    }
                break;

                case GameState.Quit:
                    this.Exit();
                break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            
            if (gameState == GameState.Menu) 
            {
                playButton.Draw(spriteBatch);
                quitButton.Draw(spriteBatch);
            }
            else if (gameState == GameState.Play)
            {
                gameBoard.Draw(spriteBatch);
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public static void ChangeGameState(GameState newState) 
        {
            gameState = newState;
        }
    }
}
