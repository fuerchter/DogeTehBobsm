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
    public class FootCollider : Collider
    {
        bool collision;

        public FootCollider(Game game, BoundingBox bounds)
            : base(game, bounds)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            collision = false;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnCollision(Collider collider)
        {
            if (collider.GetType() == typeof(Block))
            {
                //Console.WriteLine("DOOOOOOOOOOOOOOOR");
                collision = true;
            }
        }

        public bool DidCollide()
        {
            bool res = collision;
            collision = false;
            return res;
        }
    }
}
