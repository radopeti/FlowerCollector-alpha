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
    class ScoreBar
    {
        #region Fields

        //Font and position
        SpriteFont spriteFont;
        Vector2 position;
        
        //Score message
        String message;
        int score;

        #endregion

        #region Constructor

            /// <summary>
            /// Costructor
            /// </summary>
            /// <param name="contentManager">content manager</param>
            /// <param name="position">position of the score bar</param>
            public ScoreBar(ContentManager contentManager, Vector2 position)
            {
                spriteFont = contentManager.Load<SpriteFont>("scoreFont");
                this.position = position;
                this.message = "Score: ";
            }


        #endregion

        #region Properties

            /// <summary>
            /// Score property
            /// </summary>
            public int Score 
            {
                get { return score; }
                set { score = value; }
            }

        #endregion

        #region Public Methods


            /// <summary>
            /// Draw the score bar
            /// </summary>
            /// <param name="spriteBatch">sprite batch</param>
            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.DrawString(spriteFont, message + score, position, Color.White);
            }

        #endregion

        #region Private Methods



        #endregion
    }
}
