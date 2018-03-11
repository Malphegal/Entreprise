using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using LivingBeing.Player.Inputt; // TODO: init method instead
using NomDuJeu2D.Util;

namespace LivingBeing
{
    namespace Player
    {
        namespace Fight
        {
            public class Arrow : MonoBehaviour
            {
                private bool _right;
                private float _timeLeft = 8F;

                private const int arrowDamage = 32;
                private const float arrowImpulsionX = 18F;
                private const float arrowImpulsionY = 0.5F;

                // TODO: Init method instead, which use a parameter 'bool right' instead ?
                private void Awake()
                {
                    if (!(_right = GameObject.FindWithTag("Player").GetComponent<Controller>().IsFacingRight))
                        transform.FlipTransform();
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(_right ? arrowImpulsionX : -arrowImpulsionX, arrowImpulsionY), ForceMode2D.Impulse);
                }

                /* If the arrow lives more than 8sec, destroy it */
                private void Update()
                {
                    if ((_timeLeft -= Time.deltaTime) < 0)
                        Destroy(gameObject);
                }

                // TODO: Replace SendMessage by the interface ILivingBeing
                // TODO: The arrow isn't pined properly
                /* If the arrow hits an enemy, call GotHit to its gameObject */
                private void OnCollisionEnter2D(Collision2D collision)
                {
                    ILivingBeing lb = collision.gameObject.GetComponent<ILivingBeing>();
                    if (lb != null)
                        lb.GotHit(1);

                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Rigidbody2D>().angularVelocity = 0F;

                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<BoxCollider2D>());
                    transform.SetParent(collision.gameObject.transform);
                }
            }
        }
    }
}
                    //collision.gameObject.SendMessage("GotHit", arrowDamage, SendMessageOptions.DontRequireReceiver);