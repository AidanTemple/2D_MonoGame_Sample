#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion

namespace _2D_MonoGame_Sample
{
    abstract class Sprite
    {
        #region Public Members

        // Represents a texture.
        protected Texture2D Texture;

        // A rectangle that specifies the source texels from a
        // texture. Use null to draw the entire texture.
        public Rectangle Source;

        // Represents the location at given screen coordinates 
        // to draw the sprite.
        public Vector2 Position;

        // Represents the sprites origin, by default (0, 0) which 
        // corresponds to the upper left corner of the sprite.
        public Vector2 Origin;

        // Used to tint a sprite, use Color.White for full
        // color with no tinting.
        public Color Color;

        // Used to apply effects to a sprite.
        public SpriteEffects Effects;

        // Specifies the angle (in radians) to rotate the sprite
        // about its origin.
        public Single Rotation;

        // Represents the scale of the sprite between 0 and 1. 
        // By default 1 represents the sprite at its original size.
        public Single Scale;

        // Represents the depth of a layer. By default, 0 represents
        // the front layer and 1 represents the back layer. Use SpriteSortMode 
        // if you want sprites to be sorted during drawing.
        public Single Depth;

        // Used to determine if the sprite should be drawn.
        public bool IsAlive;

        #endregion

        #region Initialisation

        /// <summary>
        /// Initialises a new Sprite
        /// </summary>
        public Sprite()
        {
            this.Texture = null;

            Source = new Rectangle(0, 0, 0, 0);

            Position = Vector2.Zero;
            Origin = Vector2.Zero;

            Color = Color.White;

            Effects = SpriteEffects.None;

            Rotation = 0f;
            Scale = 1f;
            Depth = 0f;

            IsAlive = false;
        }

        #endregion

        #region Update

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        public abstract void Update();

        #endregion

        #region Draw

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Source, Color, Rotation, Origin, Scale, Effects, Depth);
        }

        #endregion
    }
}