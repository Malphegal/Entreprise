using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LivingBeing.Player.Stats;
using NomDuJeu2D.Util;

namespace Items
{
    namespace Objects
    {
            // TODO: Don't make this class static, and and a Rune panel which contains this class
        public static class RuneManagement
        {
                // STATIC FIELDS

            private static GameObject[] _runeSlots;
            private static bool[] _boolRuneSlots;

            private static CharacterSheet _characterSheet;

            // PROPERTIES

            public static bool RuneInCurrentSlot { get { return _boolRuneSlots[_characterSheet._currentSelectedRuneSlot]; } }

            // STATIC METHODS

            /* Set all static values */
            public static void InitRunes()
            {
                _runeSlots = new GameObject[4];
                _boolRuneSlots = new bool[4];

                _characterSheet = GameObject.Find("characterSheet").transform.GetChild(0).GetComponent<CharacterSheet>();

                for (int i = 0; i < 4; i++)
                {
                    _runeSlots[i] = _characterSheet.gameObject._Find("characterSheet_Rune" + i);
                    _boolRuneSlots[i] = false;
                }
            }

            public static void AddRune(Item rune)
            {
                int index = _characterSheet._currentSelectedRuneSlot;

                _boolRuneSlots[index] = true;

                // Add the sprite to Image

                Image img = _runeSlots[index].transform.GetChild(0).GetComponent<Image>();
                img.sprite = rune.ImageOfTheItem;
                img.color = Color.white;

                // Add a component Item

                Item newRune = _runeSlots[index].transform.GetChild(0).gameObject.AddComponent<Item>();
                newRune.InitValues(rune);
            }

            public static Item GetRune()
            {
                return _runeSlots[_characterSheet._currentSelectedRuneSlot].transform.GetChild(0).GetComponent<Item>();
            }

            public static void RemoveRune()
            {
                int index = _characterSheet._currentSelectedRuneSlot;

                _boolRuneSlots[index] = false;

                    // Remove the sprite to Image

                Image img = _runeSlots[index].transform.GetChild(0).GetComponent<Image>();
                img.sprite = null;
                img.color = new Color(1, 1, 1, 0);

                Object.Destroy(_runeSlots[index].transform.GetChild(0).GetComponent<Item>());
            }

            public static Item SwapRune(Item rune)
            {
                int index = _characterSheet._currentSelectedRuneSlot;

                Item oldRune = _runeSlots[index].transform.GetChild(0).GetComponent<Item>();

                _runeSlots[index].transform.GetChild(0).gameObject.AddComponent<Item>().InitValues(rune);
                _runeSlots[index].transform.GetChild(0).GetComponent<Image>().sprite = rune.ImageOfTheItem;

                return oldRune;
            }
        }
    }
}