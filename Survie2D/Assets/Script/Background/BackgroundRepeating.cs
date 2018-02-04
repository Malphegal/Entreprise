using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeating : MonoBehaviour {

    public float zValue;

    void Update()
    {
        transform.Translate(Time.deltaTime * 0.5F, 0, 0);
    }
}