using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOptions : MonoBehaviour {

    private const string nameOfOptions = "optionOfInventorySlot";

    public static int currentOptionSelected = -1;

    private static GameObject _optionWindow;

    public static bool CurrentlyInOptionWindow { get { return currentOptionSelected != -1; } }

    private void Awake()
    {
        _optionWindow = gameObject;
        gameObject.SetActive(false);
    }

    // TODO: Right keys for navigate throught the optionWindow
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            HighlightNextOption();

        if (Input.GetKeyDown(KeyCode.W))
            HighlightPreviousOption();

        if (Input.GetKeyDown(KeyCode.E))
        {

                // Eat

            if (currentOptionSelected == 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().Eat(Inventory.currentInventorySelected);
            }

                // Drop

            else if (currentOptionSelected == 1)
            {
                print("DROP - NOT IMPLEMENTED");
            }

                // Remove it in any case , and close optionWindow if there is no item left on this InventorySlot

            if (!Inventory.RemoveItem(Inventory.currentInventorySelected))
                OpenOptions(false);
        }
    }

    public static void OpenOptions(bool open)
    {
        ChangeSelectedValue(open ? 0 : currentOptionSelected, open);
        currentOptionSelected = open ? 0 : -1;
        _optionWindow.SetActive(open);
    }

    public static void HighlightNextOption()
    {
            // Unselect the current option

        ChangeSelectedValue(currentOptionSelected++, false);

        if (currentOptionSelected == 3)
            currentOptionSelected = 0;

        ChangeSelectedValue(currentOptionSelected, true);
    }

    public static void HighlightPreviousOption()
    {
            // Unselect the current option

        ChangeSelectedValue(currentOptionSelected--, false);

        if (currentOptionSelected == -1)
            currentOptionSelected = 2;

        ChangeSelectedValue(currentOptionSelected, true);
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

    public static void MoveInventoryOptionWindow(int index)
    {
        RectTransform rect = ((RectTransform)_optionWindow.transform);
        rect.anchorMin = new Vector2(0.17F + 0.07F * index, 0.77F);
        rect.anchorMax = new Vector2(0.225F + 0.07F * index, 0.865F);
    }
}
