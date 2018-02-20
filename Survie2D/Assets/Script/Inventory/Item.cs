using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Use the Sprite of the gameObject which this is attached instead of a duplicated Sprite object ?
public class Item : MonoBehaviour {

    public enum TypeOfCollectableItem
    {
        Food,
        Resource,
        Rune
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

    public virtual void InitValues(Item item)
    {
        typeOfCollectableItem = item.typeOfCollectableItem;
        itemName =              item.itemName;
        ImageOfTheItem =        item.ImageOfTheItem;
        stackable =             item.stackable;
    }

    private void Awake()
    {
            // If the item is already attached to a GameObject which has a SpriteRenderer, assigned it to ImageOfTheImage

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            ImageOfTheItem = sr.sprite;
    }
}