#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace _2D_MonoGame_Sample
{
    class Enemy : Sprite
    {
        #region Properties

        public Vector2 Velocity { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        #endregion

        #region Initialisation

        public Enemy()
        {
            Texture = ContentManager.Enemy;

            if(Texture != null)
            {
                Width = Texture.Width;
                Height = Texture.Height;
            }

            Origin = new Vector2(Width / 2, Height / 2);

            Velocity = Vector2.Zero;

            IsAlive = false;
        }

        #endregion

        #region Update

        public override void Update()
        {

        }

        #endregion

        #region Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                spriteBatch.Draw(Texture, Position, Color);

                base.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
