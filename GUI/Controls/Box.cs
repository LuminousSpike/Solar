﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar.GUI.Controls
{
    public class Box
    {
        private Vector2 Position, Scale, bVertical, bHorizontal;
        private Texture2D MainTexture, BorderTexture;
        private int Width, Height, BWidth;
        private Color MColor, BColor;

        public Box(Vector2 position, int width, int height, int bWidth, Color mColor, Color bColor, GraphicsDevice graphicsdevice)
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
            Color myMainColor = new Color(100f, 100f, 100f, 100);
            MainTexture = new Texture2D(graphicsdevice, 1, 1);
            MainTexture.SetData(new[] { myMainColor });
            Color myBorderColor = new Color(100f, 100f, 100f, 100);
            BorderTexture = new Texture2D(graphicsdevice, 1, 1);
            BorderTexture.SetData(new[] { myBorderColor });
        }

        public void UnloadContent()
        {
            MainTexture.Dispose();
        }

        public void UpdateScale(int value)
        {
            Scale.X = (((float)Width / 100) * value) - BWidth;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            // Draw borders
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y), null, BColor, 0f, Vector2.Zero, bVertical, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X + Width - BWidth, Position.Y), null, BColor, 0f, Vector2.Zero, bVertical, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y - BWidth), null, BColor, 0f, Vector2.Zero, bHorizontal, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y + Height - BWidth), null, BColor, 0f, Vector2.Zero, bHorizontal, SpriteEffects.None, 0f);

            // Draw fill
            spritebatch.Draw(MainTexture, Position, null, MColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
