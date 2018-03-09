using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items.InventoryManagement;
using NomDuJeu2D.Util;

namespace Player
{
    namespace Stats
    {
        public class CharacterSheet : MonoBehaviour
        {
                // FIELDS

            private PlayerStat _playerStat;
            private Inventory _inventory;

            private Text _attack;
            private Text _defence;

            public int _currentSelectedRuneSlot = 0;

            private Outline[] _runeOutlines;

                // METHODS

            private void Awake()
            {
                _playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
                _inventory = GameObject.Find("inventory").GetComponent<Inventory>();

                _runeOutlines = new Outline[4];
                for (int i = 0; i < 4; i++)
                    _runeOutlines[i] = gameObject._Find("characterSheet_Rune" + i).GetComponent<Outline>();

                gameObject._Find("characterSheet_Name").GetComponent<Text>().text = MasterClass.NameOfThePlayer;
                _attack = gameObject._Find("characterSheet_Attack").GetComponent<Text>();
                _defence = gameObject._Find("characterSheet_Defence").GetComponent<Text>();
            }

            // TODO: Add all other stats
            // TODO: Store all GetString in a string variable
            /* Refresh all stats in the characterSheet */
            private void OnEnable()
            {
                HighLight(_currentSelectedRuneSlot = 0, true);
                _attack.text = Lang.GetString("ui.charactersheet.attack") + " : " + _playerStat.AttackValue;
                _defence.text = Lang.GetString("ui.charactersheet.armor") + " : " + _playerStat.DefenceValue;
            }

            private void OnDisable()
            {
                HighLight(_currentSelectedRuneSlot, false);
            }

            public void Update()
            {
                if (_inventory.enabled)
                    return;

                if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                {
                    SelectNextSlot(false);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                {
                    SelectPreviousSlot(false);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.W))
                {
                    SelectPreviousSlot(true);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                {
                    SelectNextSlot(true);
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.E))
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
                if (display)
                {
                    _runeOutlines[index].effectDistance = new Vector2(2, 2);
                    _runeOutlines[index].effectColor = Color.red;
                }
                else
                {
                    _runeOutlines[index].effectDistance = new Vector2(1, 1);
                    _runeOutlines[index].effectColor = Color.black;
                }
            }
        }
    }
}