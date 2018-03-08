using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Input;

namespace Player
{
    namespace Fight
    {
        public class Arrow : MonoBehaviour
        {
            private bool _right;
            private float _timeLeft = 8F;

            private const int arrowDamage = 12;
            private const int arrowImpulsionX = 18;
            private const int arrowImpulsionY = 2;

            // TODO: Init method instead, which use a parameter 'bool right' instead ?
            private void Awake()
            {
                if (!(_right = GameObject.Find("player").GetComponent<Controller>().IsFacingRight))
                    Flip();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(_right ? arrowImpulsionX : -arrowImpulsionX, arrowImpulsionY), ForceMode2D.Impulse);
            }

            /* If the arrow lives more than 8sec, destroy it */
            private void Update()
            {
                if ((_timeLeft -= Time.deltaTime) < 0)
                    Destroy(gameObject);
            }

            /* If the arrow hits an enemy, call GotHit to its gameObject */
            private void OnCollisionEnter2D(Collision2D collision)
            {
                collision.gameObject.SendMessage("GotHit", arrowDamage, SendMessageOptions.DontRequireReceiver);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0F;
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<BoxCollider2D>());
                transform.SetParent(collision.gameObject.transform);
            }

            // TODO: Make flip as a extended static method ?
            /* Change the arrow orientation */
            void Flip()
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }
}