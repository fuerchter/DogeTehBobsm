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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : Microsoft.Xna.Framework.GameComponent
    {
        //View Informationen
        Vector3 position;
        Vector3 target;
        Vector3 upVector;

        Matrix view;
        Matrix projection;

        float speed;

        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            position = new Vector3(0, 5, 0);
            target = new Vector3(10, 5, 10);
            upVector = Vector3.Up;

            view = Matrix.CreateLookAt(position,target, upVector);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), Game.GraphicsDevice.Viewport.AspectRatio, 0.5f, 1000.0f);

            speed = 2;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            Vector3 movement=Vector3.Zero;
            Vector3 forward = target - position;
            Vector3 right = Vector3.Cross(forward, upVector);

            //Bewegung
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            {
                movement += forward;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                movement -= forward;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                movement += right;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                movement -= right;
            }

            movement.Y = 0;
            if (movement.Length() > 1)
            {
                movement.Normalize();
            }

            if (movement.Length() != 0)
            {
                movement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position += movement;
            }

            //Rotation
            MouseState mouse = Mouse.GetState();
            Vector3 rotation = Vector3.Zero;
            Vector2 halfRes = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f, Game.GraphicsDevice.Viewport.Height / 2.0f);
            rotation.Y = (halfRes.X - mouse.X) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotation.Z = (halfRes.Y - mouse.Y) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Mouse.SetPosition((int)halfRes.X, (int)halfRes.Y);

            Matrix cameraRotation=Matrix.CreateRotationY(rotation.Y)*Matrix.CreateRotationZ(rotation.Z);
            forward=Vector3.Transform(forward, cameraRotation);
            //upVector = Vector3.Transform(upVector, cameraRotation);
            
            target = forward + position;

            view = Matrix.CreateLookAt(position, target, upVector);

            base.Update(gameTime);
        }

        public Matrix GetView()
        {
            return view;
        }

        public Matrix GetProjection()
        {
            return projection;
        }
    }
}
