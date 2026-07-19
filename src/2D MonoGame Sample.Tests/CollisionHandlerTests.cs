using Microsoft.Xna.Framework;
using Xunit;

namespace _2D_MonoGame_Sample.Tests
{
    public class CollisionHandlerTests
    {
        [Fact]
        public void Intersects_OverlappingRectangles_ReturnsTrue()
        {
            var rectA = new Rectangle(0, 0, 10, 10);
            var rectB = new Rectangle(5, 5, 10, 10);

            Assert.True(CollisionHandler.Intersects(rectA, rectB));
        }

        [Fact]
        public void Intersects_IsSymmetric()
        {
            var rectA = new Rectangle(0, 0, 10, 10);
            var rectB = new Rectangle(5, 5, 10, 10);

            Assert.Equal(CollisionHandler.Intersects(rectA, rectB), CollisionHandler.Intersects(rectB, rectA));
        }

        [Fact]
        public void Intersects_DisjointRectangles_ReturnsFalse()
        {
            var rectA = new Rectangle(0, 0, 10, 10);
            var rectB = new Rectangle(100, 100, 10, 10);

            Assert.False(CollisionHandler.Intersects(rectA, rectB));
        }

        [Fact]
        public void Intersects_TouchingEdgesOnly_ReturnsFalse()
        {
            // rectB starts exactly where rectA ends, no actual pixel overlap.
            var rectA = new Rectangle(0, 0, 10, 10);
            var rectB = new Rectangle(10, 0, 10, 10);

            Assert.False(CollisionHandler.Intersects(rectA, rectB));
        }

        [Fact]
        public void Intersects_OneRectangleFullyInsideAnother_ReturnsTrue()
        {
            var outer = new Rectangle(0, 0, 20, 20);
            var inner = new Rectangle(5, 5, 5, 5);

            Assert.True(CollisionHandler.Intersects(outer, inner));
        }

        [Fact]
        public void Intersects_IdenticalRectangles_ReturnsTrue()
        {
            var rectA = new Rectangle(3, 3, 8, 8);
            var rectB = new Rectangle(3, 3, 8, 8);

            Assert.True(CollisionHandler.Intersects(rectA, rectB));
        }

        [Fact]
        public void PerPixelIntersect_OverlappingOpaquePixels_ReturnsTrue()
        {
            var rectA = new Rectangle(0, 0, 4, 4);
            var rectB = new Rectangle(2, 2, 4, 4);

            var dataA = Opaque(4, 4);
            var dataB = Opaque(4, 4);

            Assert.True(CollisionHandler.PerPixelIntersect(rectA, dataA, rectB, dataB));
        }

        [Fact]
        public void PerPixelIntersect_OverlappingBoundsButFullyTransparentPixels_ReturnsFalse()
        {
            var rectA = new Rectangle(0, 0, 4, 4);
            var rectB = new Rectangle(2, 2, 4, 4);

            var dataA = Opaque(4, 4);
            var dataB = Transparent(4, 4);

            Assert.False(CollisionHandler.PerPixelIntersect(rectA, dataA, rectB, dataB));
        }

        [Fact]
        public void PerPixelIntersect_NonOverlappingBounds_ReturnsFalse()
        {
            var rectA = new Rectangle(0, 0, 4, 4);
            var rectB = new Rectangle(100, 100, 4, 4);

            var dataA = Opaque(4, 4);
            var dataB = Opaque(4, 4);

            Assert.False(CollisionHandler.PerPixelIntersect(rectA, dataA, rectB, dataB));
        }

        [Fact]
        public void PerPixelIntersect_OnlyOverlappingPixelsAreOpaque_ReturnsTrue()
        {
            // 4x4 sprites overlapping in the bottom-right 2x2 of A / top-left 2x2 of B.
            // Make every pixel transparent except the one pixel that actually overlaps.
            var rectA = new Rectangle(0, 0, 4, 4);
            var rectB = new Rectangle(2, 2, 4, 4);

            var dataA = Transparent(4, 4);
            dataA[3 + 3 * 4] = Color.White; // (3,3) in A's local space == world (3,3), inside overlap region

            var dataB = Transparent(4, 4);
            dataB[1 + 1 * 4] = Color.White; // (1,1) in B's local space == world (3,3), same overlap pixel

            Assert.True(CollisionHandler.PerPixelIntersect(rectA, dataA, rectB, dataB));
        }

        private static Color[] Opaque(int width, int height)
        {
            var data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }
            return data;
        }

        private static Color[] Transparent(int width, int height)
        {
            var data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }
            return data;
        }
    }
}
