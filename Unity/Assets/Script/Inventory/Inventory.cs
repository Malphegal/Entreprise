using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    #region FIELDS

    private static InventorySlot[] _myInventory;

    private static GameObject inventoryUI;

    private GameObject _leftClickPanel;
    private Text _itemNameLeftClick;
    private Image _itemImageLeftClick;

    #endregion

    #region METHODS

    private void Awake()
    {
        _leftClickPanel = gameObject._Find("inventory_LeftClick");
        _itemNameLeftClick = gameObject._Find("inventory_LeftClick_ItemName").GetComponent<Text>();
        _itemImageLeftClick = gameObject._Find("inventory_LeftClick_ItemImage").GetComponent<Image>();
    }

    public void CloseInventoryLeftClickPanel()
    {
        _leftClickPanel.SetActive(false);
    }

    public void ShowInventoryLeftClickPanel(int indexOfSlotClicked, Vector2 mousePosition)
    {
        _leftClickPanel.SetActive(true);

        RectTransform panelRectTransform = (RectTransform)_leftClickPanel.transform;
        print(panelRectTransform.localPosition);

        _itemNameLeftClick.text = Lang.GetString(_myInventory[indexOfSlotClicked].Item.itemName);
        _itemImageLeftClick.sprite = _myInventory[indexOfSlotClicked].Item._imageOfTheItem;
    }

    /*Vector2 ClampToWindow(PointerEventData data)
    {
        Vector2 rawPointerPosition = data.position;

        Vector3[] canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);

        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
        return newPointerPosition;
    }*/

    #endregion

    #region STATIC METHODS

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

            //Button btn = newSlot.AddComponent<Button>();
            //btn.onClick.AddListener(delegate { RightClickOnSlot(btn); });
            newSlot.AddComponent<ClickableInventorySlot>();
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

    #endregion
}