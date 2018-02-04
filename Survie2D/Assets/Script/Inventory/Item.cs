using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Use the Sprite of the gameObject which this is attached instead of a duplicated Sprite object ?
public class Item : MonoBehaviour {

    public enum TypeOfCollectableItem
    {
        Food,
        Resource
        //None,
        //Weapon,
        //RangedWeapon,
        //Shield,
        //Hammer,
        //Pickaxe,
        //Axe,
        //Hoe,
        //Hook,
        //Clothes,
    }

        // FIELDS

    public TypeOfCollectableItem typeOfCollectableItem;

    public string itemName;

    public Sprite imageOfTheItem;

    public bool stackable;

        // METHODS

    public virtual void InitValues(Item item)
    {
        typeOfCollectableItem = item.typeOfCollectableItem;
        itemName =              item.itemName;
        imageOfTheItem =        item.imageOfTheItem;
        stackable =             item.stackable;
    }
}