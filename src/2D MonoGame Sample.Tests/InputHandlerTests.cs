using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Xunit;

namespace _2D_MonoGame_Sample.Tests
{
    // InputHandler drives its state from real hardware polling (Keyboard.GetState(), etc.),
    // which isn't controllable from a headless test host. These tests reach past that by
    // writing directly to InputHandler's private static state via reflection, then exercise
    // the (otherwise pure) comparison logic in isolation. Tests run sequentially against
    // shared static state, so each test sets both prev/current state explicitly rather than
    // relying on ordering.
    public class InputHandlerTests
    {
        private static void SetKeyboardState(Keys[] previous, Keys[] current)
        {
            SetStaticField("prevKeyboardState", new KeyboardState(previous));
            SetStaticField("keyboardState", new KeyboardState(current));
        }

        private static void SetGamePadState(GamePadState previous, GamePadState current)
        {
            SetStaticField("prevGamePadState", previous);
            SetStaticField("gamePadState", current);
        }

        private static GamePadState MakeGamePadState(Buttons buttons = 0, bool dPadUp = false, bool dPadDown = false)
        {
            var thumbSticks = new GamePadThumbSticks(Vector2.Zero, Vector2.Zero);
            var triggers = new GamePadTriggers(0f, 0f);
            var gamePadButtons = new GamePadButtons(buttons);
            var dPad = new GamePadDPad(
                dPadUp ? ButtonState.Pressed : ButtonState.Released,
                dPadDown ? ButtonState.Pressed : ButtonState.Released,
                ButtonState.Released,
                ButtonState.Released);

            return new GamePadState(thumbSticks, triggers, gamePadButtons, dPad);
        }

        private static void SetStaticField(string name, object value)
        {
            typeof(InputHandler)
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, value);
        }

        [Fact]
        public void WasKeyPressed_KeyTransitionsFromUpToDown_ReturnsTrue()
        {
            SetKeyboardState(previous: new Keys[0], current: new[] { Keys.Space });

            Assert.True(InputHandler.WasKeyPressed(Keys.Space));
        }

        [Fact]
        public void WasKeyPressed_KeyHeldAcrossBothFrames_ReturnsFalse()
        {
            SetKeyboardState(previous: new[] { Keys.Space }, current: new[] { Keys.Space });

            Assert.False(InputHandler.WasKeyPressed(Keys.Space));
        }

        [Fact]
        public void WasKeyPressed_KeyNeverDown_ReturnsFalse()
        {
            SetKeyboardState(previous: new Keys[0], current: new Keys[0]);

            Assert.False(InputHandler.WasKeyPressed(Keys.Space));
        }

        [Fact]
        public void IsHoldingKey_KeyDownBothFrames_ReturnsTrue()
        {
            SetKeyboardState(previous: new[] { Keys.Up }, current: new[] { Keys.Up });

            Assert.True(InputHandler.IsHoldingKey(Keys.Up));
        }

        [Fact]
        public void IsHoldingKey_KeyJustPressed_ReturnsFalse()
        {
            SetKeyboardState(previous: new Keys[0], current: new[] { Keys.Up });

            Assert.False(InputHandler.IsHoldingKey(Keys.Up));
        }

        [Fact]
        public void HasReleasedKey_KeyTransitionsFromDownToUp_ReturnsTrue()
        {
            SetKeyboardState(previous: new[] { Keys.Down }, current: new Keys[0]);

            Assert.True(InputHandler.HasReleasedKey(Keys.Down));
        }

        [Fact]
        public void HasReleasedKey_KeyStillDown_ReturnsFalse()
        {
            SetKeyboardState(previous: new[] { Keys.Down }, current: new[] { Keys.Down });

            Assert.False(InputHandler.HasReleasedKey(Keys.Down));
        }

        [Fact]
        public void IsKeyDown_ReflectsCurrentStateOnly()
        {
            SetKeyboardState(previous: new Keys[0], current: new[] { Keys.Escape });

            Assert.True(InputHandler.IsKeyDown(Keys.Escape));
            Assert.False(InputHandler.IsKeyUp(Keys.Escape));
        }

        [Fact]
        public void WasButtonPressed_ButtonTransitionsFromUpToDown_ReturnsTrue()
        {
            var previous = MakeGamePadState();
            var current = MakeGamePadState(buttons: Buttons.A);

            SetGamePadState(previous, current);

            Assert.True(InputHandler.WasButtonPressed(Buttons.A));
        }

        [Fact]
        public void WasButtonPressed_ButtonHeldAcrossBothFrames_ReturnsFalse()
        {
            var state = MakeGamePadState(buttons: Buttons.A);

            SetGamePadState(state, state);

            Assert.False(InputHandler.WasButtonPressed(Buttons.A));
        }

        [Fact]
        public void WasDPadUpPressed_DPadUpHeld_ReturnsTrue()
        {
            var current = MakeGamePadState(dPadUp: true);

            SetGamePadState(current, current);

            Assert.True(InputHandler.WasDPadUpPressed());
            Assert.False(InputHandler.WasDPadDownPressed());
        }

        [Fact]
        public void CurrentGamePadState_ReturnsLatestPolledState()
        {
            var current = MakeGamePadState(buttons: Buttons.A);

            SetGamePadState(MakeGamePadState(), current);

            Assert.Equal(ButtonState.Pressed, InputHandler.CurrentGamePadState.Buttons.A);
        }
    }
}
