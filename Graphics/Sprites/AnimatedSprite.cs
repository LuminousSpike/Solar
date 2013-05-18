using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Solar.Graphics.Sprites
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        private int currentFrame;
        private int totalFrames;
        private int elapsedTime, previousElapsedTime, frameTime;
        private int previousMin, previousMax;

        public AnimatedSprite(Texture2D texture, int rows, int columns, int frametime)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            frameTime = frametime;
        }

        public AnimatedSprite ShallowCopy()
        {
            AnimatedSprite other = (AnimatedSprite)this.MemberwiseClone();
            
            return other;
        }

        public void Update(GameTime gameTime, int minFrame, int maxFrame)
        {
            // Update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (maxFrame != previousMax || minFrame != previousMin)
            {
                currentFrame = minFrame;
            }

            if (elapsedTime > frameTime)
            {
                currentFrame++;
                if (currentFrame == totalFrames || currentFrame >= maxFrame)
                {
                    currentFrame = minFrame;
                }
                elapsedTime = 0;
            }
            previousMax = maxFrame;
            previousMin = minFrame;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool mirrored)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            if (mirrored == true)
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }
    }
}
