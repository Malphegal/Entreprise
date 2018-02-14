using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour {

    // FIELDS

    private const string c_skillTreeSlotName = "skillTreeSlot";
    private const int c_skillSlotCount = 7;

    private SkillTreeSlot[] _skillTreeSlot;

    private Text _info;

    private PlayerStat _playerStat;

    private int _currentSelectedSlot = 0;
    private float _remaningTime = 5F;
    private bool _isCoroutineRunning = false;

    private string _skillUnlockedText;

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

        gameObject._Find("skillTree_Title").GetComponent<Text>().text = Lang.GetString("ui.skilltree.title");

        _skillUnlockedText = Lang.GetString("ui.skilltree.skillunlocked");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SelectedPreviousSlot();

        if (Input.GetKeyDown(KeyCode.D))
            SelectNextSlot();
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
    }

    public void AllUnlocks(int id, string name)
    {
        _remaningTime = 5F;
        if (!_isCoroutineRunning)
            StartCoroutine(DisplayInfo());
        _isCoroutineRunning = true;

        _info.text = _skillUnlockedText + Lang.GetString(name);

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

    private IEnumerator DisplayInfo()
    {
        while (_remaningTime > 0)
        {
            yield return new WaitForSeconds(0.1F);
            _remaningTime -= 0.1F;
        }
        _info.text = "";
        _isCoroutineRunning = false;
    }
}