using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour {

        // FIELDS
        
    [SerializeField] private string _name;  // Le nom de l'entité vivante

    [SerializeField] private int    _hp;                // La quantité de point de vie de l'entité vivante
    [SerializeField] private int    _maxHP;             // La valeur d'HP maximum de l'entité vivante
    [SerializeField] private int    _baseAttackValue;   // La valeur d'attaque de base de l'entité vivante
    [SerializeField] private int    _baseDefenceValue;  // La valeur de défense de base de l'entité vivante
    [SerializeField] private float  _speedValue;        // La valeur de la vitesse de déplacement de base de l'entité vivante

        // PROPERTIES

    public string   Name            { get { return _name; } }
    public int      HP              { get { return _hp; }               set { _hp = Mathf.Clamp(_hp + value, 0, MaxHP); } }
    public int      MaxHP           { get { return _maxHP; }            set { _maxHP = value; } }
    public int      AttackValue     { get { return _baseAttackValue; }  set { _baseAttackValue = value; } }
    public int      DefenceValue    { get { return _baseDefenceValue; } set { _baseDefenceValue = value; } }
    public float    SpeedValue      { get { return _speedValue; }       set { _speedValue = value; } }

    // TODO: Ajouter l'inventaire de cette entité

        // ABSTRACT METHODS

    public abstract void Movement();
}