using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

        // PROPERTIES
        
    public int NumberOfSameItem { get; set; }

    public bool IsEmpty { get { return NumberOfSameItem == 0; } }

    private bool _isSelected;

        // METHODS

    public void ChangeSelectedValue()
    {
        UnityEngine.UI.Outline outline = transform.parent.GetComponent<UnityEngine.UI.Outline>();
        if (_isSelected ^= true)
        {
            outline.effectColor = Color.red;
            outline.effectDistance = new Vector2(6, 6);
        }
        else
        {
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(3, 3);
        }
    }
}