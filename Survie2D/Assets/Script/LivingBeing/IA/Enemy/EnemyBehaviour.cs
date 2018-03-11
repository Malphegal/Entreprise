using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NomDuJeu2D.Util;

namespace LivingBeing {

    namespace AI
    {
        namespace Enemy
        {
            public class EnemyBehaviour : AIDefaultBehaviour
            {
                    // FIELDS

                private bool continueIA = true;

                    // METHODS

                /* Wander to the left and the right */
                protected override void AI()
                {
                    StartCoroutine(Walk());
                }

                // TODO: Facto code
                private IEnumerator Walk()
                {
                    int rand;
                    while (continueIA)
                    {
                        rand = _isFacingRight ? (int)Random.Range(transform.position.x + 1, 8) : (int)Random.Range(-8, transform.position.x);
                        if (rand < transform.position.x)
                            while (rand < transform.position.x)
                            {
                                _rb.velocity = new Vector2(-2F, _rb.velocity.y);
                                yield return new WaitForSeconds(0.02F);
                            }
                        else
                            while (rand > transform.position.x)
                            {
                                _rb.velocity = new Vector2(2F, _rb.velocity.y);
                                yield return new WaitForSeconds(0.02F);
                            }

                            // Flip the sprite

                        transform.FlipTransform();
                        _isFacingRight = !_isFacingRight;

                        yield return new WaitForSeconds(Random.Range(2, 6));
                    }
                }

                /* Attack the player */
                public void Attack(GameObject player)
                {
                    player.GetComponent<ILivingBeing>().GotHit(attackValue);
                }

                /* If this enemy collides the player, trigger an attack */
                private void OnCollisionEnter2D(Collision2D collision)
                {
                        // if this enemy hits the player

                    if (collision.gameObject.layer == 9)
                        Attack(collision.gameObject);
                }
            }
        }
    }
}