using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Add all Text in an array, aswell as the Sprite
//       No need to use GetComponent at runtime anymore
// TODO: Unselect the inventory if an enemy hits the player
public class Inventory : MonoBehaviour {

        // FIELDS

    private const int c_inventorySize = 10;
    private const int c_maxStackSize = 99;
    private const string c_nameOfInventorySlot = "inventorySlot";

    private static InventorySlot[] _myInventory;

    private static GameObject _inventoryUI;

    public static int currentInventorySelected = 0;

        // PROPERTIES

    public static Item.TypeOfCollectableItem WhichItemInCurrentSelectedSlot { get { return _myInventory[currentInventorySelected].Item.typeOfCollectableItem; } }

        // METHODS

    private void Awake()
    {
        InitInventory();
    }

    /* Creates the inventory, called from the initClass */
    public static void InitInventory()
    {
        _inventoryUI = GameObject.Find("----------- HUD -----------")._Find("inventory");
        _myInventory = new InventorySlot[c_inventorySize];

        for (int i = 0; i < c_inventorySize; i++)
        {
            _myInventory[i] = _inventoryUI._Find(c_nameOfInventorySlot + i).GetComponent<InventorySlot>();
            _myInventory[i].NumberOfSameItem = 0;
        }
    }

    /* Return true if the item has been added */
    public static bool AddItem(Item itemToAdd)
    {
        int indexItemToAdd = ItemExistsAndStackableInInventory(itemToAdd);

            // If the item doesn't exists or there is a stack with 99 items

        if (indexItemToAdd == -1)
        {
            for (int i = 0; i < _myInventory.Length; i++)
                if (_myInventory[i].IsEmpty)
                {
                    _myInventory[i].Item = itemToAdd;
                    _myInventory[i].NumberOfSameItem = 1;

                    GameObject inventorySlotIndexI = _inventoryUI._Find(c_nameOfInventorySlot + i);
                    inventorySlotIndexI.GetComponent<Image>().sprite = itemToAdd.imageOfTheItem;

                    inventorySlotIndexI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "1";

                        // Display the counter if the item is stackable

                    if (itemToAdd.stackable)
                        inventorySlotIndexI.transform.GetChild(0).gameObject.SetActive(true);

                    return true;
                }

                // If there is no more slot left

            return false;
        }

            // If the item exists and one can be added

        Text valueToIncrement = _inventoryUI._Find(c_nameOfInventorySlot + indexItemToAdd).GetComponentInChildren<Text>();
        valueToIncrement.text = (int.Parse(valueToIncrement.text) + 1).ToString();
        _myInventory[indexItemToAdd].NumberOfSameItem++;

        return true;
    }

    /* If itemToCheck exists in inventory and stack value is lower than maxStackSize, return its index, otherwise return -1 */
    public static int ItemExistsAndStackableInInventory(Item itemToCheck)
    {
        for (int i = 0; i < c_inventorySize; i++)
            if (!_myInventory[i].IsEmpty && _myInventory[i].Item.itemName == itemToCheck.itemName)
                if (_myInventory[i].Item.stackable && int.Parse(_inventoryUI._Find(c_nameOfInventorySlot + i).GetComponentInChildren<Text>().text) < c_maxStackSize)
                    return i;
        return -1;
    }

    /* Remove one item of a inventorySlot at index indexToRemove */
    public static bool RemoveItem(int indexToRemove)
    {
        Text text = _inventoryUI._Find(c_nameOfInventorySlot + indexToRemove).transform.GetChild(0).GetChild(0).GetComponent<Text>();
        if (int.Parse(text.text) == 1)
        {
            RemoveStack(indexToRemove);
            return false;
        }
        text.text = (int.Parse(text.text) - 1).ToString();
        _myInventory[indexToRemove].NumberOfSameItem--;
        return true;
    }

    /* Remove the whole stack at index indexToRemove */
    public static void RemoveStack(int indexToRemove)
    {
        _myInventory[indexToRemove].NumberOfSameItem = 0;
        _myInventory[indexToRemove].Item = null;

        InventorySlot inventorySlot = _inventoryUI._Find(c_nameOfInventorySlot + indexToRemove).GetComponent<InventorySlot>();
        Destroy(inventorySlot.Item);
        inventorySlot.Item = null;
        inventorySlot.GetComponent<Image>().sprite = null;
        inventorySlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
        inventorySlot.transform.GetChild(0).gameObject.SetActive(false);
    }

        // TODO: Drop an item and create the proper 2D sprite
    public static void DropItem(int indexToDrop)
    {
        // InInventory = false;
        throw new System.NotImplementedException();
    }

        // TODO: Drop the whole stack at index indexToDrop
    public static void DropStack(int indexToDrop)
    {
        throw new System.NotImplementedException();
    }

        // TODO: Display a window
    public static void SelectOneSlot(int indexOfSlot)
    {
        print("I am the slot number " + indexOfSlot + " !");
        print("My gameObject name is " + _inventoryUI._Find(c_nameOfInventorySlot + indexOfSlot).name + " !");
    }

    public static void HighlightSlots()
    {
        _inventoryUI._Find(c_nameOfInventorySlot + currentInventorySelected).GetComponent<InventorySlot>().ChangeSelectedValue();
    }

    public static void HighlightNextSlot()
    {
            // Unselect the current InventorySlot selected first

        _inventoryUI._Find(c_nameOfInventorySlot + currentInventorySelected).GetComponent<InventorySlot>().ChangeSelectedValue();

        if (currentInventorySelected == c_inventorySize - 1)
            currentInventorySelected = 0;
        else
            currentInventorySelected++;

        _inventoryUI._Find(c_nameOfInventorySlot + currentInventorySelected).GetComponent<InventorySlot>().ChangeSelectedValue();
    }

    public static void HighlightPreviousSlot()
    {
            // Unselect the current InventorySlot selected first

        _inventoryUI._Find(c_nameOfInventorySlot + currentInventorySelected).GetComponent<InventorySlot>().ChangeSelectedValue();

        if (currentInventorySelected == 0)
            currentInventorySelected = c_inventorySize - 1;
        else
            currentInventorySelected--;

        _inventoryUI._Find(c_nameOfInventorySlot + currentInventorySelected).GetComponent<InventorySlot>().ChangeSelectedValue();
    }

    public static Food GetFood(int index)
    {
        return (Food)_myInventory[index].Item;
    }
}