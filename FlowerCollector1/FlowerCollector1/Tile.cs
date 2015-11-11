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
        #endregion
        //tile texture
        Texture2D tileSprite;
        Rectangle drawRectangle;

        #region Constructor
        #endregion
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
        }
        #region Properties
        #endregion

        #region Public methods
        #endregion
        public void Update(GameTime gameTime, MouseState mouse) 
        {

        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(tileSprite, drawRectangle, Color.White);
        }
        #region Private methods
        #endregion
    }
}
