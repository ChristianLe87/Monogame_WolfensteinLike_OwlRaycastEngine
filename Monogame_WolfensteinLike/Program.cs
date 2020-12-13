using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_WolfensteinLike
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }

    // ============================================================
    // Project copy from: https://github.com/Owlzy/OwlRaycastEngine
    // ============================================================
    public class Game1 : Game
    {
        //--create slicer and declare slices--//
        TextureHandler slicer;
        Rectangle[] slices;

        //--viewport and width / height--//
        int viewportWidth;
        int viewportHeight;

        //--define camera--//
        private Camera camera;

        //--graphics manager and sprite batch--//
        public static GraphicsDeviceManager graphicsDeviceManager;
        public static ContentManager contentManager;
        SpriteBatch spriteBatch;

        Texture2D[] textures = new Texture2D[5];

        //--test texture--//
        Texture2D floor;
        Texture2D sky;

        //--array of levels, levels reffer to "floors" of the world--//
        Level[] levels;


        public Game1()
        {
            // Content
            string absolutePath = Path.Combine(Environment.CurrentDirectory, "Content");
            this.Content.RootDirectory = absolutePath;
            contentManager = this.Content;


            // Window
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
            graphicsDeviceManager.PreferredBackBufferWidth = 1024;
            graphicsDeviceManager.PreferredBackBufferHeight = 700;
            graphicsDeviceManager.ApplyChanges();

            spriteBatch = new SpriteBatch(GraphicsDevice);


            // FPS
            this.IsFixedTimeStep = true;
            int fps = 60;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / fps);
        }


        protected override void Initialize()
        {
            //--get viewport--//
            var view = graphicsDeviceManager.GraphicsDevice.Viewport;

            //--set view width and height--//
            viewportWidth = view.Bounds.Width;
            viewportHeight = view.Bounds.Height;

            //--init texture slices--//
            slicer = new TextureHandler(WK.texSize);
            slices = slicer.getSlices();

            //--inits the levels--//
            levels = Tools.CreateLevels(4, viewportWidth, viewportHeight);

            //--init camera--//
            camera = new Camera(viewportWidth, viewportHeight, WK.texSize, slices, levels);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            textures[0] = Tools.GetTexture("stone", "Textures");
            textures[1] = Tools.GetTexture("left_bot_house", "Textures");
            textures[2] = Tools.GetTexture("right_bot_house", "Textures");
            textures[3] = Tools.GetTexture("left_top_house", "Textures");
            textures[4] = Tools.GetTexture("right_top_house", "Textures");

            floor = Tools.GetTexture("floor", "Textures");
            sky = Tools.GetTexture("sky", "Textures");
        }


        protected override void UnloadContent()
        {
            // TODO: Code
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            camera.update();
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // Draw
            {
                //--draw sky and floor--//
                spriteBatch.Draw(floor, new Rectangle(0, (int)(viewportHeight * 0.5f), viewportWidth, (int)(viewportHeight * 0.5f)), new Rectangle(0, 0, WK.texSize, WK.texSize), Color.White);
                spriteBatch.Draw(sky, new Rectangle(0, 0, viewportWidth, (int)(viewportHeight * 0.5f)), new Rectangle(0, 0, WK.texSize, WK.texSize), Color.White);

                //--draw walls--//
                for (int x = 0; x < viewportWidth; x++)
                    for (int i = levels.Length - 1; i >= 0; i--)
                        spriteBatch.Draw(textures[levels[i].currTexNum[x]], levels[i].sv[x], levels[i].cts[x], levels[i].st[x]);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
