using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solar.GUI.Controls;

namespace Solar.GUI.Containers
{
    public class ScrollableList : GuiSystem
    {
        // Scrollable list properties
        private readonly int Height;
        private readonly Timer TransformTime = new Timer(1);
        private readonly int Width;
        private readonly int Xposition;
        private readonly int Yposition;
        private bool Scrolling;
        private int TestJump = 50;
        private int XoffSet;
        private float YoffSet;

        public ScrollableList(int height, int width, int x, int y)
        {
            Height = height;
            Width = width;
            Xposition = x;
            Yposition = y;
        }

        public void Update(GameTime gameTime)
        {
            var elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Scroll(elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                YoffSet--;

            if (Scrolling)
            {
                if (TransformTime.Update(elapsedTime))
                {
                    Scrolling = false;
                    TransformTime.Stop();
                }
                else
                    YoffSet = MathHelper.Lerp(0.0f, 100f, TransformTime.Time);
            }
        }

        public void Scroll(float elapsedTime)
        {
            TransformTime.Start();
            TransformTime.Update(elapsedTime);
            Scrolling = true;
        }

        // Loops through each Object to execute the draw code.
        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatch savedSpriteBatch = spriteBatch;
            var me = new RasterizerState {ScissorTestEnable = true};
            spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle(Xposition, Yposition, Width, Height);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.None, me, null, Matrix.CreateTranslation(Xposition, Yposition + YoffSet, 0));
            foreach (Box item in GuiBoxList)
            {
                item.Draw(spriteBatch);
            }

            foreach (Button item in GuiButtonList)
            {
                item.Draw(spriteBatch);
            }

            foreach (LifeBar item in GuiLifeBarList)
            {
                item.Draw(spriteBatch);
            }

            foreach (TextBox item in GuiTextBoxList)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();

            // Reset the spriteBatch
            spriteBatch = savedSpriteBatch;
        }
    }
}