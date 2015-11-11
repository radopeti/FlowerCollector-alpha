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
        #endregion

        #region Constructor
        public Character(ContentManager contentManager, Vector2 centerPosition)
        {
            sprite = contentManager.Load<Texture2D>("character");
            drawRectangle = new Rectangle((int)centerPosition.X - sprite.Width / 2,
                                          (int)centerPosition.Y - sprite.Height / 2,
                                          sprite.Width, sprite.Height);
        }
        #endregion

        #region Properties
        #endregion

        #region Public methods

        public void Update(MouseState mouse) 
        {

        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion

        #region Private methods
        #endregion
    }
}
