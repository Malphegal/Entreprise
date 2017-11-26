using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GLivingEntity {

        // FIELDS

    private static ushort _nextId = 0;  // L'identifiant que prendra la prochaine entité vivante créée

        // PROPERTIES
    
    public static List<LivingEntity> AllLivingEntity { get; private set; }  // Liste de toutes les entités vivantes créées

        // STATIC METHODS

    public static void CreateCharacter(string name, int hpMax, int attackValue, int defenceValue, int speedValue)
    {
        AllLivingEntity.Add(new Character(_nextId++, name, hpMax, attackValue, defenceValue, speedValue));
    }
    
    public static void CreateCreature(string name, int hpMax, int attackValue, int defenceValue, int speedValue)
    {
        AllLivingEntity.Add(new Creature(_nextId++, name, hpMax, attackValue, defenceValue, speedValue));
    }
}
