using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using pong_csharp.Screens;

namespace pong_csharp
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ScreenManager screenManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 480,
                SynchronizeWithVerticalRetrace = false
            };

            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;

            TargetElapsedTime = TimeSpan.FromSeconds(1f / 60f);
            screenManager = Components.Add<ScreenManager>();
        }

        protected override void LoadContent()
        {
             base.LoadContent();

            screenManager.LoadScreen(new TitleScreen(this), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
        }

    }
}
