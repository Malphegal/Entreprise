using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour {

        // TODO: Can't attack the same enemy twice or more with a single hit

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            collision.gameObject.GetComponent<Enemy>().GotHit(5);
    }
}
