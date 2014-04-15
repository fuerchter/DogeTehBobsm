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
        float winTimer; //TimeSpan verwenden?
        float bombTimer;
        Block[,] blocks;

        public Level(Game game)
            : base(game)
        {
            Vector3 blockScale = new Vector3(10, 10, 10);

            //Erstelle ein 5x5 Level mit Bloecken der Groesse 10x10x10
            blocks = new Block[5, 5];
            for (int z = 0; z <= blocks.GetUpperBound(0); z++)
            {
                for (int x = 0; x <= blocks.GetUpperBound(1); x++)
                {
                    blockScale.Y += 1;
                    blocks[z, x] = new Block(game, new BoundingBox(new Vector3(x*blockScale.X, 0, z*blockScale.Z),
                                                                    new Vector3(x*blockScale.X+blockScale.X, blockScale.Y, z*blockScale.Z+blockScale.Z)));
                    game.Components.Add(blocks[z, x]);
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
