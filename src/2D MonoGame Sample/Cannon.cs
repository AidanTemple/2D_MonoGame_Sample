#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
#endregion

namespace _2D_MonoGame_Sample
{
    class Cannon : Sprite
    {
        #region Constants

        private const int m_MaxCannonBalls = 5;

        #endregion

        #region Private Members

        private CannonBall[] m_CannonBalls;

        #endregion

        #region Properties

        public CannonBall[] CannonBalls
        {
            get { return m_CannonBalls; }
            set { m_CannonBalls = value; }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        #endregion

        #region Initialisation

        public Cannon(GraphicsDevice device)
        {
            Texture = ContentManager.Cannon;

            if(Texture != null)
            {
                Width = Texture.Width;
                Height = Texture.Height;
            }

            Position = new Vector2(120, device.Viewport.Height - 80);
            Origin = new Vector2(Width / 2, Height / 2);

            IsAlive = true;

            m_CannonBalls = new CannonBall[m_MaxCannonBalls];

            for(int i = 0; i < m_MaxCannonBalls; i++)
            {
                m_CannonBalls[i] = new CannonBall(device);
            }
        }

        #endregion

        #region Helper Methods

        private void FireCannon()
        {
            foreach(CannonBall ball in m_CannonBalls)
            {
                if(!ball.IsAlive)
                {
                    ball.IsAlive = true;
                    ball.Position = Position - ball.Origin;

                    ball.Velocity = new Vector2((float)Math.Cos(Rotation), 
                        (float)Math.Sin(Rotation)) * 5.0f;

                    return;
                }
            }
        }

        #endregion

        #region Update

        public override void Update()
        {
            UpdateInput();

            Rotation = MathHelper.Clamp(Rotation, -MathHelper.PiOver2, 0);

            UpdateCannonBalls();
        }

        private void UpdateInput()
        {
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0
                || InputHandler.IsHoldingKey(Keys.Up))
            {
                Rotation -= 0.01f;
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0
                || InputHandler.IsHoldingKey(Keys.Down))
            {
                Rotation += 0.01f;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed 
                || InputHandler.WasKeyPressed(Keys.Space))
            {
                FireCannon();
            }

            InputHandler.Update();
        }

        private void UpdateCannonBalls()
        {
            foreach(CannonBall ball in m_CannonBalls)
            {
                ball.Update();
            }
        }

        #endregion

        #region Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                DrawCannonBall(spriteBatch);

                spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, 1f, Effects, 0);

                base.Draw(spriteBatch);
            }
        }

        private void DrawCannonBall(SpriteBatch spriteBatch)
        {
            foreach (CannonBall ball in m_CannonBalls)
            {
                if (ball.IsAlive)
                {
                    ball.Draw(spriteBatch);
                }
            }
        }

        #endregion
    }
}