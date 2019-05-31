using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace pong_csharp.Objects
{
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Scale = Vector2.One;
        public float Rotation;
        public Sprite Sprite;
        public RectangleF BoundingRectangle => Sprite.GetBoundingRectangle(Position, 0, Scale);
    }
}