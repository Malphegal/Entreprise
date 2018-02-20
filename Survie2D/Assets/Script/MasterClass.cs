﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MasterClass {

        // FIELDS

    public static string NameOfThePlayer { get; set; }

        // STATIC METHODS

    /* Find a children of a specific GameObject */
    public static GameObject _Find(this GameObject parent, string name)
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in transforms)
            if (t.name == name)
                return t.gameObject;
        return null;
    }

        // TODO: Choose the right language instead of "French"
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeTheGame()
    {
        Lang.DefineLanguage(System.IO.Directory.GetFiles(".", "lang.fr.xml", System.IO.SearchOption.AllDirectories)[0], "French");
    }
}