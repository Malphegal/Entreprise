using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot {

    #region PROPERTIES

    public Item Item { get; set; }

    public bool IsEmpty { get { return Item == null; } }

    #endregion

}