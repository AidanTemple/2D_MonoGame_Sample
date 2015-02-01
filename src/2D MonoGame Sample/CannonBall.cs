#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace _2D_MonoGame_Sample
{
    class CannonBall : Sprite
    {
        #region Private Members

        private Rectangle m_Viewport;

        #endregion

        #region Properties

        public Vector2 Velocity { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        #endregion

        #region Initialisation

        public CannonBall(GraphicsDevice device)
        {
            Texture = ContentManager.CannonBall;

            if(Texture != null)
            {
                Width = Texture.Width;
                Height = Texture.Height;
            }

            Origin = new Vector2(Width / 2, Height / 2);

            Velocity = Vector2.Zero;

            m_Viewport = new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height);

            IsAlive = false;
        }

        #endregion

        #region Update

        public override void Update()
        {
            if (IsAlive)
            {
                Position += Velocity;

                if (!m_Viewport.Contains(new Point((int)Position.X, (int)Position.Y)))
                {
                    IsAlive = false;
                }
            }
        }

        #endregion

        #region Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsAlive)
            {
                spriteBatch.Draw(Texture, Position, Color);

                base.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
