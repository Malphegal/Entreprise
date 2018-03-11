using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LivingBeing.Player.Stats;
using NomDuJeu2D.Util;

namespace LivingBeing {

    namespace Player
    {
        namespace Skill
        {
            public class SkillTree : MonoBehaviour
            {
                    // FIELDS

                private const string c_skillTreeSlotName = "skillTreeSlot";
                private const int c_skillSlotCount = 7;

                private SkillTreeSlot[] _skillTreeSlot;

                private Text _info;
                private Text _coin;
                private Text _price;

                private string priceText;

                private PlayerStat _playerStat;

                private int _currentSelectedSlot = 0;
                private float _remaningTime = 5F;

                private string _skillUnlockedText;

                private int _skillTreeCoins = 0;
                public int SkillTreeCoins { get { return _skillTreeCoins; } set { _coin.text = (_skillTreeCoins = value).ToString(); } }

                    // METHODS

                private void OnEnable()
                {
                    ChangeSelectedValue(0, true);
                }

                private void OnDisable()
                {
                    ChangeSelectedValue(_currentSelectedSlot, false);
                    _currentSelectedSlot = 0;
                }

                private void Awake()
                {
                    _playerStat = GameObject.Find("player").GetComponent<PlayerStat>();

                    _skillTreeSlot = new SkillTreeSlot[c_skillSlotCount];
                    for (int i = 0; i < c_skillSlotCount; i++)
                        _skillTreeSlot[i] = gameObject._Find(c_skillTreeSlotName + i).GetComponent<SkillTreeSlot>();

                    _info = gameObject._Find("skillTree_SkillUnlocked").GetComponent<Text>();
                    _coin = gameObject._Find("skillTree_Coin").GetComponent<Text>();
                    _price = gameObject._Find("skillTree_CurrentPrice").GetComponent<Text>();

                    priceText = Lang.GetString("ui.skilltree.price");

                    gameObject._Find("skillTree_Title").GetComponent<Text>().text = Lang.GetString("ui.skilltree.title");

                    _skillUnlockedText = Lang.GetString("ui.skilltree.skillunlocked");
                }

                private void Update()
                {
                    if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                        SelectedPreviousSlot();

                    if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                        SelectNextSlot();

                    if (_remaningTime > 0)
                    {
                        _remaningTime -= Time.deltaTime;
                        if (_remaningTime <= 0)
                            _info.text = "";
                    }
                }

                private void SelectNextSlot()
                {
                    ChangeSelectedValue(_currentSelectedSlot++, false);

                    if (_currentSelectedSlot == c_skillSlotCount)
                        _currentSelectedSlot = 0;

                    ChangeSelectedValue(_currentSelectedSlot, true);
                }

                private void SelectedPreviousSlot()
                {
                    ChangeSelectedValue(_currentSelectedSlot--, false);

                    if (_currentSelectedSlot == -1)
                        _currentSelectedSlot = c_skillSlotCount - 1;

                    ChangeSelectedValue(_currentSelectedSlot, true);
                }

                private void ChangeSelectedValue(int index, bool selectIt)
                {
                    Outline outline = _skillTreeSlot[index].transform.parent.gameObject.GetComponent<Outline>();

                    if (selectIt)
                    {
                        outline.effectColor = Color.red;
                        outline.effectDistance = new Vector2(6, 6);
                    }
                    else
                    {
                        outline.effectColor = Color.black;
                        outline.effectDistance = new Vector2(3, 3);
                    }

                    _skillTreeSlot[index].enabled = _skillTreeSlot[index].Unlocked ? false : selectIt;
                    _price.text = priceText + (_skillTreeSlot[index].enabled ? _skillTreeSlot[index].price.ToString() : "0");
                }

                public void AllUnlocks(int id, string name)
                {
                    _remaningTime = 5F; // Used to display the name of the new unlocked skill, in seconds

                    _info.text = _skillUnlockedText + Lang.GetString(name); // Information about the name of the current unlocked skill, lasts few seconds
                    _price.text = priceText + "0"; // The current price required for the selected skill is now equal to 0

                        // Unlock the skill by modify what it requires

                    switch (id)
                    {
                        case 0:
                            _playerStat.AdditionalPercentageDefenceValue += 30;
                            return;
                        case 1:
                            _playerStat.AdditionalPercentageAttackValue += 30;
                            return;
                        case 2:
                            _playerStat.AdditionalPercentageAttackValue += 30;
                            return;
                        case 3:
                            _playerStat.AdditionalPercentageAttackValue += 30;
                            return;
                        case 4:
                            _playerStat.AdditionalPercentageDefenceValue += 30;
                            return;
                        case 5:
                            _playerStat.AdditionalPercentageDefenceValue += 30;
                            return;
                        case 6:
                            _playerStat.AdditionalPercentageDefenceValue += 30;
                            return;
                    }
                }
            }
        }
    }
}