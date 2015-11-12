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
    class Tile
    {
        #region Fields

        //tile texture
        Texture2D tileSprite;
        Rectangle drawRectangle;

        //center position of the tile
        Vector2 center;

        //length of the tile
        int sideLenght;

        #endregion
        

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">content manager</param>
        /// <param name="positionX">x position of the draw rectangle</param>
        /// <param name="positionY">y position of the draw rectangle</param>
        public Tile(ContentManager contentManager, int positionX, int positionY)
        {
            tileSprite = contentManager.Load<Texture2D>("tile");
            drawRectangle = new Rectangle(positionX, positionY, tileSprite.Width, tileSprite.Height);
            this.sideLenght = tileSprite.Width;
            center = new Vector2(positionX + sideLenght / 2, positionY + sideLenght / 2);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Return the draw rectangle
        /// </summary>
        public Rectangle DrawRectangle 
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Return the center position of the tile
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
        }

        #endregion
        
        #region Public methods

        public void Update(GameTime gameTime, MouseState mouse)
        {
            
        }
        /// <summary>
        /// Draws the tile
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileSprite, drawRectangle, Color.White);
        }

        #endregion
       
        #region Private methods
        #endregion
    }
}
