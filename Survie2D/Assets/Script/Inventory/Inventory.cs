using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player.Input;
using Player.Stats;
using NomDuJeu2D.Util;
using Items.Objects;

// TODO: Add all Text in an array, aswell as the Sprite
//       No need to use GetComponent at runtime anymore
// TODO: Unselect the inventory if an enemy hits the player
namespace Items
{
    namespace InventoryManagement
    {
        public class Inventory : MonoBehaviour
        {
                // FIELDS

            #region -------- Inventory --------

            private const int c_inventorySize = 10;
            private const int c_maxStackSize = 99;
            private const string c_nameOfInventorySlot = "inventorySlot";

            private static InventorySlot[] _myInventory;

            private int _currentSelectedSlot = 0;

            private static PlayerAction _playerAction;
            private static CharacterSheet _characterSheet;

            private bool _inRuneMode = false;

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
                _playerAction = GameObject.FindWithTag("Player").GetComponent<PlayerAction>();
                _characterSheet = GameObject.Find("characterSheet").transform.GetChild(0).GetComponent<CharacterSheet>();

                // Inventory

                _myInventory = new InventorySlot[c_inventorySize];
                for (int i = 0; i < c_inventorySize; i++)
                {
                    _myInventory[i] = gameObject._Find(c_nameOfInventorySlot + i).GetComponent<InventorySlot>();
                    _myInventory[i].NumberOfSameItem = 0;
                }
            }

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

                // -------- Options window AND runes --------

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
                        if (_currentSelectedOption == 0) // Eat
                        {
                            _playerAction.Eat((Food)_myInventory[_currentSelectedSlot].Item);
                        }
                        else if (_currentSelectedOption == 1) // Drop
                        {
                            print("DROP - NOT IMPLEMENTED");
                        }

                        // Remove it in any case, and close optionWindow if there is no item left on this InventorySlot

                        if (RemoveItem(_currentSelectedSlot) == 0) // Remove
                        {
                            CloseOptions();
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    // Inventory options

                    if (!_inRuneMode && _myInventory[_currentSelectedSlot].NumberOfSameItem > 0)
                    {
                        OpenOptions();
                    }

                    // Runes

                    else if (_inRuneMode)
                    {
                        MoveRune();
                    }
                }
            }

            #region -------- Inventory --------

