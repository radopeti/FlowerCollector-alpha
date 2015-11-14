﻿using System;
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
    class Button
    {
        #region Fields

            //texture
                Texture2D sprite;
                Rectangle drawRectangle;
                Rectangle sourceRectangle;
            //click support
                bool clickStarted = false;
                bool buttonReleased = true;
            //gamestate
                GameState gameState;

        #endregion

        #region Constructor

            public Button(ContentManager contentManager, String spriteName, GameState gameState, int posX, int posY) 
            {
                sprite = contentManager.Load<Texture2D>(spriteName);
                this.gameState = gameState;
                drawRectangle = new Rectangle(posX, posY, sprite.Width / 2, sprite.Height);
                sourceRectangle = new Rectangle(0, 0, sprite.Width / 2, sprite.Height);
            }

        #endregion

        #region Public Methods

            public void Update(MouseState mouse) 
            {
                if (drawRectangle.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && buttonReleased)
                {
                    buttonReleased = false;
                    clickStarted = true;
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        buttonReleased = true;
                        if (clickStarted)
                        {
                            clickStarted = false;
                            sourceRectangle.X = sprite.Width / 2;
                            Game1.ChangeGameState(gameState);
                        }
                    }
                }
                else 
                {
                    sourceRectangle.X = 0;
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
            }

        #endregion

        #region Private Methods
        #endregion
    }
}
