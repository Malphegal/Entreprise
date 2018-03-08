using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    namespace Objects
    {
        public class Food : Item
        {
                // FIELDS

            public int hungerValue;
            public int thirstValue;

                // METHODS

            /* Copy all variable from itemToCopyFrom to this instance */
            public override void InitValues(Item itemToCopyFrom)
            {
                base.InitValues(itemToCopyFrom);
                Food foreignFood = ((Food)itemToCopyFrom);
                hungerValue = foreignFood.hungerValue;
                thirstValue = foreignFood.thirstValue;
            }
        }
    }
}