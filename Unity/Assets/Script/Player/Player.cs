using System;
using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// La classe représentant les statistiques du joueur ainsi que les méthodes
/// décrivant les actions possibles par le joueur.
/// </summary>
public class Player
{
        // PROPERTIES

    public string   Pseudo      { get; private set; }   // Le pseudo du joueur

    public ushort   Hunger      { get; private set; }   // Le niveau de faim du joueur
    public ushort   Thrist      { get; private set; }   // Le niveau de soif du joueur

    public byte     Speed       { get; private set; }   // La vitesse du joueur

    public int      CurrentHP   { get; private set; }   // Les points de vie acutels du joueur
    public int      MaxHP       { get; private set; }   // Le nombre de points de vie maximum du joueur

        // METHODS
    
    /* DEBUG : à retirer, pour le debug uniquement */
    public override string ToString()
    {
        return "[" + this.Pseudo + "] " + this.CurrentHP + " / " + this.MaxHP + " PV  -  ";
    }

    #region action
    /* Si on mange un aliment, on lance une Coroutine effectuant l'action, et afficher sur l'UI le logo nourriture */
    public void Eat()
    {
        
    }
    #endregion
}