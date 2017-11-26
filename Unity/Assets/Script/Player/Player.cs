using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// La classe représentant les statistiques du joueur ainsi que les méthodes
/// décrivant les actions possibles par le joueur.
/// </summary>
public class Player : MonoBehaviour
{
        // FIELDS

    private static GameObject           _pnlCurrentBuffs;

    private static Player               _player;

    private static UnityEngine.UI.Text  _txtHungerValue;
    private static UnityEngine.UI.Text  _txtThirstValue;

    private static ushort _hunger;  
    private static ushort _thirst;

        // PROPERTIES

    public static string    Name        { get; private set; }   // Le pseudo du joueur

    public static ushort    Hunger      { get { return _hunger; } private set { _hunger = (ushort)Mathf.Clamp(value, 0, 100); } }   // Le niveau de faim du joueur
    public static ushort    Thirst      { get { return _thirst; } private set { _thirst = (ushort)Mathf.Clamp(value, 0, 100); } }   // Le niveau de soif du joueur

    public static byte      Speed       { get; private set; }   // La vitesse du joueur

    public static int       CurrentHP   { get; private set; }   // Les points de vie acutels du joueur
    public static int       MaxHP       { get; private set; }   // Le nombre de points de vie maximum du joueur

        // INVENTORY

    public static List<Item> _inventory = new List<Item>();

        // STATIC METHODS

    public static Player Instance()
    {
        return _player;
    }

        // METHODS
    
    void Awake()
    {
        _player = this;

        Hunger = 100;
        Thirst = 100;

        _pnlCurrentBuffs = GameObject.Find("pnlCurrentBuffs");

        _txtHungerValue = GameObject.Find("TxtHungerValue").GetComponent<UnityEngine.UI.Text>();
        _txtThirstValue = GameObject.Find("TxtThirstValue").GetComponent<UnityEngine.UI.Text>();

        InvokeRepeating("LoweringHunger", 0, 2);
        InvokeRepeating("LoweringThirst", 0, 1);
    }

    /* DEBUG : à retirer, pour le debug uniquement */
    public override string ToString()
    {
        return "[" + Name + "] " + CurrentHP + " / " + MaxHP + " PV  -  ";
    }

    #region action
    // TODO: Mettre la bonne image en fonction du buff
    /* Si on mange un aliment, on lance une Coroutine effectuant l'action, et afficher sur l'UI le logo nourriture */
    public void Eat(ushort hungerRegen, ushort thristRegen, ushort nbOfTicks)
    {
        GameObject newBuff = new GameObject("The eat !");

        UnityEngine.UI.Image img = newBuff.AddComponent<UnityEngine.UI.Image>();
        img.color = new Color(UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0F, 1F));

        newBuff.transform.SetParent(_pnlCurrentBuffs.transform);

        StartCoroutine(BuffImageRemainingTime(newBuff, hungerRegen, thristRegen, nbOfTicks));
    }
    #endregion

    private IEnumerator BuffImageRemainingTime(GameObject buffImage, ushort hungerRegen, ushort thirstRegen, ushort nbOfTicks)
    {
        byte i = 0;
        while (i < nbOfTicks)
        {
            yield return new WaitForSeconds(1);
            Hunger += hungerRegen;
            Thirst += thirstRegen;
            i++;
        }
        Destroy(buffImage);
    }

    private void LoweringHunger()
    {
        Hunger -= 1;
        _txtHungerValue.text = Hunger + " / 100 Hunger";
    }

    private void LoweringThirst()
    {
        Thirst -= 1;
        _txtThirstValue.text = Thirst + " / 100 Thirst";
    }
}