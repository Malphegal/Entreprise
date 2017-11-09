using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity {

        // PROPERTIES

    public ushort Id        { get; private set; }   // L'id identifiant l'entité vivante
    public string Name      { get; private set; }   // Le nom de l'entité vivante
    public int HP           { get; private set; }   // La quantité de point de vie de l'entité vivante
    public int HPMax        { get; private set; }   // La valeur d'HP maximum de l'entit" vivante
    public int AttackValue  { get; private set; }   // La valeur d'attaque de base de l'entité vivante
    public int DefenceValue { get; private set; }   // La valeur de défense de base de l'entité vivante
    public int SpeedValue   { get; private set; }   // La valeur de la vitesse de déplacement de base de l'entité vivante


        // CONSTRUCTORS
    
    public LivingEntity(ushort id, string name, int hpMax, int attackValue, int defenceValue, int speedValue)
    {
        this.Id = id;
        this.Name = name;
        this.HPMax = hpMax;
        this.HP = hpMax;
        this.AttackValue = attackValue;
        this.DefenceValue = defenceValue;
        this.SpeedValue = speedValue;
    }
}
