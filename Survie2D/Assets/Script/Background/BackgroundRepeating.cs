using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public class BackgroundRepeating : MonoBehaviour
    {
        void Update()
        {
            transform.Translate(Time.deltaTime * 0.5F, 0, 0);
        }
    }
}