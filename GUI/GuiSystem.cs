using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solar.GUI.Containers;
using Solar.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Solar.GUI
{
    // System for making the GUI easier to use. Handles Loading, Unloading, and Drawing each Object.
    public class GuiSystem
    {
        // Lists for each GUI Object
        protected List<Box> GuiBoxList;
        protected List<Button> GuiButtonList;
        protected List<LifeBar> GuiLifeBarList;
        protected List<TextBox> GuiTextBoxList;
        protected List<ScrollableList> GuiScrollableList;

        protected KeyboardState CurrentKeyboardState, PreviousKeyboardState;
        protected int ButtonIndex = 0;

        public virtual int GuiButtonCount
        {
            get { return GuiButtonList.Count; }
        }

        public virtual int GuiButtonIndex
        {
            get { return ButtonIndex; }
        }

        // Initializes the list, here incase an Object requires something at runtime in the future.
        public virtual void Initialize()
        {
            GuiBoxList = new List<Box>();
            GuiButtonList = new List<Button>();
            GuiLifeBarList = new List<LifeBar>();
            GuiTextBoxList = new List<TextBox>();
            GuiScrollableList = new List<ScrollableList>();
        }

        // Anything which needs to load content or do something at content load time.
        public virtual void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            foreach (Button item in GuiButtonList)
            {
                item.LoadContent(content, graphicsDevice);
            }
            foreach (LifeBar item in GuiLifeBarList)
            {
                item.LoadContent(content, graphicsDevice);
            }
            foreach (TextBox item in GuiTextBoxList)
            {
                item.LoadContent(content, graphicsDevice);
            }
            foreach (ScrollableList item in GuiScrollableList)
            {
                item.LoadContent(content, graphicsDevice);
            }
            ButtonIndexUpdate(0);
        }

        // Anything which uses textures generated at runtime or in the future.
        public virtual void UnloadContent()
        {
            foreach (Box item in GuiBoxList)
            {
                item.UnloadContent();
            }

            foreach (Button item in GuiButtonList)
            {
                item.UnloadContent();
            }

            foreach (LifeBar item in GuiLifeBarList)
            {
                item.UnloadContent();
            }

            foreach (TextBox item in GuiTextBoxList)
            {
                item.UnloadContent();
            }

            foreach (ScrollableList item in GuiScrollableList)
            {
                item.UnloadContent();
            }
        }

        // The Add function is used for adding any GUI Object to the lists above.
        public  void Add(Box guiBox)
        {
            GuiBoxList.Add(guiBox);
        }

        public void Add(Button guiButton)
        {
            GuiButtonList.Add(guiButton);
        }

        public void Add(LifeBar guiLifeBar)
        {
            GuiLifeBarList.Add(guiLifeBar);
        }

        public void Add(TextBox guiTextBox)
        {
            GuiTextBoxList.Add(guiTextBox);
        }

        public void Add(ScrollableList guiScrollableList)
        {
            GuiScrollableList.Add(guiScrollableList);
        }

        // Any update code which is generic for all Objects of a type.
        public virtual void Update(MouseState mouseState)
        {
            // Button Management
            UpdateMouse(mouseState);

            CurrentKeyboardState = Keyboard.GetState();
            if ((CurrentKeyboardState.IsKeyUp(Keys.Down) && PreviousKeyboardState.IsKeyDown(Keys.Down)) || (CurrentKeyboardState.IsKeyUp(Keys.S) && PreviousKeyboardState.IsKeyDown(Keys.S)))
            {
                if (ButtonIndex < GuiButtonCount - 1)
                    ButtonIndex++;

                ButtonIndexUpdate(ButtonIndex);
            }

            if ((CurrentKeyboardState.IsKeyUp(Keys.Up) && PreviousKeyboardState.IsKeyDown(Keys.Up)) || (CurrentKeyboardState.IsKeyUp(Keys.W) && PreviousKeyboardState.IsKeyDown(Keys.W)))
            {
                if (ButtonIndex > 0)
                    ButtonIndex--;

                ButtonIndexUpdate(ButtonIndex);
            }
            PreviousKeyboardState = CurrentKeyboardState;
        }

        public void ButtonIndexUpdate(int index)
        {
            for (int i = 0; i < GuiButtonList.Count; i++)
            {
                if (i == index)
                    GuiButtonList[i].IsSelected = true;
                else
                    GuiButtonList[i].IsSelected = false;
            }
        }

        // Loops through each Object to execute the draw code.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
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
        }

        private void UpdateMouse(MouseState mouseState)
        {
            foreach (Button item in GuiButtonList)
            {
                if (GetMouseOverRect(item.Rectangle, item.Position, mouseState))
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }

        private bool GetMouseOverRect(Rectangle rect, Vector2 position, MouseState mouseState)
        {
            if (mouseState.X > position.X - (rect.Width / 2) && mouseState.X < position.X + (rect.Width / 2) &&
                mouseState.Y > position.Y - (rect.Height / 2) && mouseState.Y < position.Y + (rect.Height / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
