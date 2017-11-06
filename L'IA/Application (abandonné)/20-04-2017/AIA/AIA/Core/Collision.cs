using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIA.Game1;

namespace AIA.Core
{
    public static class Collision
    {
        // À mettre dans la classe GameObject ou une classe Pixel car est la passabilité d'un élément
        public enum Passabilité
        {
            FALSE = 0,
            TRUE = 1
        }

        /*private static int getPassabilité ()
        {

        }

        // Revoie true si le personnage est en collision sur la droite
        private static bool CollidedRight()
        {

        }

        // Renvoie true si le personnage est en collision sur la gauche
        private static bool CollidedLeft()
        {

        }*/

        // Permet de déterminer si le personnage est en collision pour la direction passéeen argument
        public static bool Collided(directions direction)
        {
            /*switch (direction)
            {
                case directions.DROITE:
                    return CollidedRight();
                case directions.GAUCHE:
                    return CollidedLeft();
                default:
                    return false;
            }*/
            return false;
        }
    }
}
