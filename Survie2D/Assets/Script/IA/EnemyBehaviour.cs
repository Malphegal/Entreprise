using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Stats;

namespace IA
{
    namespace Enemy
    {
        public class EnemyBehaviour : MonoBehaviour
        {
                // FIELDS

            public string nameOfEnemy;

            private Sprite _sprite;

            public int currentHealth;
            private int _maxHealth;
            public int attackValue;

            public int defenceValue;

            private bool _facingRight = true;

            private bool continueIA = true;

            Rigidbody2D _rb;

            private float _remainingTime_Blink;
            public bool _blinking = false;

                // METHODS

            private void Awake()
            {
                _maxHealth = currentHealth;
                _rb = GetComponent<Rigidbody2D>();

                IA();
            }

            // TODO: Implement method
            protected virtual void IA()
            {
                StartCoroutine(Walk());
            }

            // TODO: Facto code
            private IEnumerator Walk()
            {
                int rand;
                while (continueIA)
                {
                    rand = _facingRight ? (int)Random.Range(transform.position.x + 1, 8) : (int)Random.Range(-8, transform.position.x);
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

                    Flip();
                    yield return new WaitForSeconds(Random.Range(2, 6));
                }
            }

            /* Vertical flip of the sprite */
            void Flip()
            {
                _facingRight ^= true;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }

            // TODO: Add defence
            // TODO: Check if not dead yet
            public virtual void GotHit(int damage)
            {
                currentHealth = Mathf.Max(currentHealth - damage, 0);

                if (currentHealth == 0)
                {
                    StopAllCoroutines();
                    StartCoroutine(Die());
                    return;
                }

                _remainingTime_Blink = 1F;
                if (!_blinking)
                    StartCoroutine(StartBlinking());
            }

            // TODO: SendMessage instead of GetComponent ?
            /* Attack the player */
            public void Attack(GameObject player)
            {
                player.GetComponent<PlayerStat>().GotHit(attackValue);
            }

            /* Trigger death animation and then Destroy() itself */
            public virtual IEnumerator Die()
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Destroy(GetComponent<Collider2D>());
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<Animator>());

                    // Reset blinking values first

                spriteRenderer.color = Color.white;

                    // Slowly decrease its alpha value

                float timer = 5F;
                while ((timer -= 0.2F) > 0)
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.04F);
                    yield return new WaitForSeconds(0.1F);
                }
                Destroy(gameObject);
            }

            /* If this enemy collides the player, trigger an attack */
            private void OnCollisionEnter2D(Collision2D collision)
            {
                // if this enemy hits the player

                if (collision.gameObject.layer == 9)
                    Attack(collision.gameObject);
            }

            /* Blinks the player to show that he got hit */
            private IEnumerator StartBlinking()
            {
                _blinking = true;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                while (_remainingTime_Blink > 0)
                {
                    spriteRenderer.color = new Color(1, 0.5F, 0.5F);
                    yield return new WaitForSeconds(0.1F);
                    spriteRenderer.color = Color.white;
                    yield return new WaitForSeconds(0.1F);
                    _remainingTime_Blink -= 0.2F;
                }
                _blinking = false;
                yield break;
            }
        }
    }
}