using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace JnRGame
{
    public class Main : Game
    {
        enum GameState { Menu, Level }
        GameState gameState;
        GraphicsDeviceManager graphics;
        RenderTarget2D target;
        SpriteBatch targetBatch;
        SpriteBatch sb_tiles;
        SpriteBatch sb_actor;
        int screenWidth = 1280;
        int screenHeight = 720;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            GameContent.content = Content;
            GameContent.content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
        }

        protected override void Initialize()
        {
            base.Initialize();
            gameState = GameState.Level;
            World.BuildLevel("level0");
        }

        protected override void LoadContent()
        {
            target = new RenderTarget2D(GraphicsDevice, 320, 180);
            targetBatch = new SpriteBatch(GraphicsDevice);
            sb_tiles = new SpriteBatch(GraphicsDevice);
            sb_actor = new SpriteBatch(GraphicsDevice);
            GameContent.Load();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    MenuUpdate();
                    break;
                case GameState.Level:
                    LevelUpdate();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    MenuDraw();
                    break;
                case GameState.Level:
                    LevelDraw();
                    break;
            }

            base.Draw(gameTime);
        }

        void MenuUpdate()
        {

        }

        void LevelUpdate()
        {
            World.Update();
            foreach (Actor actor in World.actorList.ToList())
                actor.Update();
        }

        void MenuDraw()
        {

        }

        void LevelDraw()
        {
            GraphicsDevice.SetRenderTarget(target); // Set Render Target to "target"
            sb_tiles.Begin();
            World.Draw(sb_tiles);
            sb_tiles.End();
            sb_actor.Begin();
            foreach (Actor actor in World.actorList.ToList()) // Draw each Actor
                actor.Draw(sb_actor);
            sb_actor.End();
            GraphicsDevice.SetRenderTarget(null); // Set Render Target to null (Back Buffer)
            targetBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);
            targetBatch.Draw(target, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            targetBatch.End();
        }
    }
}
