using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlowerCollector1
{
    class Board
    {
        #region Fields

            //initial position
                Vector2 boardPosition;

            //Constants
                const int NUM_ROWS = 5;
                const int NUM_COLUMNS = 5;
                const int BORDER_SIZE = 5;

                const int INITIAL_MINE_NUMBER = 3;

            //tiles
                Tile[,] tiles = new Tile[NUM_ROWS, NUM_COLUMNS];

            //character
                Character collector;

            //landmines
                List<LandMine> landmines;
                int maxNumberOfMines;

            //flowers
                List<Flower> flowers;
                
            //explosion
                Explosion explosion;

            //click support
                bool clickStarted = false;
                bool mouseButtonReleased = true;

            //keyboard button support
                bool buttonPressStarted = false;
                bool keyboardButtonReleased = true;
                Keys previousKey;

            //random number generator
                Random rand = new Random();

            //sound support
                AudioEngine audioEngine;
                WaveBank waveBank;
                SoundBank soundBank;

            //timer to hide mines
                int elapsedTime = 0;
                const int LANDMINE_VISIBILITY = 2000;
            
            //Countdown timer
                Timer countdownTimer;
        #endregion

        #region Constructor

            /// <summary>
            /// Counstructor
            /// Creates a matrix with tiles
            /// </summary>
            /// <param name="contentManager"></param>
            public Board(ContentManager contentManager, Vector2 boardPosition)
            {
                this.boardPosition = boardPosition;
                landmines = new List<LandMine>();
                maxNumberOfMines = INITIAL_MINE_NUMBER;
                flowers = new List<Flower>();

                audioEngine = new AudioEngine(@"Content\sounds.xgs");
                waveBank = new WaveBank(audioEngine, @"Content\Wave Bank.xwb");
                soundBank = new SoundBank(audioEngine, @"Content\Sound Bank.xsb");
                
                //generate tile matrix
                GenerateTileMatrix(contentManager, NUM_ROWS, NUM_COLUMNS, boardPosition);

                //placing the collector
                int row = GenerateRandomRow();
                int col = GenerateRandomColumn();
                collector = new Character(contentManager, tiles[row, col].Center, row, col);
                tiles[row, col].Reserved = true;

                //placing mines and flowers on tiles
                AddLandMines(contentManager, 5);
                AddFlowers(contentManager, 3);

                //add explosion
                explosion = new Explosion(contentManager);

                //add countdown timer
                countdownTimer = new Timer(contentManager, "timerFont", new Vector2(400, 100), 0, 30);
            }

        #endregion

        #region Properties
        #endregion

        #region Public methods
            
            /// <summary>
            /// Update the board
            /// </summary>
            /// <param name="gameTime">the game time</param>
            /// <param name="mouse">mouse input</param>
            public void Update(GameTime gameTime, MouseState mouse, KeyboardState keyboard)
            {
                countdownTimer.Start(gameTime);

                for (int i = 0; i < NUM_ROWS; i++)
                {
                    for (int j = 0; j < NUM_COLUMNS; j++)
                    {
                        //check mouseclicks on tiles, if it's one step to up, down, left of right
                        if (tiles[i, j].DrawRectangle.Contains(mouse.X, mouse.Y) &&
                            ((collector.Row == i || collector.Column == j) 
                            && 
                            (Math.Abs(collector.Row - i) == 1 || Math.Abs(collector.Column - j) == 1)))
                        {
                            if (mouse.LeftButton == ButtonState.Pressed && mouseButtonReleased)
                            {
                                mouseButtonReleased = false;
                                clickStarted = true;
                            }
                            else if (mouse.LeftButton == ButtonState.Released)
                            {
                                mouseButtonReleased = true;
                                if (clickStarted)
                                {
                                    clickStarted = false;
                                    collector.Move(tiles[i, j].Center, i, j);
                                }
                            }
                        }
                    }
                }

                //Keyboard controls 
                //Must add code, to move only when the control buttons pressed only ONCE at the time

                if (IsButtonPressed(keyboard, Keys.Left))
                {
                    int currentRow = collector.Row;
                    int currentColumn = collector.Column - 1;
                    collector.Move(tiles[currentRow, currentColumn].Center, currentRow, currentColumn);
                }
                else if (IsButtonPressed(keyboard, Keys.Right)) 
                {
                    int currentRow = collector.Row;
                    int currentColumn = collector.Column + 1;
                    collector.Move(tiles[currentRow, currentColumn].Center, currentRow, currentColumn);
                }
                else if (IsButtonPressed(keyboard, Keys.Up))
                {
                    int currentRow = collector.Row - 1;
                    int currentColumn = collector.Column;
                    collector.Move(tiles[currentRow, currentColumn].Center, currentRow, currentColumn);
                }
                else if (IsButtonPressed(keyboard, Keys.Down))
                {
                    int currentRow = collector.Row + 1;
                    int currentColumn = collector.Column;
                    collector.Move(tiles[currentRow, currentColumn].Center, currentRow, currentColumn);
                }

                //check for collision between collector and landmines
                foreach (LandMine landmine in landmines) 
                {
                    if (landmine.DrawRectangle.Intersects(collector.DrawRectangle))
                    {
                        explosion.Play(landmine.Center);
                        landmine.Active = false;
                        soundBank.PlayCue("explosion");
                    }
                }

                //check for collision between collector and flowers
                foreach (Flower flower in flowers)
                {
                    if (flower.DrawRectangle.Intersects(collector.DrawRectangle))
                    {
                        flower.Active = false;
                        soundBank.PlayCue("success");
                    }
                }

                //remove inactive landmines
                for (int i = landmines.Count - 1; i >= 0; i--) 
                {
                    if (!landmines[i].Active)
                    {
                        landmines.RemoveAt(i);
                    }
                }

                //remove inactive flowers
                for (int i = flowers.Count - 1; i >= 0; i--)
                {
                    if (!flowers[i].Active)
                    {
                        flowers.RemoveAt(i);
                    }
                }

                //update explosion
                explosion.Update(gameTime);
            }

            /// <summary>
            /// Draws the tiles and the character
            /// </summary>
            /// <param name="spriteBatch"></param>
            public void Draw(SpriteBatch spriteBatch)
            {
                //draw tiles
                for (int i = 0; i < NUM_ROWS; i++)
                {
                    for (int j = 0; j < NUM_COLUMNS; j++)
                    {
                        tiles[i, j].Draw(spriteBatch);
                    }
                }

                //draw the collector Character
                collector.Draw(spriteBatch);

                //draw landmines
                foreach (LandMine landmine in landmines)
                {
                    landmine.Draw(spriteBatch);
                }

                //draw flowers
                foreach (Flower flower in flowers) 
                {
                    flower.Draw(spriteBatch);
                }

                //draw explosion
                explosion.Draw(spriteBatch);

                //draw countdown timer
                countdownTimer.Draw(spriteBatch);
            }

            /// <summary>
            /// Generate a rows x cols Tile Matrix
            /// </summary>
            /// <param name="contentManager"></param>
            /// <param name="rows"></param>
            /// <param name="columns"></param>
            public void GenerateTileMatrix(ContentManager contentManager, int rows, int columns, Vector2 boardPosition)
            {
                //generate tiles
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        tiles[i, j] = new Tile(contentManager,
                                                (int)boardPosition.X + (BORDER_SIZE * (j + 1)) + j * 64,
                                                (int)boardPosition.Y + (BORDER_SIZE * (i + 1)) + i * 64);
                    }
                }
            }

            /// <summary>
            /// Add landmines on the tile matrix
            /// </summary>
            /// <param name="contentManager">content manager</param>
            /// <param name="numberOfLandMines">nummber of landmines</param>
            public void AddLandMines(ContentManager contentManager, int numberOfLandMines)
            {
                int mineCounter = 0;
                int row;
                int col;
                while (mineCounter < numberOfLandMines)
                {
                    row = GenerateRandomRow();
                    col = GenerateRandomColumn();
                    if (!tiles[row, col].Reserved)
                    {
                        landmines.Add(new LandMine(contentManager, tiles[row, col].Center));
                        tiles[row, col].Reserved = true;
                        mineCounter++;
                    }
                }
            }

            /// <summary>
            /// Add flowers on the tile matrix
            /// </summary>
            /// <param name="contentManager">content manager</param>
            /// <param name="numberOfFlowers">nummber of flowers</param>
            public void AddFlowers(ContentManager contentManager, int numberOfFlowers)
            {
                int flowerCounter = 0;
                int row;
                int col;
                while (flowerCounter < numberOfFlowers)
                {
                    row = GenerateRandomRow();
                    col = GenerateRandomColumn();
                    if (!tiles[row, col].Reserved)
                    {
                        flowers.Add(new Flower(contentManager, tiles[row, col].Center, GenerateRandomFlowerName()));
                        tiles[row, col].Reserved = true;
                        flowerCounter++;
                    }
                }
            }

            public bool HideLandMines(GameTime gameTime) 
            {
                if (elapsedTime < LANDMINE_VISIBILITY)
                {
                    elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                    return false;
                }
                else 
                {
                    foreach (LandMine landmine in landmines) 
                    {
                        landmine.Hidden = true;
                    }
                    return true;
                }
            }
        #endregion

        #region Private methods
            
            /// <summary>
            /// Return a random number between 0 and the maximum number of rows
            /// </summary>
            /// <returns></returns>
            private int GenerateRandomRow() 
            {
                int randomRow = rand.Next(NUM_ROWS);
                return randomRow;
            }
            
            /// <summary>
            /// Return a random number between 0 and the maximum number of columns
            /// </summary>
            /// <returns></returns>
            private int GenerateRandomColumn()
            {
                int randomColumn = rand.Next(NUM_COLUMNS);
                return randomColumn;
            }
            
            /// <summary>
            /// Return a flower file name
            /// </summary>
            /// <returns></returns>
            private String GenerateRandomFlowerName()
            {
                int random = rand.Next(0,3);
                if (random == 0) 
                {
                    return "flower" + random;
                }
                else if (random == 1) 
                {
                    return "flower" + random;
                }
                else if (random == 2)
                {
                    return "flower" + random;
                }
                else
                {
                    return null;
                }
            }
            
            /// <summary>
            /// Return true if the specifed key pressed and released
            /// </summary>
            /// <param name="keyboard">The keyboard</param>
            /// <param name="key">Key on keyboard</param>
            /// <returns></returns>
            public bool IsButtonPressed(KeyboardState keyboard, Keys key) 
            {
                if (keyboard.IsKeyDown(key) && keyboardButtonReleased)
                {
                    buttonPressStarted = true;
                    if (buttonPressStarted)
                    {
                        keyboardButtonReleased = false;
                        buttonPressStarted = false;
                        previousKey = key;
                        return true;
                    }
                    return false;
                }
                else if (keyboard.IsKeyUp(key) && key == previousKey)
                {
                    keyboardButtonReleased = true;
                    return false;
                }
                else
                {
                    return false;
                }
            }

        #endregion
    }
}
