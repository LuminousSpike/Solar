using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Solar.Graphics.Sprites
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        private Vector2 origin;

        public SpriteSheet(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
        }

        public SpriteSheet(Texture2D texture, Vector2 spriteRect)
        {
            Texture = texture;
            origin = spriteRect;
            Rows = texture.Bounds.Width / (int)spriteRect.X;
            Columns = texture.Bounds.Height / (int)spriteRect.Y;
        }

        public SpriteSheet ShallowCopy()
        {
            SpriteSheet other = (SpriteSheet)this.MemberwiseClone();

            return other;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffect, int currentFrame)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, origin, spriteEffect, 0f);
        }

    }
}
