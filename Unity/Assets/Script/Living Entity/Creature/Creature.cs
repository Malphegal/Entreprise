using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature {

    public ushort Id { get; private set; }   // L'id identifiant l'entité vivante
    public string Name { get; private set; }   // Le nom de l'entité vivante
    public int HP { get; private set; }   // La quantité de point de vie de l'entité vivante
    public int HPMax { get; private set; }   // La valeur d'HP maximum de l'entité vivante
    public int AttackValue { get; private set; }   // La valeur d'attaque de base de l'entité vivante
    public int DefenceValue { get; private set; }   // La valeur de défense de base de l'entité vivante
    public int SpeedValue { get; private set; }   // La valeur de la vitesse de déplacement de base de l'entité vivante
}