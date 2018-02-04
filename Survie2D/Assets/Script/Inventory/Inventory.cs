using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Add all Text in an array, aswell as the Sprite
//       No need to use GetComponent at runtime anymore
// TODO: Unselect the inventory if an enemy hits the player
// TODO: Each slot has an Item component, but its value changes if NumberOfSameItem
//       goes from 0 to something higher
public class Inventory : MonoBehaviour {

        // FIELDS

    #region -------- Inventory --------

    private const int c_inventorySize = 10;
    private const int c_maxStackSize = 99;
    private const string c_nameOfInventorySlot = "inventorySlot";

    private InventorySlot[] _myInventory;

    private int _currentSelectedSlot = 0;

    public PlayerStat _playerStat;

    #endregion

    #region -------- Option --------

    private GameObject _inventoryOption;
    private GameObject _eat;

    private int _currentSelectedOption = -1;
    private bool _optionWindowDisplayed = false;
    private bool _isEatActive = false;

    #endregion

        // METHODS

    private void Awake()
    {
        _inventoryOption = transform.parent.gameObject._Find("optionOfInventorySlot")._Find("optionOfInventorySlot_Panel");
        _eat = _inventoryOption._Find("optionOfInventorySlot_0");
        _playerStat = GameObject.Find("player").GetComponent<PlayerStat>();

        _myInventory = new InventorySlot[c_inventorySize];
        for (int i = 0; i < c_inventorySize; i++)
        {
            _myInventory[i] = gameObject._Find(c_nameOfInventorySlot + i).GetComponent<InventorySlot>();
            _myInventory[i].NumberOfSameItem = 0;
        }
    }

    // TODO: Remove InventoryOption ?
    private void Update()
    {
            // -------- Slots --------

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HighlightPreviousSlot();
            if (_optionWindowDisplayed)
                CloseOptions();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightNextSlot();
            if (_optionWindowDisplayed)
                CloseOptions();
        }

            // -------- Options window --------

        if (_optionWindowDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                HighlightNextOption();
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                HighlightPreviousOption();
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                CloseOptions();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentSelectedOption == 0)
                {
                    _playerStat.Eat(_myInventory[_currentSelectedSlot].GetComponent<Food>());
                }
                else if (_currentSelectedOption == 1)
                {
                    print("DROP - NOT IMPLEMENTED");
                }

                    // Remove it in any case, and close optionWindow if there is no item left on this InventorySlot

