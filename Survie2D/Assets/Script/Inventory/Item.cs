using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum TypeOfCollectableItem
    {
        None,
        Food,
        Weapon,
        Clothes,
        Resource
    }

    #region FIELDS

    public TypeOfCollectableItem typeOfCollectableItem;

    public string itemName;

    public Sprite imageOfTheItem;

    public bool stackable;

    #endregion

    #region METHODS

    public override string ToString()
    {
        return Lang.GetString(itemName);
    }

    #endregion
}