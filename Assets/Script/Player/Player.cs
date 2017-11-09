using System;
using System.Collections;
using System.Linq;

public class Player
{
        // FIELDS

        // PROPERTIES

    public string Name      { get; private set; }   // Le nom du joueur
    public int HP           { get; private set; }   // La quantité de point de vie du joueur
    public int AttackValue  { get; private set; }   // La valeur d'attaque de base (sans prendre en compte l'équipement) du personnage
    public int DefenceValue { get; private set; }   // La valeur de défense de base (sans prendre en compte l'équipement) du personnage.
    public int SpeedValue   { get; private set; }   // La valeur de la vitesse de déplacement de base du personnage, sans prendre en compte le poids de l'équipement


        // CONSTRUCTORS

    public Player(string name)
    {
        this.Name = name;
    }

        // METHODS

        // STATIC METHODS

}