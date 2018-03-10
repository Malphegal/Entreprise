using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    namespace Objects
    {
        public class Item : MonoBehaviour
        {
                // ENUMS

            public enum TypeOfCollectableItem
            {
                Food,
                Resource,
                Rune,
                Tool
                //None,
                //Weapon,
                //RangedWeapon,
                //Shield,
                //Clothes,

                // Tools

                //Hammer,
                //Pickaxe,
                //Axe,
                //Hoe,
                //Hook,
            }

                // FIELDS

            public TypeOfCollectableItem typeOfCollectableItem;

            public string itemName;

            public Sprite ImageOfTheItem { get; private set; }

            public bool stackable;

                // METHODS

            /* Copy all variable from itemToCopyFrom to this instance */
            public virtual void InitValues(Item itemToCopyFrom)
            {
                typeOfCollectableItem = itemToCopyFrom.typeOfCollectableItem;
                itemName = itemToCopyFrom.itemName;
                ImageOfTheItem = itemToCopyFrom.ImageOfTheItem;
                stackable = itemToCopyFrom.stackable;
            }

            /* Tries to assign the sprite contained in gameObject to ImageOfTheItem */
            private void Awake()
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                    ImageOfTheItem = sr.sprite;
            }
        }
    }
}