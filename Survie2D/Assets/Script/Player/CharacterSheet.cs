using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour {

        // FIELDS

    private PlayerStat _playerStat;

    private Text _attack;
    private Text _defence;

        // METHODS

    private void Awake()
    {
        _playerStat = GameObject.Find("player").GetComponent<PlayerStat>();

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

    private void OnEnable()
    {
        UpdateAllStats();
    }
}