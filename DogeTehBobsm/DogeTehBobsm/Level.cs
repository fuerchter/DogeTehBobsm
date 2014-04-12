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
    public class Level : Microsoft.Xna.Framework.GameComponent
    {
        float winTimer_; //TimeSpan verwenden?
        float bombTimer_;
        Block[,] blocks_;

        public Level(Game game)
            : base(game)
        {
            //Erstelle ein 5x5 Level mit Bloecken der Groesse 10x10x10
            blocks_ = new Block[5, 5];
            for (int z = 0; z <= blocks_.GetUpperBound(0); z++)
            {
                for (int x = 0; x <= blocks_.GetUpperBound(1); x++)
                {
                    blocks_[z, x] = new Block(game, z, x, new Vector3(10, 10, 10));
                    game.Components.Add(blocks_[z, x]);
                }
            }
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
    }
}
