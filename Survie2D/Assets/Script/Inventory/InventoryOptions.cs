using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Obsolete("REMOVE IT !", true)]
public class InventoryOptions : MonoBehaviour {

        // FIELDS

    private const string nameOfOptions = "optionOfInventorySlot_";

    public static int currentOptionSelected = -1;

    private static GameObject _optionWindow;

    private static GameObject _thirdOption;
    private static Text _textOfThird;
    private static bool isThirdActive = false;

    public static bool CurrentlyInOptionsWindow { get { return currentOptionSelected != -1; } }

    public Inventory inventory;
    public CharacterSheet characterSheet;

    private void Awake()
    {
        _optionWindow = transform.GetChild(0).gameObject;
        _thirdOption = gameObject._Find("optionOfInventorySlot_0");
        _textOfThird = _thirdOption.GetComponentInChildren<Text>();
    }

    // TODO: Change keys to navigate throught the optionWindow
    private void Update()
    {

    }
    
    private static void ChangeSelectedValue(int index, bool selectIt)
    {
        Outline outline = _optionWindow._Find(nameOfOptions + index).GetComponent<Outline>();

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
}
