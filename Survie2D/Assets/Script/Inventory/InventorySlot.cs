using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

    #region PROPERTIES

    public Item Item { get; set; }

    public int NumberOfSameItem { get; set; }

    public bool IsEmpty { get { return NumberOfSameItem == 0; } }

    public bool Selected { get; set; }

    #endregion

    #region METHODS

    public void ChangeSelectedValue()
    {
        UnityEngine.UI.Outline outline = transform.parent.GetComponent<UnityEngine.UI.Outline>();
        if (Selected ^= true)
        {
            outline.effectColor = Color.red;
            outline.effectDistance = new Vector2(6, 6);
            InventoryOptions.MoveInventoryOptionWindow(int.Parse(gameObject.name[gameObject.name.Length - 1].ToString()));
        }
        else
        {
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(3, 3);
        }
    }

    #endregion
}