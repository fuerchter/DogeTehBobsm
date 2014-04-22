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
            List<VertexPositionNormalTexture> vertexList = new List<VertexPositionNormalTexture>();
            Vector3[] boundCorners = bounds.GetCorners();
            Vector3[] normals = new Vector3[boundCorners.Length];

            short[] indexList ={
                                0, 1, 2,    0, 2, 3,
                                1, 5, 6,    1, 6, 2,
                                2, 6, 7,    2, 7, 3,
                                3, 7, 4,    3, 4, 0,
                                4, 6, 5,    4, 7, 6,
                                5, 0, 4,    5, 1, 0
                            }; //Noch nicht komplett richtige Indizierung
            iBuffer = new IndexBuffer(Game.GraphicsDevice, IndexElementSize.SixteenBits, indexList.Length, BufferUsage.WriteOnly);
            iBuffer.SetData(indexList);

            //Normale aus Positionen und Indices generieren?
            for (int i = 0; i < indexList.Length / 3; i++)
            {
                Vector3 indices=new Vector3(indexList[i * 3], indexList[i * 3 + 1], indexList[i * 3 + 2]);

                Vector3 first = boundCorners[(int)indices.X];
                Vector3 second = boundCorners[(int)indices.Y];
                Vector3 third = boundCorners[(int)indices.Z];

                Vector3 normal = Vector3.Cross(second - first, third - first);
                normals[(int)indices.X] = normal;
                normals[(int)indices.Y] = normal;
                normals[(int)indices.Z] = normal;
            }

            for (int i = 0; i < boundCorners.Length; i++)
            {
                //Console.WriteLine(boundCorners[i].ToString());
                vertexList.Add(new VertexPositionNormalTexture(boundCorners[i], normals[i], Vector2.Zero));
            }

            //Erstelle VertexBuffer aus VertexList
            vBuffer = new VertexBuffer(Game.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, vertexList.Count, BufferUsage.WriteOnly);
            vBuffer.SetData<VertexPositionNormalTexture>(vertexList.ToArray());

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
