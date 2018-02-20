using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour {

        // FIELDS

    private PlayerStat _playerStat;
    private Inventory _inventory;

    private Text _attack;
    private Text _defence;

    private GameObject[] _runeSlots;
    private int _currentSelectedRuneSlot = 0;

        // PROPERTIES

    public int RuneSlotIndex {
        get { return _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Item>() != null
                ? _currentSelectedRuneSlot + 1
                : -(_currentSelectedRuneSlot + 1); }
    }

        // METHODS

    private void Awake()
    {
        _playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        _inventory = GameObject.Find("inventory").GetComponent<Inventory>();

        _runeSlots = new GameObject[4];
        for (int i = 0; i < 4; i++)
            _runeSlots[i] = gameObject._Find("characterSheet_Rune" + i);
        
        gameObject._Find("characterSheet_Name").GetComponent<Text>().text = MasterClass.NameOfThePlayer;
        gameObject._Find("characterSheet_Name").GetComponent<Text>().text = "Akuumy"; // DEBUG : REMOVE IT !
        _attack = gameObject._Find("characterSheet_Attack").GetComponent<Text>();
        _defence = gameObject._Find("characterSheet_Defence").GetComponent<Text>();
    }

    // TODO: Add all other stats
    // TODO: Store all GetString in a string variable
    public void UpdateAllStats()
    {
        _attack.text = Lang.GetString("ui.charactersheet.attack") + " : " + _playerStat.AttackValue;
        _defence.text = Lang.GetString("ui.charactersheet.defence") + " : " + _playerStat.DefenceValue;
    }

    /* Refresh all stats in the characterSheet */
    private void OnEnable()
    {
        HighLight(_currentSelectedRuneSlot = 0, true);
        UpdateAllStats();
    }

    private void OnDisable()
    {
        HighLight(_currentSelectedRuneSlot, false);
    }

    public void Update()
    {
        if (_inventory.enabled)
            return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            SelectNextSlot(false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SelectPreviousSlot(false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectPreviousSlot(true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectNextSlot(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.enabled = true;
        }
    }

    private void SelectNextSlot(bool bot)
    {
        if (bot)
        {
            if (_currentSelectedRuneSlot > 1)
                return;

            HighLight(_currentSelectedRuneSlot, false);
            _currentSelectedRuneSlot += 2;
            HighLight(_currentSelectedRuneSlot, true);
        }
        else
        {
            if (_currentSelectedRuneSlot == 3 || _currentSelectedRuneSlot == 1)
                return;

            HighLight(_currentSelectedRuneSlot, false);
            _currentSelectedRuneSlot++;
            HighLight(_currentSelectedRuneSlot, true);
        }      
    }

    private void SelectPreviousSlot(bool top)
    {
        if (top)
        {
            if (_currentSelectedRuneSlot < 2)
                return;

            HighLight(_currentSelectedRuneSlot, false);
            _currentSelectedRuneSlot -= 2;
            HighLight(_currentSelectedRuneSlot, true);
        }
        else
        {
            if (_currentSelectedRuneSlot == 2 || _currentSelectedRuneSlot == 0)
                return;

            HighLight(_currentSelectedRuneSlot, false);
            _currentSelectedRuneSlot--;
            HighLight(_currentSelectedRuneSlot, true);
        }
    }

    private void HighLight(int index, bool display)
    {
        Outline outline = _runeSlots[index].GetComponent<Outline>();

        if (display)
        {
            outline.effectDistance = new Vector2(2, 2);
            outline.effectColor = Color.red;
        }
        else
        {
            outline.effectDistance = new Vector2(1, 1);
            outline.effectColor = Color.black;
        }
    }

    public void AddRune(Item rune)
    {
            // Add the sprite to Image

        Image img = _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Image>();
        img.sprite = rune.ImageOfTheItem;
        img.color = Color.white;

            // Add a component Item

        Item newRune = _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).gameObject.AddComponent<Item>();
        newRune.InitValues(rune);
    }

    public Item GetRune()
    {
        return _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Item>();
    }

    public void RemoveRune(bool remove = true)
    {
            // Add the sprite to Image

        Image img = _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Image>();
        img.sprite = null;
        img.color = new Color(1, 1, 1, 0);

            // Destroy it if remove is true

        if (remove)
            Destroy(_runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Item>());
    }

    public Item SwapRune(Item rune)
    {
        Item oldRune = _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Item>();

        _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).gameObject.AddComponent<Item>().InitValues(rune);
        _runeSlots[_currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Image>().sprite = rune.ImageOfTheItem;

        return oldRune;
    }
}