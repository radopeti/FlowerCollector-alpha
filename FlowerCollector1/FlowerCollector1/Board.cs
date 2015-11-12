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

            //tiles
                Tile[,] tiles = new Tile[NUM_ROWS, NUM_COLUMNS];

            //character
                Character collector;

            //landmines
                List<LandMine> landmines;

            //flowers
                List<Flower> flowers;

            //explosion
                Explosion explosion;

            //click support
                bool clickStarted = false;
                bool buttonReleased = true;

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

            collector = new Character(contentManager, tiles[0, 0].Center, 0, 0);
            landmines.Add(new LandMine(contentManager, tiles[0, 1].Center));
            flowers.Add(new Flower(contentManager, tiles[0, 2].Center));
            flowers.Add(new Flower(contentManager, tiles[0, 3].Center));
            flowers.Add(new Flower(contentManager, tiles[0, 4].Center));
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
        #endregion
    }
}
