using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : LivingEntity
{
        // CONSTRUCTOR

    public Character(ushort id, string name, int hpMax, int attackValue, int defenceValue, int speedValue)
        : base(id, name, hpMax, attackValue, defenceValue, speedValue) { }
}
