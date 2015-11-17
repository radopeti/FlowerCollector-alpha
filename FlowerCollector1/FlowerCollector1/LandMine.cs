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
    class LandMine
    {
        #region Fields

        //texture
        Texture2D sprite;
        Texture2D emptySprite;
        Rectangle drawRectangle;
        //active flag
        bool active;
        //hidden flag
        bool hidden;
        //center position of the landmine
        Vector2 center;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor, creates a landmine at the given position
        /// </summary>
        /// <param name="contentManager">content manager</param>
        /// <param name="centerPosition">center position, the place of the landmine</param>
        public LandMine(ContentManager contentManager, Vector2 centerPosition)
        {
            active = true;
            center = centerPosition;
            sprite = contentManager.Load<Texture2D>("landmine");
            emptySprite = contentManager.Load<Texture2D>("empty_32x32");
            drawRectangle = new Rectangle((int)centerPosition.X - sprite.Width / 2,
                                          (int)centerPosition.Y - sprite.Height / 2,
                                           sprite.Width, sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Return if the landmine active or not
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Return the draw rectangle of the landmine
        /// </summary>
        public Rectangle DrawRectangle 
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Return the center of the landmine
        /// </summary>
        public Vector2 Center 
        {
            get { return center; }
        }

        /// <summary>
        /// Hidden property
        /// </summary>
        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        #endregion



        #region Public methods

        /// <summary>
        /// Draw the landmine if it's active
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) 
        {
            if (active)
            {
                if (hidden)
                {
                    spriteBatch.Draw(emptySprite, drawRectangle, Color.White);
                }
                else 
                {
                    spriteBatch.Draw(sprite, drawRectangle, Color.White);
                }
            }
        }

        #endregion

        #region Private methods
        #endregion
    }
}
