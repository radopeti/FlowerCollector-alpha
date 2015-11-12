using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlowerCollector1
{
    class Explosion
    {
        #region fields
        //location of the animation
        Rectangle drawRectangle;

        //strip info
        Texture2D sprite;
        String stripName = "explosion";
        int frameWidth, frameHeight;

        //info about the animation picture
        const int FRAMES_PER_ROW = 7;
        const int NUMBER_OF_ROWS = 7;
        const int NUMBER_OF_FRAMES = 49;

        //animation control helpers
        Rectangle sourceRectangle;
        int currentFrame;
        const int FRAME_TIME = 10;
        int elapsedFrameTime = 0;

        bool playing = false;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentManager">content manager</param>
        public Explosion(ContentManager contentManager) 
        {
            currentFrame = 0;
            sprite = contentManager.Load<Texture2D>(stripName);

            frameWidth = sprite.Width / FRAMES_PER_ROW;
            frameHeight = sprite.Height / NUMBER_OF_ROWS;

            drawRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        #endregion

        #region properties

        /// <summary>
        /// current frame of the animation
        /// </summary>
        public int CurrentFrame
        { 
            get { return currentFrame; } 
        }

        #endregion

        #region methods

        /// <summary>
        /// Sets the location of the explosion's source rectangle
        /// </summary>
        /// <param name="currentFrameNumber"></param>
        private void setSourceRectangleLocation(int currentFrameNumber)
        {
            sourceRectangle.X = (currentFrameNumber % NUMBER_OF_ROWS) * frameWidth;
            sourceRectangle.Y = (currentFrameNumber / NUMBER_OF_ROWS) * frameHeight;
        }

        
        /// <summary>
        /// Play initializes and sets the position of the explosion
        /// </summary>
        /// <param name="centerPosition">center position of the explosion</param>
        public void Play(Vector2 centerPosition)
        {
            playing = true;

            currentFrame = 0;
            elapsedFrameTime = 0;

            drawRectangle.X = (int)centerPosition.X - frameWidth / 2;
            drawRectangle.Y = (int)centerPosition.Y - frameHeight / 2;

            setSourceRectangleLocation(currentFrame);
        }

        /// <summary>
        /// Update animates the explosion
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime) 
        {
            if (playing) 
            {
                elapsedFrameTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedFrameTime > FRAME_TIME) 
                {
                    elapsedFrameTime = 0;
                    if (currentFrame < NUMBER_OF_FRAMES - 1)
                    {
                        currentFrame++;
                        setSourceRectangleLocation(currentFrame);
                    }
                    else 
                    {
                        playing = false;
                    }
                }
            }
        }

        /// <summary>
        /// Draw the explosion if it has initialized
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing) 
            {
                spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
            }
        }

        #endregion
    }
}
