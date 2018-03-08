using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    namespace Objects
    {
        public class Tool : Item
        {
                // ENUMS

            public enum Tools
            {
                Hammer,
                Hoe,
                Pickaxe,
                Axe,
                Hook
            }

                // FIELDS

            public Tools typeOfTool;
        }
    }
}