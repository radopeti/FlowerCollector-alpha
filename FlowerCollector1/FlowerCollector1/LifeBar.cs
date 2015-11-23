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
    /// <summary>
    /// Represent a life bar with 3 hearts
    /// </summary>
    class LifeBar
    {
        #region Fields

            //List of hearths
                List<Hearth> hearths;
                const int INITIAL_NUMBER_OF_HEARTHS = 3;
                const int BORDER_SIZE = 5;

        #endregion

        #region Constructor

                /// <summary>
                /// Constructor
                /// </summary>
                /// <param name="contentManager">content manager</param>
                /// <param name="position">position of the lifebar</param>
                public LifeBar(ContentManager contentManager, Vector2 position)
                {
                    hearths = new List<Hearth>();
                    for (int i = 0; i < INITIAL_NUMBER_OF_HEARTHS; i++)
                    {
                        Vector2 hearthPosition = new Vector2(position.X + (BORDER_SIZE * (i + 1)) + i * 32, position.Y);
                        hearths.Add(new Hearth(contentManager, hearthPosition));
                    }
                }

        #endregion

        #region Properties

                /// <summary>
                /// Current number of lifes
                /// </summary>
                public int Count
                {
                    get { return hearths.Count; }
                }

        #endregion

        #region Public Methods

                /// <summary>
                /// Remove a hearth from the list
                /// </summary>
                public void Remove()
                {
                    if (hearths.Count > 0)
                    {
                        hearths.RemoveAt(hearths.Count - 1);
                    }
                }

                /// <summary>
                /// Draw the life bar
                /// </summary>
                /// <param name="spriteBatch">sprite batch</param>
                public void Draw(SpriteBatch spriteBatch)
                {
                    foreach (Hearth hearth in hearths)
                    {
                        hearth.Draw(spriteBatch);
                    }
                }

                /// <summary>
                /// Represent a hearth sprite on a given position
                /// </summary>

        #endregion

        #region Private Methods



        #endregion

        protected class Hearth 
        {
            //texture
            Texture2D sprite;
            Vector2 position;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="contentManager">content manager</param>
            /// <param name="position">position of the hearth</param>
            public Hearth(ContentManager contentManager, Vector2 position) 
            {
                sprite = contentManager.Load<Texture2D>("hearth");
                this.position = position;
            }

            /// <summary>
            /// Draw the sprite
            /// </summary>
            /// <param name="spriteBatch">sprite batch</param>
            public void Draw(SpriteBatch spriteBatch) 
            {
                spriteBatch.Draw(sprite, position, Color.White);
            }
        }
    }
}
