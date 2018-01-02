using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InitPlayerStats {

    public static void NewGameInitStats(int HP)
    {
        Player.MaxHP = HP;
        Player.CurrentHP = HP;

            // Inventory

        Inventory.InitInventory(10);
    }
}