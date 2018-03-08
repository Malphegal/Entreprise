using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    namespace Behaviour
    {
        public abstract class AIDefaultBehaviour : MonoBehaviour, ILivingBeing
        {
                // FIELDS

            public new string name;

            public int currentHealth;
            public int maxHealth;

            public int attackValue;
            public int defenceValue;

            protected bool _isFacingRight;

            protected Rigidbody2D _rb;

            protected float _blink_RemainingTime;
            protected bool _isBlinking = false;

                // ABSTRACT METHODS

            protected abstract void AI();

            /* Trigger the death animation and then Destroy() itself */
            protected virtual IEnumerator Die()
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

                // METHODS

            protected void Awake()
            {
                _isFacingRight = true;
                _rb = GetComponent<Rigidbody2D>();

                    // Starts the AI

                AI();
            }

            // TODO: Seperate the damage received in a generic method ?
            /* Decrease its currentHealth, and call Die if it has reached 0 or less */
            public virtual void GotHit(int damage)
            {
                currentHealth -= Mathf.Max(0, damage - defenceValue);

                    // If the AI is dead

                if (currentHealth <= 0)
                {
                    StopAllCoroutines();
                    StartCoroutine(Die());
                    return;
                }

                    // It blinks if it has been touched

                _blink_RemainingTime = 1F;
                if (!_isBlinking)
                    StartCoroutine(StartBlinking());
            }

            /* Blinks the player to show that he got hit */
            private IEnumerator StartBlinking()
            {
                _isBlinking = true;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                while (_blink_RemainingTime > 0)
                {
                    spriteRenderer.color = new Color(1, 0.5F, 0.5F);
                    yield return new WaitForSeconds(0.1F);
                    spriteRenderer.color = Color.white;
                    yield return new WaitForSeconds(0.1F);
                    _blink_RemainingTime -= 0.2F;
                }
                _isBlinking = false;
                yield break;
            }
        }

        public interface ILivingBeing
        {
            void GotHit(int damage);
        }
    }
}