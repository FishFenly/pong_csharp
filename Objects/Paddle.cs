using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace pong_csharp.Objects
{
    public class Paddle : GameObject
    {
        public int Velocity;
        
        public void MovePaddlePlayer(KeyboardStateExtended keyState, int screenHeight)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                Position.Y -= Velocity;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                Position.Y += Velocity;
            }

            if (BoundingRectangle.Top < 0 )
            {
                Position.Y = BoundingRectangle.Height / 2f;
            }
            
            if (BoundingRectangle.Bottom > screenHeight)
            {
                Position.Y = screenHeight - BoundingRectangle.Height / 2f;
            }
        }

        public void MovePaddleAi(Ball ball, float elapsedSecs, float difficulty)
        {
            // change below to set difficulty
            var paddleSpeed = Math.Abs(ball.Velocity.Y) * difficulty;

            if (paddleSpeed < 0)
                paddleSpeed = -paddleSpeed;

            //ball moving down
            if (ball.Velocity.Y > 0)
            {
                if (ball.Position.Y > Position.Y)
                    Position.Y += paddleSpeed * elapsedSecs;
                else
                    Position.Y -= paddleSpeed * elapsedSecs;
            }

            //ball moving up
            if (ball.Velocity.Y < 0)
            {
                if (ball.Position.Y < Position.Y)
                    Position.Y -= paddleSpeed * elapsedSecs;
                else
                    Position.Y += paddleSpeed * elapsedSecs;
            }
        }
    }
}