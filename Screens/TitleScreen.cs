using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace pong_csharp.Screens
{
    public class TitleScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;

        public TitleScreen(Game game) : base(game) {}

        public override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("textures/title-screen");
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = KeyboardExtended.GetState();

            if (keyboardState.WasKeyJustDown(Keys.Q))
                Game.Exit();

            if (keyboardState.WasKeyJustDown(Keys.Space))
                ScreenManager.LoadScreen(
                     new MainGameScreen(Game), 
                    new FadeTransition(GraphicsDevice, Color.Black, 0.5f)
                );
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            
            spriteBatch.End();
        }
    }
}