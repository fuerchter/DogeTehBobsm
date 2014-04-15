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
    public class Block : Collider
    {
        bool exists;
        VertexBuffer vBuffer;
        IndexBuffer iBuffer;

        public Block(Game game, BoundingBox bounds)
            : base(game, bounds)
        {
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            exists = true;

            //Erstelle VertexList aus BoundingBox
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();
            Vector3[] boundCorners = bounds.GetCorners();
            for (int i = 0; i < boundCorners.Length; i++)
            {
                //Console.WriteLine(boundCorners[i].ToString());
                vertexList.Add(new VertexPositionColor(boundCorners[i], new Color(255, 255, 255)));
            }

            //Erstelle VertexBuffer aus VertexList
            vBuffer = new VertexBuffer(Game.GraphicsDevice, VertexPositionColor.VertexDeclaration, vertexList.Count, BufferUsage.WriteOnly);
            vBuffer.SetData<VertexPositionColor>(vertexList.ToArray());

            short[] indexList={
                                0, 1, 2,    0, 2, 3,
                                1, 5, 6,    1, 6, 2,
                                2, 6, 7,    2, 7, 3,
                                3, 7, 4,    3, 4, 0,
                                4, 6, 5,    4, 7, 6,
                                5, 0, 4,    5, 1, 0
                            }; //Noch nicht komplett richtige Indizierung
            iBuffer=new IndexBuffer(Game.GraphicsDevice, IndexElementSize.SixteenBits, indexList.Length, BufferUsage.WriteOnly);
            iBuffer.SetData(indexList);

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

        public override void Draw(GameTime gameTime) //Kamera/Viewdaten muessen mitgegeben werden
        {
            effect.VertexColorEnabled = true;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game.GraphicsDevice.SetVertexBuffer(vBuffer);
                Game.GraphicsDevice.Indices = iBuffer;
                Game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vBuffer.VertexCount, 0, iBuffer.IndexCount / 3);
            }
            base.Draw(gameTime);
        }

        public override void OnCollision(Collider collider)
        {
            base.OnCollision(collider);
        }
    }
}
