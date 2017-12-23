using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MasterClass {

    /* À partir d'un GameObject, trouver un de ses fils */
    public static GameObject _Find(this GameObject parent, string name)
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in transforms)
            if (t.name == name)
                return t.gameObject;
        return null;
    }

}
