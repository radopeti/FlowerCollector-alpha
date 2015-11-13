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
    class Flower
    {
        #region Fields

        //texture
        Texture2D sprite;
        Rectangle drawRectangle;
        //active flag
        bool active;
        //random number generator
        Random rand = new Random();
        //center position of the flower
        Vector2 center;

        #endregion

        #region Constructor

        public Flower(ContentManager contentManager, Vector2 centerPosition, String spriteName) 
        {
            active = true;
            center = centerPosition;
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle((int)centerPosition.X - sprite.Width / 2,
                                          (int)centerPosition.Y - sprite.Height / 2,
                                          sprite.Width, sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Return if the flower active or not
        /// </summary>
        public bool Active 
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Return the draw rectangle of the flower
        /// </summary>
        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Return the center of the flower
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
        }

        #endregion

        #region Public methods

        public void Draw(SpriteBatch spriteBatch) 
        {
            if (active) 
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }
        }
        #endregion

        #region Private methods
        #endregion
    }
}
