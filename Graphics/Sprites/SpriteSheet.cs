using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solar.Graphics.Sprites
{
    public class SpriteSheet
    {
        private readonly Vector2 origin;

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
            Rows = texture.Bounds.Width/(int) spriteRect.X;
            Columns = texture.Bounds.Height/(int) spriteRect.Y;
        }

        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public SpriteSheet ShallowCopy()
        {
            var other = (SpriteSheet) MemberwiseClone();

            return other;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffect, int currentFrame)
        {
            int width = Texture.Width/Columns;
            int height = Texture.Height/Rows;
            var row = (int) (currentFrame/(float) Columns);
            int column = currentFrame%Columns;

            var sourceRectangle = new Rectangle(width*column, height*row, width, height);
            var destinationRectangle = new Rectangle((int) location.X, (int) location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, origin, spriteEffect, 0f);
        }
    }
}