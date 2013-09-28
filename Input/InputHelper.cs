using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Solar.Input
{
    /// <summary>
    ///     Helper class for taking multiple inputs.
    /// </summary>
    public static class InputHelper
    {
        private static KeyboardState currentKeyboardState, previousKeyboardState;
        private static GamePadState currentGamePadState, previousGamePadState;

        /// <summary>
        ///     Updates the current states.
        /// </summary>
        public static void UpdateCurrentInputStates()
        {
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        /// <summary>
        ///     Updates the previous states.
        /// </summary>
        public static void UpdatePreviousInputStates()
        {
            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;
        }

        /// <summary>
        ///     Checks if input is pressed down.
        /// </summary>
        /// <param name="keyboardKey"></param>
        /// <param name="gamePadButton"></param>
        /// <returns></returns>
        public static bool InputDown(Keys keyboardKey, Buttons gamePadButton)
        {
            if (currentKeyboardState.IsKeyDown(keyboardKey) || currentGamePadState.IsButtonDown(gamePadButton))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Checks if input is released.
        /// </summary>
        /// <param name="keyboardKey"></param>
        /// <param name="gamePadButton"></param>
        /// <returns></returns>
        public static bool InputUp(Keys keyboardKey, Buttons gamePadButton)
        {
            if (currentKeyboardState.IsKeyUp(keyboardKey) || currentGamePadState.IsButtonUp(gamePadButton))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Checks if input is pressed and released.
        /// </summary>
        /// <param name="keyboardKey"></param>
        /// <param name="gamePadButton"></param>
        /// <returns></returns>
        public static bool InputPressed(Keys keyboardKey, Buttons gamePadButton)
        {
            //if ((previousKeyboardState.IsKeyDown(keyboardKey) || currentGamePadState.IsButtonDown(gamePadButton)) && (currentKeyboardState.IsKeyUp(keyboardKey) || currentGamePadState.IsButtonUp(gamePadButton)))
            if (previousKeyboardState.IsKeyDown(keyboardKey) && currentKeyboardState.IsKeyUp(keyboardKey))
            {
                return true;
            }
            return false;
        }
    }
}