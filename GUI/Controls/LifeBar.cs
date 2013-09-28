using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar.GUI.Controls
{
    public class LifeBar
    {
        private readonly Color BColor;
        private readonly int BWidth;
        private readonly int Height;
        private readonly Color MColor;
        private readonly Vector2 Position;
        private readonly string TexturePath;
        private readonly int Width;
        private float BorderAlpha;
        private float MainAlpha;
        private float MaxValue;
        private Vector2 Scale;
        private Texture2D Texture;
        private Box myBox;

        public LifeBar(Vector2 position, float maxValue, string texturePath = null)
        {
            Position = position;
            MaxValue = maxValue;
            TexturePath = texturePath;
        }

        public LifeBar(Vector2 position, float maxValue, int width, int height, int bWidth, Color mColor, Color bColor,
            float mainAlpha, float borderAlpha)
        {
            Position = position;
            MaxValue = maxValue;
            Width = width;
            Height = height;
            BWidth = bWidth;
            MColor = mColor;
            BColor = bColor;
            MainAlpha = mainAlpha;
            BorderAlpha = borderAlpha;
        }

        private void Initialize()
        {
        }

        // Loads any content required
        public void LoadContent(ContentManager content, GraphicsDevice graphicsdevice)
        {
            if (TexturePath != null)
            {
                Texture = content.Load<Texture2D>(TexturePath);
                Scale = new Vector2(1, 1);
            }
            else
                myBox = new Box(Position, Width, Height, BWidth, MColor, BColor, graphicsdevice);
        }

        public void UnloadContent()
        {
            if (Texture == null)
                myBox.UnloadContent();
        }

        public void Update(int value, int maxValue)
        {
            if (Texture == null)
                myBox.UpdateScale(value);
            else
                Scale.X = (((float) Texture.Width/maxValue)*(value/(float) Texture.Width));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (Texture != null)
            {
                // Only scales the image, need to code a way to render a partial image
                spritebatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
            else
            {
                myBox.Draw(spritebatch);
            }
        }
    }
}