            /* Return true if the item has been added */
            public static bool AddItem(Item itemToAdd)
            {
                    // If the item is a tool

                if (itemToAdd.typeOfCollectableItem == Item.TypeOfCollectableItem.Tool)
                {
                        // Add it in the ToolManagement

                    if (ToolManagement.AddTool((Tool)itemToAdd))
                    {
                        _characterSheet.AddTool((Tool)itemToAdd);
                        return true;
                    }
                    return false;
                }

                    // If it's not a tool

                int indexItemToAdd = ItemExistsAndStackableInInventory(itemToAdd);

                    // If the item doesn't exists or there is a stack with 99 items

                if (indexItemToAdd == -1)
                {
                    for (int i = 0; i < _myInventory.Length; i++)
                        if (_myInventory[i].IsEmpty)
                        {
                            AddItemAt(itemToAdd, i);
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

            /* Add an item at a specific empty location */
            private static void AddItemAt(Item itemToAdd, int index)
            {
                if (itemToAdd.typeOfCollectableItem == Item.TypeOfCollectableItem.Food)
                    _myInventory[index].gameObject.AddComponent<Food>();
                else
                    _myInventory[index].gameObject.AddComponent<Item>();
                _myInventory[index].Item.InitValues(itemToAdd);
                _myInventory[index].NumberOfSameItem = 1;

                GameObject inventorySlotIndexI = _myInventory[index].gameObject;
                inventorySlotIndexI.GetComponent<Image>().sprite = itemToAdd.ImageOfTheItem;

                if (itemToAdd.stackable)
                {
                    inventorySlotIndexI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "1";
                    inventorySlotIndexI.transform.GetChild(0).gameObject.SetActive(true);
                }
            }

            /* If itemToCheck exists in inventory and stack value is lower than maxStackSize, return its index, otherwise return -1 */
            private static int ItemExistsAndStackableInInventory(Item itemToCheck)
            {
                for (int i = 0; i < c_inventorySize; i++)
                    if (!_myInventory[i].IsEmpty && _myInventory[i].Item.itemName == itemToCheck.itemName)
                        if (_myInventory[i].Item.stackable && int.Parse(_myInventory[i].gameObject.GetComponentInChildren<Text>().text) < c_maxStackSize)
                            return i;
                return -1;
            }

            /* Return false is there is no more item at index indexToRemove */
            public static int RemoveItem(int indexToRemove)
            {
                if (_myInventory[indexToRemove].NumberOfSameItem <= 1)
                {
                    RemoveStack(indexToRemove);
                    return 0;
                }

                _myInventory[indexToRemove].GetComponentInChildren<Text>(true).text = (--_myInventory[indexToRemove].NumberOfSameItem).ToString();
                return _myInventory[indexToRemove].NumberOfSameItem;
            }

            /* Return the number of items still needed to reach amount */
            public static int RemoveItem(Item itemToRemove, int amount)
            {
                for (int i = 0; i < c_inventorySize && amount > 0; i++)
                {
                    Item currentItem = _myInventory[i].Item;

                    // If the current slot does not have any Item attached to it

                    if (currentItem == null)
                        continue;

                    // If the current slot matches the requested Item

                    if (currentItem.itemName == itemToRemove.itemName)
                    {
                        int temp = Mathf.Min(_myInventory[i].NumberOfSameItem, amount);
                        amount -= temp;

                        if ((_myInventory[i].NumberOfSameItem -= temp) == 0)
                            RemoveStack(i);
                        else
                            _myInventory[i].GetComponentInChildren<Text>().text = _myInventory[i].NumberOfSameItem.ToString();
                    }
                }
                return amount;
            }

            /* Remove the whole stack at index indexToRemove */
            public static void RemoveStack(int indexToRemove)
            {
                _myInventory[indexToRemove].NumberOfSameItem = 0;
                _myInventory[indexToRemove].GetComponent<Image>().sprite = null;
                _myInventory[indexToRemove].GetComponentInChildren<Text>(true).text = "0";
                _myInventory[indexToRemove].transform.GetChild(0).gameObject.SetActive(false);

                Destroy(_myInventory[indexToRemove].Item);
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

            // DEBUG : REMOVE IT ?
            public bool ItemExists(Item item, out int counter, out int indexToRemove)
            {
                for (int i = 0; i < _myInventory.Length; i++)
                {
                    Item tempItem = _myInventory[i].Item;
                    if (tempItem == null)
                        continue;
                    if (tempItem.itemName == item.itemName)
                    {
                        counter = _myInventory[i].NumberOfSameItem;
                        indexToRemove = i;
                        return true;
                    }
                }
                counter = -1;
                indexToRemove = -1;
                return false;
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
                _inRuneMode = false;
                HighlightSlots();
                if (_optionWindowDisplayed)
                    CloseOptions();
            }

            /* Select the current slot */
            private void OnEnable()
            {
                if (PlayerInput.RuneMode)
                    _inRuneMode = true;
                HighlightSlots();
            }

            #endregion

            #region -------- OptionWindow --------

            public void OpenOptions()
            {
                Item item = _myInventory[_currentSelectedSlot].Item;

                // If there is no item in the current selected slot

                if (item == null)
                    return;

                // If an item is selected

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

            #region -------- Runes --------

            /* Add, remove or swap runes, depends of the current inventory slot and character sheet slot */
            private void MoveRune()
            {
                Item rune = _myInventory[_currentSelectedSlot].Item;

                // If _characterSheet has no rune in the selected slot

                if (!RuneManagement.RuneInCurrentSlot)
                {
                    if (rune != null && rune.typeOfCollectableItem == Item.TypeOfCollectableItem.Rune)
                    {
                        RuneManagement.AddRune(rune);
                        RemoveItem(_currentSelectedSlot);
                        enabled = false;
                    }
                }

                // If _characterSheet has a rune in the selected slot

                else
                {
                    if (rune == null)
                    {
                        AddItemAt(RuneManagement.GetRune(), _currentSelectedSlot);
                        RuneManagement.RemoveRune();
                        enabled = false;
                    }

                    else if (rune != null && rune.typeOfCollectableItem == Item.TypeOfCollectableItem.Rune)
                    {
                        // SWAP
                        Item runeInCharSheet = RuneManagement.SwapRune(rune);

                        _myInventory[_currentSelectedSlot].GetComponent<Image>().sprite = runeInCharSheet.ImageOfTheItem;
                        _myInventory[_currentSelectedSlot].gameObject.GetComponent<Item>().InitValues(runeInCharSheet);

                        Destroy(runeInCharSheet);

                        enabled = false;
                    }
                }
            }

            #endregion
        }
    }
}