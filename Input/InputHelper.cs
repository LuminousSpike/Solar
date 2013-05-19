using Microsoft.Xna.Framework.Input;

namespace Solar.Input
{
    /// <summary>
    /// Helper class for taking multiple inputs.
    /// </summary>
    static public class InputHelper
    {
        static KeyboardState currentKeyboardState, previousKeyboardState;
        static GamePadState currentGamePadState, previousGamePadState;

        /// <summary>
        /// Updates the current states.
        /// </summary>
        static public void UpdateCurrentInputStates()
        {
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
        }

        /// <summary>
        /// Updates the previous states.
        /// </summary>
        static public void UpdatePreviousInputStates()
        {
            previousKeyboardState = Keyboard.GetState();
            previousGamePadState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
        }

        /// <summary>
        /// Checks if input is pressed down.
        /// </summary>
        /// <param name="keyboardKey"></param>
        /// <param name="gamePadButton"></param>
        /// <returns></returns>
        static public bool InputDown(Keys keyboardKey, Buttons gamePadButton)
        {
            if (currentKeyboardState.IsKeyDown(keyboardKey) || currentGamePadState.IsButtonDown(gamePadButton))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if input is released.
        /// </summary>
        /// <param name="keyboardKey"></param>
        /// <param name="gamePadButton"></param>
        /// <returns></returns>
        static public bool InputUp(Keys keyboardKey, Buttons gamePadButton)
        {
            if (currentKeyboardState.IsKeyUp(keyboardKey) || currentGamePadState.IsButtonUp(gamePadButton))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if input is pressed and released.
        /// </summary>
        /// <param name="keyboardKey"></param>
        /// <param name="gamePadButton"></param>
        /// <returns></returns>
        static public bool InputPressed(Keys keyboardKey, Buttons gamePadButton)
        {
            if ((currentKeyboardState.IsKeyDown(keyboardKey) || currentGamePadState.IsButtonDown(gamePadButton)) && (currentKeyboardState.IsKeyUp(keyboardKey) || currentGamePadState.IsButtonUp(gamePadButton)))
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
