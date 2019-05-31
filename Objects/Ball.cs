using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace pong_csharp.Objects
{
    public class Ball : GameObject
    {
        public Vector2 Velocity;
        public string scored;
        private readonly FastRandom random = new FastRandom();

        public void MoveBall(float elapsedSecs, int screenWidth, int screenHeight)
        {
            Position += Velocity * elapsedSecs;

            var halfHeight = BoundingRectangle.Height / 2;
            var halfWidth = BoundingRectangle.Width / 2;

            if (Position.Y - halfHeight < 0)
            {
                Position.Y = halfHeight;
                Velocity.Y = -Velocity.Y;
            }

            if (Position.Y + halfHeight > screenHeight)
            {
                Position.Y = screenHeight - halfHeight;
                Velocity.Y = -Velocity.Y;
            }

            if (Position.X > screenWidth + halfWidth && Velocity.X > 0)
            {
                Position = new Vector2(screenWidth / 2f, screenHeight / 2f);
                Velocity = new Vector2(random.Next(2,5) * -100, 100);
                
                scored = "player";
            }

            if (Position.X < -halfWidth && Velocity.X < 0)
            {
                Position = new Vector2(screenWidth / 2f, screenHeight / 2f);
                Velocity = new Vector2(random.Next(2,5) * 100, 100);
                
                scored = "ai";
            }
        }

        public void CollisionCheck(Paddle paddle)
        {
            if (BoundingRectangle.Intersects(paddle.BoundingRectangle))
            {
                if (BoundingRectangle.Left < paddle.BoundingRectangle.Left)
                    Position.X = paddle.BoundingRectangle.Left - BoundingRectangle.Width / 2;

                if (BoundingRectangle.Right > paddle.BoundingRectangle.Right)
                    Position.X = paddle.BoundingRectangle.Right + BoundingRectangle.Width / 2;

                Velocity.X = -Velocity.X;
            }
        }
    }

}