#region Using Statement
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace _2D_MonoGame_Sample
{
    static class InputHandler
    {
        #region Private Members

        private static KeyboardState keyboardState;
        private static KeyboardState prevKeyboardState;

        private static GamePadState gamePadState;
        private static GamePadState prevGamePadState;

        private static MouseState mouseState;
        private static MouseState prevMouseState;

        #endregion

        #region GamePad Input

        public static bool WasButtonPressed(Buttons button)
        {
            return (prevGamePadState.IsButtonUp(button) && gamePadState.IsButtonDown(button));
        }

        public static bool WasDPadUpPressed()
        {
            return (gamePadState.DPad.Up == ButtonState.Pressed);
        }

        public static bool WasDPadDownPressed()
        {
            return (gamePadState.DPad.Down == ButtonState.Pressed);
        }

        public static bool WasDPadLeftPressed()
        {
            return (gamePadState.DPad.Left == ButtonState.Pressed);
        }

        public static bool WasDPadRightPressed()
        {
            return (gamePadState.DPad.Right == ButtonState.Pressed);
        }

        #endregion

        #region Keyboard Input

        public static bool IsKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key));
        }

        public static bool IsKeyUp(Keys key)
        {
            return (keyboardState.IsKeyUp(key));
        }

        public static bool IsHoldingKey(Keys key)
        {
            return (prevKeyboardState.IsKeyDown(key) && keyboardState.IsKeyDown(key));
        }

        public static bool WasKeyPressed(Keys key)
        {
            return (prevKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key));
        }

        public static bool HasReleasedKey(Keys key)
        {
            return (prevKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key));
        }

        #endregion

        #region Update

        public static void Update()
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            prevGamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        #endregion
    }
}