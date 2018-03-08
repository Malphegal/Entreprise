using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA.Enemy;

public class WeaponAttack : MonoBehaviour {

    // TODO: Use SendMessage instead ?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            collision.gameObject.GetComponent<EnemyBehaviour>().GotHit(Random.Range(20, 35));
    }
}
