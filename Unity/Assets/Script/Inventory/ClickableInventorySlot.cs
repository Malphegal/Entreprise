using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableInventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private void OnMouseOver()
    {
        print(name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //if (eventData.button == PointerEventData.InputButton.Left && GetComponent<UnityEngine.UI.Image>().sprite != null)
        //    GetComponentInParent<Inventory>().ShowInventoryLeftClickPanel(int.Parse(name.Split('_')[1]), eventData.position);
        //else if (eventData.button == PointerEventData.InputButton.Right)
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");
        //GetComponentInParent<Inventory>().ShowInventoryLeftClickPanel(int.Parse(name.Split('_')[1]), eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");
        //GetComponentInParent<Inventory>().CloseInventoryLeftClickPanel();
    }
}