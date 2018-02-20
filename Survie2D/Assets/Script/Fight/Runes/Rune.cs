using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour {

    public enum Color
    {
        None = 0,
        Earth,  // Yellow
        Fire,   // Red
        Water,  // Blue
        Wind    // White
    }

	    // FIELDS

    public Color _color;
}