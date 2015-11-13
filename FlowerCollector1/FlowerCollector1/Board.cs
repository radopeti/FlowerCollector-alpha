﻿using System;
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
                bool buttonReleased = true;

            //random number generator
                Random rand = new Random();

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

            for (int i = 0; i < NUM_ROWS; i++)
            {
                for (int j = 0; j < NUM_COLUMNS; j++)
                {
                    tiles[i, j] = new Tile(contentManager, 
                                          (int)boardPosition.X + (BORDER_SIZE * (j + 1)) + j * 64,
                                          (int)boardPosition.Y + (BORDER_SIZE * (i + 1)) + i * 64);
                }
            }

            //placing the collector
            int row = GenerateRandomRow();
            int col = GenerateRandomColumn();
            collector = new Character(contentManager, tiles[row, col].Center, row, col);
            tiles[row, col].Reserved = true;

            //placing mines and flowers on tiles
            int mineCounter = 0;
            int flowerCounter = 0;
            while (mineCounter < 5 || flowerCounter < 3)
            {
                row = GenerateRandomRow();
                col = GenerateRandomColumn();
                if (!tiles[row, col].Reserved && (mineCounter + flowerCounter) % 2 == 0) 
                {
                    landmines.Add(new LandMine(contentManager, tiles[row, col].Center));
                    tiles[row, col].Reserved = true;
                    mineCounter++;
                }
                else if (!tiles[row, col].Reserved && (mineCounter + flowerCounter) % 2 == 1 &&
                        flowerCounter <= 3)
                {
                    flowers.Add(new Flower(contentManager, tiles[row, col].Center, GenerateRandomFlowerName()));
                    tiles[row, col].Reserved = true;
                    flowerCounter++;
                }
            }
            
            explosion = new Explosion(contentManager);
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
        public void Update(GameTime gameTime, MouseState mouse) 
        {
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
                        if (mouse.LeftButton == ButtonState.Pressed && buttonReleased)
                        {
                            buttonReleased = false;
                            clickStarted = true;
                        }
                        else if (mouse.LeftButton == ButtonState.Released)
                        {
                            buttonReleased = true;
                            if (clickStarted)
                            {
                                clickStarted = false;
                                collector.Move(tiles[i, j].Center, i, j);
                            }
                        }
                    }
                }
            }

            //remove inactive landmines

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
        }

        #endregion

        #region Private methods

        private int GenerateRandomRow() 
        {
            int randomRow = rand.Next(NUM_ROWS);
            return randomRow;
        }

        private int GenerateRandomColumn()
        {
            int randomColumn = rand.Next(NUM_COLUMNS);
            return randomColumn;
        }

        private String GenerateRandomFlowerName()
        {
            int random = rand.Next(0, 3);
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
                return "flower" + random;
            }
        }
        #endregion
    }
}
