#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace _2D_MonoGame_Sample
{
    class Animation : Sprite
    {
        #region Private Members

        private int m_Rows;
        private int m_Columns;
        private int m_Frame;
        private int m_TotalFrames;

        #endregion

        #region Initialisation

        public Animation(Texture2D texture, Vector2 position, int rows, int columns)
        {
            Texture = texture;
            Position = position;

            m_Rows = rows;
            m_Columns = columns;

            m_Frame = 0;
            m_TotalFrames = m_Rows * m_Columns;
        }

        #endregion

        #region Update

        public override void Update()
        {
            m_Frame++;

            if (m_Frame == m_TotalFrames)
                m_Frame = 0;
        }

        #endregion

        #region Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / m_Columns;
            int height = Texture.Height / m_Rows;
            int row = m_Frame / m_Columns;
            int column = m_Frame % m_Columns;

            Rectangle source = new Rectangle(width * column, height * row, width, height);
            Rectangle destination = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(Texture, destination, source, Color.White);

            base.Draw(spriteBatch);
        }

        #endregion
    }
}