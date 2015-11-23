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
    class Timer
    {
        #region Fields

            //Draw support
                SpriteFont spriteFont;
                String elapsedTime;
                Vector2 position;

            //second counter support
                float elapsedTimeCounter;
                int start;

            //finished flag
                bool finished;

        #endregion

        #region Constructor

                /// <summary>
                /// Timer constructor
                /// </summary>
                /// <param name="contentManager">content manager</param>
                /// <param name="spriteFontName">name of the spritefont content</param>
                /// <param name="position">position of the timer</param>
                /// <param name="start"></param>
                public Timer(ContentManager contentManager, String spriteFontName, Vector2 position, int start)
                {
                    spriteFont = contentManager.Load<SpriteFont>(spriteFontName);
                    this.position = position;
                    this.start = start;
                    elapsedTimeCounter = start;
                    elapsedTime = start.ToString();
                }

        #endregion

        #region Properties



        #endregion

        #region Public Methods

                /// <summary>
                /// Start the timer
                /// </summary>
                /// <param name="gameTime">game time</param>
                public void Start(GameTime gameTime)
                {
                    if (start == 0)
                    {
                        elapsedTimeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        elapsedTime = ((int)elapsedTimeCounter).ToString();
                    }

                    if (start > 0)
                    {
                        {
                            elapsedTimeCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                            elapsedTime = ((int)elapsedTimeCounter).ToString();
                        }
                    }
                }

                /// <summary>
                /// Draw the timer
                /// </summary>
                /// <param name="spriteBatch">sprite batch</param>
                public void Draw(SpriteBatch spriteBatch)
                {
                    spriteBatch.DrawString(spriteFont, elapsedTime, position, Color.White);
                }

        #endregion

        #region Private Methods



        #endregion

        
    }
}
