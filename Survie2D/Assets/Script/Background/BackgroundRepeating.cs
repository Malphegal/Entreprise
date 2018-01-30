using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeating : MonoBehaviour {

    public float zValue;

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.D))
            transform.Translate(move / (zValue * 8), 0, 0);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(move / (zValue * 8), 0, 0);
    }
}