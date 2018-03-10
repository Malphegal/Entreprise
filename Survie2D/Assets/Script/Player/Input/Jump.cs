using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    namespace Inputt
    {
        public class Jump : MonoBehaviour
        {
            private Rigidbody2D _rb;

            private float fallMultiplier = 2.5F;
            private float lowJumpMultiplier = 2F;

                // METHODS

            private void Awake()
            {
                _rb = GetComponent<Rigidbody2D>();
            }

            void Update()
            {
                if (_rb.velocity.y < 0)
                    _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
                    _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}