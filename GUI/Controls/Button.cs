using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar.GUI.Controls
{
    public class Button
    {
        private readonly Color BColor;
        private readonly int BWidth;
        private readonly Color FontColor = Color.White;
        private readonly string FontPath;
        private readonly int Height;
        private readonly Color MColor;
        private readonly Color MColorSelected;
        private readonly string Text;
        private readonly string TextureMainPath;
        private readonly string TextureSelectedPath;
        private readonly bool Textured;
        private readonly int Width;
        public Texture2D CurrentTexture;
        private Box DefaultBox;
        private SpriteFont Font;
        private Box HoverBox;
        private Texture2D HoverTexture;
        private bool Selected;
        private Box SelectedBox;
        private Texture2D SelectedTexture;
        private Texture2D mainTexture;
        private Vector2 position;
        private Rectangle rectangle;
        private Vector2 scale, textPosition;

        public Button(Vector2 position, string text, string textureMainPath, string textureSelectedPath, string fontPath)
        {
            this.position = position;
            Textured = true;
            Text = text;
            TextureMainPath = textureMainPath;
            TextureSelectedPath = textureSelectedPath;
            FontPath = fontPath;
        }

        public Button(Vector2 position, int width, int height, int bWidth, Color mColor, Color mColorSelected,
            Color bColor, Color fontColor, string text, string fontPath)
        {
            this.position = position;
            Width = width;
            Height = height;
            BWidth = bWidth;
            scale = new Vector2(width - bWidth, height - bWidth);
            MColor = mColor;
            MColorSelected = mColorSelected;
            BColor = bColor;
            FontColor = fontColor;
            Text = text;
            FontPath = fontPath;
        }

        public bool IsSelected
        {
            get { return Selected; }
            set { Selected = value; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsdevice)
        {
            Font = content.Load<SpriteFont>(FontPath);
            if (TextureMainPath == null)
            {
                DefaultBox = new Box(position, Width, Height, BWidth, MColor, BColor, graphicsdevice);
                SelectedBox = new Box(position, Width, Height, BWidth, MColorSelected, BColor, graphicsdevice);
                textPosition = new Vector2((int) (position.X + (Width/2) - (Font.MeasureString(Text).X/2)),
                    (int) (position.Y + (Height/2) - (Font.MeasureString(Text).Y/2)));
            }
            else
            {
                mainTexture = content.Load<Texture2D>(TextureMainPath);
                SelectedTexture = content.Load<Texture2D>(TextureSelectedPath);
                CurrentTexture = mainTexture;
                textPosition =
                    new Vector2((int) (position.X + (CurrentTexture.Width/2) - (Font.MeasureString(Text).X/2)),
                        (int) (position.Y + (CurrentTexture.Height/2) - (Font.MeasureString(Text).Y/2)));
            }

            rectangle = new Rectangle((int) position.X, (int) position.Y, mainTexture.Width, mainTexture.Height);
        }

        public void UnloadContent()
        {
            if (TextureMainPath == null)
            {
                DefaultBox.UnloadContent();
                SelectedBox.UnloadContent();
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (Textured)
            {
                if (Selected)
                {
                    spritebatch.Draw(SelectedTexture, position, null, Color.White, 0f,
                        new Vector2(mainTexture.Width/2, mainTexture.Height/2), 1.0f, SpriteEffects.None, 0);
                }
                else
                {
                    spritebatch.Draw(CurrentTexture, position, null, Color.White, 0f,
                        new Vector2(mainTexture.Width/2, mainTexture.Height/2), 1.0f, SpriteEffects.None, 0);
                }
                spritebatch.DrawString(Font, Text, textPosition, FontColor, 0f,
                    new Vector2(mainTexture.Width/2, mainTexture.Height/2), 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                if (Selected)
                    SelectedBox.Draw(spritebatch);
                else
                    DefaultBox.Draw(spritebatch);
                spritebatch.DrawString(Font, Text, textPosition, FontColor);
            }
        }
    }
}