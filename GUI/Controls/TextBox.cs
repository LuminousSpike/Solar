﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar.GUI.Controls
{
    public class TextBox
    {
        private readonly Color BColor;
        private readonly int BWidth;
        private readonly string FontPath;
        private readonly int Height;
        private readonly Color MColor;
        private readonly string TexturePath;
        private readonly bool Textured;
        private readonly int Width;
        private Texture2D BorderTexture;
        public Texture2D CurrentTexture;
        private Box DefaultBox;
        private SpriteFont Font;
        private Box HoverBox;
        private Texture2D HoverTexture;
        private Texture2D MainTexture;
        private Vector2 Position, Scale;
        private Box SelectedBox;
        private Texture2D SelectedTexture;
        private string Text;
        private Vector2 TextPosition;

        public TextBox(Vector2 position, string text, string texturePath = null)
        {
            Position = position;
            Textured = true;
            Text = text;
            TexturePath = texturePath;
        }

        public TextBox(Vector2 position, int width, int height, int bWidth, Color mColor, Color bColor, string text,
            string fontPath)
        {
            Position = position;
            Width = width;
            Height = height;
            BWidth = bWidth;
            Scale = new Vector2(width - bWidth, height - bWidth);
            MColor = mColor;
            BColor = bColor;
            Text = text;
            FontPath = fontPath;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsdevice)
        {
            Font = content.Load<SpriteFont>(FontPath);
            if (TexturePath != null)
            {
                MainTexture = content.Load<Texture2D>(TexturePath);
                ;
                CurrentTexture = MainTexture;
                TextPosition =
                    new Vector2((int) (Position.X + (CurrentTexture.Width/2) - (Font.MeasureString(Text).X/2)),
                        (int) (Position.Y + (CurrentTexture.Height/2) - (Font.MeasureString(Text).Y/2)));
            }
            else
            {
                DefaultBox = new Box(Position, Width, Height, BWidth, MColor, BColor, graphicsdevice);
                TextPosition = new Vector2((int) (Position.X + (Width/2) - (Font.MeasureString(Text).X/2)),
                    (int) (Position.Y + (Height/2) - (Font.MeasureString(Text).Y/2)));
            }
        }

        public void UnloadContent()
        {
            DefaultBox.UnloadContent();
        }

        public void UpdateText(string text, GraphicsDevice graphicsdevice)
        {
            Text = text;
            if (MainTexture == null)
            {
                TextPosition = new Vector2((int) (Position.X + (Width/2) - (Font.MeasureString(Text).X/2)),
                    (int) (Position.Y + (Height/2) - (Font.MeasureString(Text).Y/2)));
            }
            else
                TextPosition =
                    new Vector2((int) (Position.X + (CurrentTexture.Width/2) - (Font.MeasureString(Text).X/2)),
                        (int) (Position.Y + (CurrentTexture.Height/2) - (Font.MeasureString(Text).Y/2)));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (Textured)
            {
                spritebatch.Draw(CurrentTexture, Position, Color.White);
                spritebatch.DrawString(Font, Text, TextPosition, Color.White);
            }
            else
            {
                DefaultBox.Draw(spritebatch);
                spritebatch.DrawString(Font, Text, TextPosition, Color.White);
            }
        }
    }
}