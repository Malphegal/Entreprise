using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// La classe représentant les statistiques du joueur ainsi que les méthodes
/// décrivants les actions possibles par le joueur.
/// </summary>
public sealed class Player : MonoBehaviour
{
    #region FIELDS

    private static Player _player; // Référence vers le joueur

    private static GameObject _pnlCurrentBuffs; // Le panel qui contient les icones des buffs actifs

    private static Text _txtHungerValue;    // Texte de la valeur de faim actuelle
    private static Text _txtThirstValue;    // Texte de la valeur de soif actuelle

    private static ushort _hunger;  // Valeur actuelle du niveau de faim du joueur
    private static ushort _thirst;  // Valeur actuelle du niveau de soif du joueur

    #endregion

    #region PROPERTIES

    public static string    Name        { get; private set; }   // Le pseudo du joueur

    public static ushort    Hunger      { get { return _hunger; } set { _hunger = (ushort)Mathf.Clamp(value, 0, 100); } }   // Le niveau de faim du joueur
    public static ushort    Thirst      { get { return _thirst; } set { _thirst = (ushort)Mathf.Clamp(value, 0, 100); } }   // Le niveau de soif du joueur

    public static byte      Speed       { get; private set; }   // La vitesse du joueur

    public static int       MaxHP       { get; private set; }   // Le nombre de points de vie maximum du joueur
    public static int       CurrentHP   { get; private set; }   // Les points de vie acutels du joueur

    #endregion

    #region INVENTORY

    public static List<Item> _inventory = new List<Item>();

    #endregion

    #region METHODS
    
    void Awake()
    {
        _player = this;

        Hunger = 100;
        Thirst = 100;

        _pnlCurrentBuffs = GameObject.Find("pnlCurrentBuffs");

        _txtHungerValue = GameObject.Find("TxtHungerValue").GetComponent<Text>();
        _txtThirstValue = GameObject.Find("TxtThirstValue").GetComponent<Text>();

        InvokeRepeating("LoweringHunger", 0, 2);
        InvokeRepeating("LoweringThirst", 0, 1);
    }

    /* DEBUG : à retirer, pour le debug uniquement */
    public override string ToString()
    {
        return "[" + Name + "] " + CurrentHP + " / " + MaxHP + " PV  -  ";
    }

    #endregion

    #region ACTION

    // TODO: Mettre la bonne image en fonction de l'aliment
    // TODO: En fonction de FoodType, jouer le bon son (et la bonne animation ?)
    /* Si on mange un aliment, on lance une Coroutine effectuant l'action, et afficher sur l'UI le logo nourriture */
    public void EatOrDrink(EdibleFood.FoodType foodType, ushort hungerRegen, ushort thristRegen, ushort nbOfTicks)
    {
            // TODO: En fonction de FoodType, jouer le bon son (et la bonne animation ?)

            // Créer une icone à mettre dans le 'pnlCurrentBuffs'

        GameObject newBuff = new GameObject("Eating Food");

        Image img = newBuff.AddComponent<Image>();
        // TODO: A changer -> mettre une image à la place, en fonction de l'aliment
        img.color = new Color(UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0F, 1F));

        newBuff.transform.SetParent(_pnlCurrentBuffs.transform);

            // Manger un aliment pendant 'nbOfTicks' secondes

        StartCoroutine(Food_BuffImageRemainingTime(newBuff, hungerRegen, thristRegen, nbOfTicks));
    }

    #endregion

    #region FOOD

    /* Supprime l'image (buffImage) du panel des buffs après que l'effet de la nourriture soit terminé (après 'nbOfTicks' secondes) */
    private IEnumerator Food_BuffImageRemainingTime(GameObject buffImage, ushort hungerRegen, ushort thirstRegen, ushort nbOfTicks)
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

    // TODO: Changer pour avoir de bonnes valeurs
    /* Faire baisser continuellement le niveau de faim du joueur */
    private void LoweringHunger()
    {
        Hunger -= 1;
        _txtHungerValue.text = Hunger + "/100 " + Lang.GetString("ui.currenthunger");
    }

    // TODO: Changer pour avoir de bonnes valeurs
    /* Faire baisser continuellement le niveau de soif du joueur */
    private void LoweringThirst()
    {
        Thirst -= 1;
        _txtThirstValue.text = Thirst + "/100 " + Lang.GetString("ui.currentthirst");
    }

    #endregion
}