using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item {

    public int hungerValue;
    public int thirstValue;

    public override void InitValues(Item item)
    {
        base.InitValues(item);
        Food foreignFood = ((Food)item);
        hungerValue = foreignFood.hungerValue;
        thirstValue = foreignFood.thirstValue;
    }
}
