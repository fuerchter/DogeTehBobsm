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
    public class CollisionSystem
    {
        public static void Update(GameTime gameTime, Collider collider1, Collider collider2)
        {
            if (collider1.GetBounds().Intersects(collider2.GetBounds()))
            {
                collider1.OnCollision(collider2);
                collider2.OnCollision(collider1);
            }
        }
    }
}
