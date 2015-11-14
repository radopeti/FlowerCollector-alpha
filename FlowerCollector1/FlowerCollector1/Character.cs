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
    class Character
    {
        #region Fields
        //texture
        Texture2D sprite;
        Rectangle drawRectangle;
        //character positions on the matrix
        int row;
        int column;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">contentManager</param>
        /// <param name="centerPosition">center position of the character</param>
        /// <param name="row">number of the row where the character is</param>
        /// <param name="column">number of the column where the character is</param>
        public Character(ContentManager contentManager, Vector2 centerPosition, int row, int column)
        {
            this.row = row;
            this.column = column;
            sprite = contentManager.Load<Texture2D>("character");
            drawRectangle = new Rectangle((int)centerPosition.X - sprite.Width / 2,
                                          (int)centerPosition.Y - sprite.Height / 2,
                                          sprite.Width, sprite.Height);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Return the number of the row where the character is
        /// </summary>
        public int Row 
        {
            get { return row; }
            set { row = value; }
        }

        /// <summary>
        /// Return the number of the column where the character is
        /// </summary>
        public int Column 
        {
            get { return column; }
            set { column = value; }
        }

        /// <summary>
        /// Return the draw rectangle of the character
        /// </summary>
        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Move the character on the given position of the object and sets the number of row and column
        /// </summary>
        /// <param name="centerPosition">center position of the object</param>
        /// <param name="row">number of the row</param>
        /// <param name="column">number of the column</param>
        public void Move(Vector2 centerPosition, int row, int column)
        {
            drawRectangle.X = (int)centerPosition.X - sprite.Width / 2;
            drawRectangle.Y = (int)centerPosition.Y - sprite.Height / 2;
            this.row = row;
            this.column = column;
        }

        public void Update() 
        {

        }

        /// <summary>
        /// Draws the character where it has centered
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion

        #region Private methods
        #endregion
    }
}
