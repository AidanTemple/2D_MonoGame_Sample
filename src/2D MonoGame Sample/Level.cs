#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion

namespace _2D_MonoGame_Sample
{
    class Level
    {
        #region Constants

        private const int m_MaxEnemies = 3;

        #endregion

        #region Private Members

        private Cannon m_Cannon;

        private Enemy[] m_Enemies;

        private Random m_Random;

        private Rectangle m_Viewport;

        #endregion

        #region Initialisation

        public Level(GraphicsDevice device)
        {
            m_Cannon = new Cannon(device);

            m_Enemies = new Enemy[m_MaxEnemies];

            for (int i = 0; i < m_MaxEnemies; i++)
            {
                m_Enemies[i] = new Enemy();
            }

            m_Random = new Random();

            m_Viewport = new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height);
        }

        #endregion

        #region Update

        public void Update()
        {
            m_Cannon.Update();

            UpdateEnemies();

            HandleCollisions();
        }

        private void UpdateEnemies()
        {
            foreach (Enemy enemy in m_Enemies)
            {
                if (enemy.IsAlive)
                {
                    enemy.Position += enemy.Velocity;

                    if (!m_Viewport.Contains(new Point((int)enemy.Position.X, (int)enemy.Position.Y)))
                    {
                        enemy.IsAlive = false;
                    }
                }
                else
                {
                    enemy.IsAlive = true;

                    enemy.Position = new Vector2(m_Viewport.Right, MathHelper.Lerp(
                        (float)m_Viewport.Height * 0.5f,
                        (float)m_Viewport.Height * 0.1f,
                        (float)m_Random.NextDouble()));

                    enemy.Velocity = new Vector2(MathHelper.Lerp(-1.0f, -5.0f, (float)m_Random.NextDouble()), 0);
                }
            }
        }

        private void HandleCollisions()
        {
            foreach (CannonBall ball in m_Cannon.CannonBalls)
            {
                foreach (Enemy enemy in m_Enemies)
                {
                    enemy.Source = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height);
                    ball.Source = new Rectangle((int)ball.Position.X, (int)ball.Position.Y, ball.Width, ball.Height);                   

                    if (ball.IsAlive)
                    {
                        if (CollisionHandler.Intersects(enemy.Source, ball.Source))
                        {
                            enemy.IsAlive = false;
                            ball.IsAlive = false;

                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.Background, Vector2.Zero, Color.White);

            m_Cannon.Draw(spriteBatch);

            foreach(Enemy enemy in m_Enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        #endregion
    }
}