using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private static InventorySlot[] _myInventory;

    private static GameObject inventoryUI;

    /* Creates the inventory. Called from the initClass. */
    public static void InitInventory(int inventorySize)
    {
        inventoryUI = GameObject.Find("--------------- UI ---------------")._Find("itemList");

        _myInventory = new InventorySlot[inventorySize];
        for (int i = 0; i < inventorySize; i++)
            _myInventory[i] = new InventorySlot();

        for (int i = 0; i < inventorySize; i++)
        {
            GameObject newSlot = new GameObject("inventorySlot_" + i);
            Image img = newSlot.AddComponent<Image>();
            img.color = Color.gray;
            Outline outline = newSlot.AddComponent<Outline>();
            outline.effectDistance = new Vector2(3, 3);
            // TODO: Default image for an empty slot
            //img.sprite = Resources.Load<Sprite>("apple");
            newSlot.transform.SetParent(inventoryUI.transform);
            ((RectTransform)newSlot.transform).localScale = new Vector3(1, 1, 1);

            Button btn = newSlot.AddComponent<Button>();
            btn.onClick.AddListener(delegate { RightClickOnSlot(btn); });
        }
    }

    // TODO: Some items can be stacked !
    public static bool AddItem(Item itemToAdd)
    {
        GameObject goInventory = GameObject.Find("--------------- UI ---------------")._Find("itemList");
        for (int i = 0; i < _myInventory.Length; i++)
        {
            if (_myInventory[i].IsEmpty)
            {
                _myInventory[i].Item = itemToAdd;
                _myInventory[i].Item.InInventory = true;
                Image img = goInventory._Find("inventorySlot_" + i).GetComponent<Image>();
                img.sprite = itemToAdd._imageOfTheItem;
                img.color = Color.white;
                return true;
            }
        }
        return false;
    }

    // TODO: Some items can be stacked
    public static void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < _myInventory.Length; i++)
        {
            if (!_myInventory[i].IsEmpty && _myInventory[i].Item == itemToRemove)
            {
                _myInventory[i].Item.StopAllCoroutines();
                _myInventory[i].Item = null;
                Image img = inventoryUI._Find("inventorySlot_" + i).GetComponent<Image>();
                img.sprite = null;
                img.color = Color.gray;
                return;
            }
        }
    }

    // TODO: drop an item and create the proper 3D model 
    public static void DropItem(Item itemToRemove)
    {
        // InInventory = false;
        throw new System.NotImplementedException();
    }

    // TODO: Remove it and change it to OnPointerClick(...)
    public static void RightClickOnSlot(Button btn)
    {
        print(_myInventory[int.Parse(btn.gameObject.name.Split('_')[1])].Item);
        RemoveItem(_myInventory[int.Parse(btn.gameObject.name.Split('_')[1])].Item);
    }

    /*
     public class ClickableObject : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                Debug.Log("Left click");
            else if (eventData.button == PointerEventData.InputButton.Middle)
                Debug.Log("Middle click");
            else if (eventData.button == PointerEventData.InputButton.Right)
                Debug.Log("Right click");
        }
    }
     */
}
