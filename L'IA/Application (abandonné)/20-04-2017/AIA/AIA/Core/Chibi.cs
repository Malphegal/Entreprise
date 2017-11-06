using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIA.Game1;

namespace AIA.Core
{
    public class Chibi : AnimatedObject
    {
        // FIELDS
        private int _vitesse;
        private directions _collidedDirection;

        // CONSTRUCTOR
        public Chibi(int totalAnimationFrames, int frameWidth, int frameHeight)
            : base(totalAnimationFrames, frameWidth, frameHeight)
        {
            _vitesse = 3;
            _collidedDirection = directions.FACE;
        }

        // METHODS
        public void seDéplacer (directions direction)
        {
            if (!Collision.Collided(direction))   // si le perso n'est pas en collision
            this.Position = Vector2.Add(this.Position, new Vector2((float)direction*_vitesse, 0));

            switch (direction)
            {
                case directions.DROITE:
                    _ligne = 1;
                    break;
                case directions.GAUCHE:
                    _ligne = 2;
                    break;
                case directions.FACE:
                    _ligne = 0;
                    break;
            }
        }

        public void sauter ()
        {
           this.Position = Vector2.Add(this.Position, new Vector2(0, -3));
        }

        public void allerÀ (Vector2 coordonnees) // aller aux coordonnées données
        {

        }

    }
}
