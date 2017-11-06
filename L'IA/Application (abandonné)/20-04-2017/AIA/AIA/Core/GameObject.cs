using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AIA.Core
{
    public class GameObject
    {
        // FIELDS
        public Vector2 Position;
        public Texture2D Texture;
        public World world;
        protected Collision.Passabilité passabilité;
      
        // CONSTRUCTOR
        public GameObject ()
        {
            
        }

        public GameObject (World world)
        {
            this.world = world;
        }

        // METHODS
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        // Renvoie la passabilité de l'objet (s'il peut être traverser ou non)
        public Collision.Passabilité getPassabilité ()
        {
            return this.passabilité;
        }

        public void Gravity()
        {

        }

        


    }
}
