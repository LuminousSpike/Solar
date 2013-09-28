using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solar.GUI.Controls
{
    public class Box
    {
        private readonly Color BColor;
        private readonly int BWidth;
        private readonly int Height;
        private readonly Color MColor;
        private readonly Texture2D MainTexture;
        private readonly int Width;
        private readonly Vector2 bHorizontal;
        private readonly Vector2 bVertical;
        private Texture2D BorderTexture;
        private Vector2 Position, Scale;

        public Box(Vector2 position, int width, int height, int bWidth, Color mColor, Color bColor,
            GraphicsDevice graphicsdevice)
        {
            Position = position;
            Width = width;
            Height = height;
            BWidth = bWidth;
            Scale = new Vector2(width - bWidth, height - bWidth);
            MColor = mColor;
            BColor = bColor;

            // Set border positions
            bVertical = new Vector2(bWidth, height - BWidth);
            bHorizontal = new Vector2(width + BWidth, bWidth);

            // Create Texture
            var myMainColor = new Color(100f, 100f, 100f, 100);
            MainTexture = new Texture2D(graphicsdevice, 1, 1);
            MainTexture.SetData(new[] {myMainColor});
            var myBorderColor = new Color(100f, 100f, 100f, 100);
            BorderTexture = new Texture2D(graphicsdevice, 1, 1);
            BorderTexture.SetData(new[] {myBorderColor});
        }

        public void UnloadContent()
        {
            MainTexture.Dispose();
        }

        public void UpdateScale(int value)
        {
            Scale.X = (((float) Width/100)*value) - BWidth;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            // Draw borders
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y), null, BColor, 0f, Vector2.Zero,
                bVertical, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X + Width - BWidth, Position.Y), null, BColor, 0f,
                Vector2.Zero, bVertical, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y - BWidth), null, BColor, 0f,
                Vector2.Zero, bHorizontal, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y + Height - BWidth), null, BColor,
                0f, Vector2.Zero, bHorizontal, SpriteEffects.None, 0f);

            // Draw fill
            spritebatch.Draw(MainTexture, Position, null, MColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}