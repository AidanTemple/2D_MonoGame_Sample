using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _2D_MonoGame_Sample
{
    static class ContentManager
    {
        public static Texture2D Background { get; private set; }
        public static Texture2D Cannon { get; private set; }
        public static Texture2D CannonBall { get; private set; }
        public static Texture2D Enemy { get; private set; }

        public static void Load(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            Background = content.Load<Texture2D>("Textures/background");
            Cannon = content.Load<Texture2D>("Textures/cannon");
            CannonBall = content.Load<Texture2D>("Textures/cannonball");
            Enemy = content.Load<Texture2D>("Textures/enemy");
        }
    }
}
