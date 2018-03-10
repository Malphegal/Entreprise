using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    namespace Objects
    {
        public class Rune : Item
        {
                // ENUMS

            public enum Color
            {
                None = 0,
                Earth,  // Yellow
                Fire,   // Red
                Water,  // Blue
                Wind    // White
            }

                // FIELDS

            public Color _color;
        }
    }
}