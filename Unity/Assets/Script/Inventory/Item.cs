using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum TypeOfCollectableItem
    {
        EdibleFood
    }

    #region FIELDS

    public TypeOfCollectableItem typeOfCollectableItem;

    public string itemName;

    public Sprite _imageOfTheItem;

    #endregion

    #region PROPERTIES

    public bool InInventory { get; set; }

    #endregion

    #region METHODS

    // DEBUG : Remove it later
    public override string ToString()
    {
        return Lang.GetString(itemName) + " : " + typeOfCollectableItem;
    }

    #endregion
}