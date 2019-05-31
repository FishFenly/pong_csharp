using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tweening;
using pong_csharp.Objects;

namespace pong_csharp.Screens
{
    public class MainGameScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private SpriteFont gameFont;
        private Ball ball;
        private Paddle playerPaddle;
        private Paddle aiPaddle;
        private int playerScore;
        private int aiScore;
        private Tweener tweener = new Tweener();
        private bool debug = false;
        private readonly FramesPerSecondCounter fpsCounter = new FramesPerSecondCounter();

        public int ScreenWidth => GraphicsDevice.Viewport.Width;
        public int ScreenHeight => GraphicsDevice.Viewport.Height;

        public MainGameScreen(Game game) : base(game){}

        public override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("fonts/PressStart2P");

            ball = new Ball()
            {
                Position = new Vector2(ScreenWidth / 2f, ScreenHeight / 2f),
                Sprite = new Sprite(Content.Load<Texture2D>("textures/ball")),
                Velocity = new Vector2(250, 200),
                Scale = new Vector2(3)
            };

            playerPaddle = new Paddle()
            {
                Position = new Vector2(ScreenWidth / 6f, ScreenHeight / 2f),
                Sprite = new Sprite(Content.Load<Texture2D>("textures/paddle")),
                Velocity = (int)5,
                Scale = new Vector2(5)
            };

            aiPaddle = new Paddle()
            {
                Position = new Vector2(ScreenWidth / 1.25f, ScreenHeight / 2f),
                Sprite = new Sprite(Content.Load<Texture2D>("textures/paddle")),
                Velocity = (int)5,
                Scale = new Vector2(5)
            };
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            spriteBatch.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            var elapsedSecs = gameTime.GetElapsedSeconds();
            var keyboardState = KeyboardExtended.GetState();

            if (keyboardState.WasKeyJustDown(Keys.Q))
            {
                ScreenManager.LoadScreen(
                     new TitleScreen(Game), 
                    new ExpandTransition(GraphicsDevice, Color.Black)
                );
            }

            if (keyboardState.WasKeyJustDown(Keys.D))
            {
                if (debug)
                {
                    debug = false;
                }
                else if (!debug)
                {
                    debug = true;
                }
            }

            ball.MoveBall(elapsedSecs, ScreenWidth, ScreenHeight);
            ball.CollisionCheck(aiPaddle);
            ball.CollisionCheck(playerPaddle);
            
            playerPaddle.MovePaddlePlayer(keyboardState, ScreenHeight);
            aiPaddle.MovePaddleAi(ball, elapsedSecs, 0.85f);

            if (ball.scored == "player")
            {
                playerScore++;
            }
            else if (ball.scored == "ai")
            {
                aiScore++;
            }
            ball.scored = "";

            fpsCounter.Update(gameTime);
            tweener.Update(elapsedSecs);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            
            spriteBatch.Draw(ball.Sprite, ball.Position, ball.Rotation, ball.Scale);
            spriteBatch.Draw(playerPaddle.Sprite, playerPaddle.Position, playerPaddle.Rotation, playerPaddle.Scale);
            spriteBatch.Draw(aiPaddle.Sprite, aiPaddle.Position, aiPaddle.Rotation, aiPaddle.Scale);
            spriteBatch.DrawString(
                gameFont, 
                "Score",
                new Vector2(ScreenWidth / 2.35f, ScreenHeight / 8f),
                Color.WhiteSmoke
            );
            spriteBatch.DrawString(
                gameFont, 
                playerScore + " | " + aiScore,
                new Vector2(ScreenWidth / 2.35f, ScreenHeight / 6f),
                Color.WhiteSmoke
            );
            fpsCounter.Draw(gameTime);

            if (debug)
            {
                spriteBatch.DrawString(
                    gameFont,
                    "FPS: " + fpsCounter.FramesPerSecond,
                    new Vector2(50, 50),
                    Color.WhiteSmoke
                );
                spriteBatch.DrawRectangle(playerPaddle.BoundingRectangle, Color.Red, 1);
                spriteBatch.DrawRectangle(aiPaddle.BoundingRectangle, Color.Red, 1);
                spriteBatch.DrawRectangle(ball.BoundingRectangle, Color.Red, 1);
            }

            spriteBatch.End();
        }
    }
}