                if (!RemoveItem(_currentSelectedSlot))
                {
                    CloseOptions();
                }
            }
        }
        else if (!_optionWindowDisplayed && Input.GetKeyDown(KeyCode.E) && _myInventory[_currentSelectedSlot].NumberOfSameItem > 0)
        {
            OpenOptions();
        }
    }

    #region -------- Inventory --------

    /* Return true if the item has been added */
    public bool AddItem(Item itemToAdd)
    {
        int indexItemToAdd = ItemExistsAndStackableInInventory(itemToAdd);

            // If the item doesn't exists or there is a stack with 99 items

        if (indexItemToAdd == -1)
        {
            for (int i = 0; i < _myInventory.Length; i++)
                if (_myInventory[i].IsEmpty)
                {
                    if (itemToAdd.typeOfCollectableItem == Item.TypeOfCollectableItem.Food)
                    {
                        Food newFood = _myInventory[i].gameObject.AddComponent<Food>(); // Create a new component copied from the original item
                        newFood.InitValues(itemToAdd);
                    }
                    else
                    {
                        Item newItem = _myInventory[i].gameObject.AddComponent<Item>(); // Create a new component copied from the original item
                        newItem.InitValues(itemToAdd);
                    }

                    _myInventory[i].GetComponent<Item>().InitValues(itemToAdd); // Copy itemToAdd to this new component
                    _myInventory[i].NumberOfSameItem = 1;

                    GameObject inventorySlotIndexI = _myInventory[i].gameObject;
                    inventorySlotIndexI.GetComponent<Image>().sprite = itemToAdd.imageOfTheItem;

                        // Display the counter if the item is stackable

                    if (itemToAdd.stackable)
                    {
                        inventorySlotIndexI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "1";
                        inventorySlotIndexI.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    
                    return true;
                }

                // If there is no more slot left

            return false;
        }

            // If the item exists and one can be added

        _myInventory[indexItemToAdd].gameObject.GetComponentInChildren<Text>().text =
            (++_myInventory[indexItemToAdd].NumberOfSameItem).ToString();

        return true;
    }

        // TODO: Try to optimize the code
    /* If itemToCheck exists in inventory and stack value is lower than maxStackSize, return its index, otherwise return -1 */
    private int ItemExistsAndStackableInInventory(Item itemToCheck)
    {
        for (int i = 0; i < c_inventorySize; i++)
            if (!_myInventory[i].IsEmpty && _myInventory[i].GetComponent<Item>().itemName == itemToCheck.itemName)
                if (_myInventory[i].GetComponent<Item>().stackable && int.Parse(_myInventory[i].gameObject.GetComponentInChildren<Text>().text) < c_maxStackSize)
                    return i;
        return -1;
    }

    /* Return false is there is no more item at index indexToRemove */
    public bool RemoveItem(int indexToRemove)
    {
        Text text = _myInventory[indexToRemove].gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        if (text.text == "1" || !_myInventory[indexToRemove].GetComponent<Item>().stackable)
        {
            RemoveStack(indexToRemove);
            return false;
        }

        text.text = (--_myInventory[indexToRemove].NumberOfSameItem).ToString();
        return true;
    }

    /* Remove the whole stack at index indexToRemove */
    public void RemoveStack(int indexToRemove)
    {
        _myInventory[indexToRemove].NumberOfSameItem = 0;
        _myInventory[indexToRemove].GetComponent<Image>().sprite = null;
        _myInventory[indexToRemove].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
        _myInventory[indexToRemove].transform.GetChild(0).gameObject.SetActive(false);

        Destroy(_myInventory[indexToRemove].GetComponent<Item>());
    }

        // TODO: Drop an item and create the proper 2D sprite
    public void DropItem(int indexToDrop)
    {
        // InInventory = false;
        throw new System.NotImplementedException();
    }

        // TODO: Drop the whole stack at index indexToDrop
    public void DropStack(int indexToDrop)
    {
        throw new System.NotImplementedException();
    }

    /* Display the current selected slot */
    private void HighlightSlots()
    {
        _myInventory[_currentSelectedSlot].ChangeSelectedValue();
    }

    /* Move the current selected slot forward */
    private void HighlightNextSlot()
    {
            // Unselect the current InventorySlot selected first

        _myInventory[_currentSelectedSlot].ChangeSelectedValue();

        if (_currentSelectedSlot == c_inventorySize - 1)
            _currentSelectedSlot = 0;
        else
            _currentSelectedSlot++;

        _myInventory[_currentSelectedSlot].ChangeSelectedValue();
    }

    /* Move the current selected slot backward */
    private void HighlightPreviousSlot()
    {
            // Unselect the current InventorySlot selected first

        _myInventory[_currentSelectedSlot].ChangeSelectedValue();

        if (_currentSelectedSlot == 0)
            _currentSelectedSlot = c_inventorySize - 1;
        else
            _currentSelectedSlot--;

        _myInventory[_currentSelectedSlot].ChangeSelectedValue();
    }

    /* Unselect the current slot */
    private void OnDisable()
    {
        HighlightSlots();
        if (_optionWindowDisplayed)
            CloseOptions();
    }

    /* Select the current slot */
    private void OnEnable()
    {
        HighlightSlots();
    }

    #endregion

    #region -------- OptionWindow --------

    public void OpenOptions()
    {
        Item item = _myInventory[_currentSelectedSlot].GetComponent<Item>();

            // If there is no item in the current selected slot

        if (item == null)
            return;

            // If an item is selected


        print(item.typeOfCollectableItem);
        if (item.typeOfCollectableItem == Item.TypeOfCollectableItem.Food)
        {
            _eat.SetActive(_isEatActive = true);
            ChangeSelectedOption(0, true);
            _currentSelectedOption = 0;
        }
        else
        {
            ChangeSelectedOption(1, true);
            _currentSelectedOption = 1;
        }

            // Finally display the window, in the correct location

        MoveInventoryOptionWindow();
        _inventoryOption.SetActive(_optionWindowDisplayed = true);
    }

    public void CloseOptions()
    {
        ChangeSelectedOption(_currentSelectedOption, false);
        _currentSelectedOption = -1;
        _inventoryOption.SetActive(false);
        _eat.SetActive(_isEatActive = false);
        _optionWindowDisplayed = false;
    }

    private void MoveInventoryOptionWindow()
    {
        RectTransform rect = ((RectTransform)_inventoryOption.transform);
        rect.offsetMin = new Vector2(35 + (134 * _currentSelectedSlot), _isEatActive ? 0 : 33);
        rect.offsetMax = new Vector2(-(35 + (134 * (9 - _currentSelectedSlot))), 0);
    }

    private void ChangeSelectedOption(int index, bool selectIt)
    {
        Outline outline = _inventoryOption._Find("optionOfInventorySlot_" + index).GetComponent<Outline>();

        if (selectIt)
        {
            outline.effectColor = Color.red;
            outline.effectDistance = new Vector2(4, 2);
        }
        else
        {
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(2, 1);
        }
    }

    private void HighlightNextOption()
    {
            // First unselect the current option

        ChangeSelectedOption(_currentSelectedOption++, false);

        if (_currentSelectedOption == 3)
            _currentSelectedOption = _isEatActive ? 0 : 1;

            // Then select the new one

        ChangeSelectedOption(_currentSelectedOption, true);
    }

    private void HighlightPreviousOption()
    {
            // First unselect the current option

        ChangeSelectedOption(_currentSelectedOption--, false);

        if (_currentSelectedOption == 0)
            _currentSelectedOption = _isEatActive ? 0 : 2;

            // Then select the new one

        ChangeSelectedOption(_currentSelectedOption, true);
    }

    #endregion
}