using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : LivingEntity {

    // CONSTRUCTOR

    public Creature(ushort id, string name, int hpMax, int attackValue, int defenceValue, int speedValue)
        : base(id, name, hpMax, attackValue, defenceValue, speedValue) { }
}
