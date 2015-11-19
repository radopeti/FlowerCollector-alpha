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
        SpriteFont spriteFont;
        String elapsedTime;
        float elapsedTimeCounter;
        Vector2 position;
        int from;
        int to;

        bool finished;

        public Timer(ContentManager contentManager, String spriteFontName, Vector2 position, int from, int to)
        {
            spriteFont = contentManager.Load<SpriteFont>(spriteFontName);
            this.position = position;
            this.from = from;
            this.to = to;
            elapsedTimeCounter = from;
            elapsedTime = from.ToString();
        }

        public void Start(GameTime gameTime)
        {
            if (from < to)
            {
                elapsedTimeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

                elapsedTime = ((int)elapsedTimeCounter).ToString();
            }

            if (from > to)
            {
                {
                    elapsedTimeCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    elapsedTime = ((int)elapsedTimeCounter).ToString();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, elapsedTime, position, Color.White);
        }
    }
}
