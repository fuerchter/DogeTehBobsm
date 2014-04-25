using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DogeTehBobsm
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicEffect defaultEffect;

        float aspectRatio;
        Matrix viewMatrix;
        Matrix projectionMatrix;


        Player player;
        float health = 20;
        private Model Bomb;
        float time = 5;
        public static SpriteFont font;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Components.Add(new Level(this));
            player = new Player(this);
            Components.Add(player);
            defaultEffect = new BasicEffect(GraphicsDevice);
            defaultEffect.EnableDefaultLighting();
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
            font = Content.Load<SpriteFont>("HUDFont");
            Bomb = Content.Load<Model>("bomb");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            //if (Bomb.Intersects(player.))
            //{

            //    health -= 2;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            //Ueberpruefe Kollision von jedem Collider mit jedem anderem Collider
            for (int i = 0; i < Components.Count; i++)
            {
                for (int j = i; j < Components.Count; j++)
                {
                    if (i != j)
                    {
                        if (Components[i].GetType().IsSubclassOf(typeof(Collider)) && Components[j].GetType().IsSubclassOf(typeof(Collider)))
                        {
                            CollisionSystem.Update(gameTime, (Collider)Components[i], (Collider)Components[j]);
                        }
                    }
                }

                if (Components[i].GetType().IsSubclassOf(typeof(Collider)))
                {
                    Collider currentCollider = (Collider)Components[i];
                    currentCollider.SetEffect(defaultEffect);
                    currentCollider.SetCamera(player.GetView(), player.GetProjection());
                }
            }

            if (time > 0.0f)
            {
                time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            viewMatrix = player.GetView();
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), 800.0f / 600.0f, 0.5f, 1000.0f);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix[] transforms = new Matrix[Bomb.Bones.Count];
            Bomb.CopyAbsoluteBoneTransformsTo(transforms);
            // Draw- model
            foreach (ModelMesh mesh in Bomb.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();  // Beleuchtung aktivieren
                    effect.World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(2, 17, 10);
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();

                spriteBatch.Begin();
                spriteBatch.DrawString(Game1.font, "Health: " + health, Vector2.Zero, Color.Firebrick);
                spriteBatch.DrawString(Game1.font, "Time left: " + (int)time, new Vector2(200, 0), Color.Firebrick);
                if (time <= 0.0f)
                {
                    spriteBatch.DrawString(Game1.font, "WIN GET", new Vector2(0, 200), Color.Firebrick);
                }
                spriteBatch.End();

                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

                base.Draw(gameTime);
            }
        }
    }
}
