using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Player : LivingEntity
{
        // FIELDS

        // PROPERTIES

    public ushort Hunger { get; private set; } // Le niveau de faim du joueur
    public ushort Thrist { get; private set; } // Le niveau de soif du joueur

        // CONSTRUCTORS

    public Player(ushort id, string name, int hpMax, int attackValue, int defenceValue, int speedValue)
        : base (id, name, hpMax, attackValue, defenceValue, speedValue)
    {
        // Hunger = maxHunger
        // Thrist = maxThrist
    }

        // METHODS
    
    public override string ToString()
    {
        return this.Id + " " + this.Name + " " + this.HP + " " + this.HPMax + " " + this.AttackValue + " " +
            this.DefenceValue + " " + this.SpeedValue;
    }

    public void BringDownHunger()
    {
        this.Hunger--;
    }

    public void BringDownThrist()
    {
        this.Thrist--;
    }
}