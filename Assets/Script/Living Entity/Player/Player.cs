using System;
using System.Collections;
using System.Linq;

public class Player : LivingEntity
{
        // FIELDS

        // PROPERTIES

        // CONSTRUCTORS

    public Player(ushort id, string name, int hpMax, int attackValue, int defenceValue, int speedValue)
        : base (id, name, hpMax, attackValue, defenceValue, speedValue)
    {

    }

        // METHODS
    
    public override string ToString()
    {
        return this.Id + " " + this.Name + " " + this.HP + " " + this.HPMax + " " + this.AttackValue + " " +
            this.DefenceValue + " " + this.SpeedValue;
    }
}