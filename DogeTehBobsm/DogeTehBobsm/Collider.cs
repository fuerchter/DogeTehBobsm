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
    public class Collider : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected BoundingBox bounds;
        protected BasicEffect effect;

        public Collider(Game game, BoundingBox bounds)
            : base(game)
        {
            this.bounds = bounds;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public virtual void OnCollision(Collider collider)
        {

        }

        public void SetEffect(BasicEffect effect)
        {
            this.effect = effect;
        }

        public void SetCamera(Matrix view, Matrix projection)
        {
            effect.View = view;
            effect.Projection = projection;
        }
    }
}
