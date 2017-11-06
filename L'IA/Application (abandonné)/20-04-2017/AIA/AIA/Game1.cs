using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AIA.Core;

namespace AIA
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 480;
        World world;
        Chibi chibi;

        // Énumération des directions
        public enum directions
        {
            GAUCHE = -1,
            DROITE = 1,
            FACE = 0
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.IsFullScreen = false;
            this.Window.AllowUserResizing = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            chibi = new Chibi(2, 150, 300);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            chibi.Texture = Content.Load<Texture2D>("spriteSheet");
            chibi.Position = new Vector2(WINDOW_WIDTH/2-chibi.frameWidth/2, WINDOW_HEIGHT-chibi.frameHeight);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Pour quitter l'application
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Gestion clavier du déplacement
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                chibi.seDéplacer(directions.DROITE);
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                chibi.seDéplacer(directions.GAUCHE);
            else
                chibi.seDéplacer(directions.FACE);


            // Gestion clavier du saut
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
               chibi.sauter();
            else if (chibi.Position.Y != WINDOW_HEIGHT - chibi.frameHeight)
                chibi.Position = new Vector2(chibi.Position.X, WINDOW_HEIGHT - chibi.frameHeight);
            

            // TODO: Add your update logic here
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            chibi.UpdateFrame(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            chibi.DrawAnimation(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
