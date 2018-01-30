using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour {

    // TODO: Can't attack the same enemy twice or more with a single hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
            collision.gameObject.GetComponent<Enemy>().GotHit(5);
    }
}
