using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    #region FIELD

    private UnityEngine.UI.Text _txtHealthValue;    // Current and maximum health of the player
    private string _ui_currenthp;   // HP translated

    #endregion

    #region METHODS

    private void Awake()
    {
        // DEBUG:
        Lang.DefineLanguage(System.IO.Directory.GetFiles(".", "lang.fr.xml", System.IO.SearchOption.AllDirectories)[0], "French");

        _txtHealthValue = GameObject.Find("txtHealthValue").GetComponent<UnityEngine.UI.Text>();
        _ui_currenthp = Lang.GetString("ui.currenthp");
    }

    #endregion

    // TODO: Add the reduction of damage taken
    /* Called if the player got hit by something which affects his HP */
    public void GotHit(int damage)
    {
        Player.CurrentHP -= damage;
        _txtHealthValue.text = Player.CurrentHP + "/" + Player.MaxHP + " " + _ui_currenthp;
    }
}