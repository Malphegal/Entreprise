using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LivingBeing
{
    namespace Player
    {
        namespace Fight
        {
            public class WeaponAttack : MonoBehaviour
            {
                private void OnTriggerEnter2D(Collider2D collision)
                {
                    if (collision.gameObject.layer == 8)
                        collision.gameObject.GetComponent<ILivingBeing>().GotHit(Random.Range(20, 35));
                }
            }
        }
    }
}