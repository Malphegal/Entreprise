using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items.InventoryManagement;
using Items.Objects;
using Environnement.Bridge; // TODO: Move the used class to CanBuildBridge
using NomDuJeu2D.Util;

namespace LivingBeing
{
    namespace Player
    {
        namespace Inputt
        {
            public class Controller : MonoBehaviour
            {
                    // FIELDS

                private float _maxSpeed = 10f;
                private float _jumpVelocity = 5;
                private bool _facingRight = true;

                private Rigidbody2D _rb;
                private PlayerInput _playerInput;

                    // PROPERTIES

                public bool IsFacingRight { get { return _facingRight; } }

                    // METHODS

                private void Awake()
                {
                    _rb = GetComponent<Rigidbody2D>();
                    _playerInput = GetComponent<PlayerInput>();
                }

                private void Update()
                {
                        // -------- Mouvement --------

                    if (!_playerInput.CanMove)
                        return;

                    float move = Input.GetAxis("Horizontal");

                    _rb.velocity = new Vector2(move * _maxSpeed, _rb.velocity.y);
                    if ((move > 0 && !_facingRight) || (move < 0 && _facingRight))
                    {
                        transform.FlipTransform();
                        _facingRight = !_facingRight;
                    }

                    if (Input.GetButtonDown("Jump"))
                        _rb.velocity = Vector2.up * _jumpVelocity;
                }

                private void OnTriggerEnter2D(Collider2D collision)
                {
                        // 10 => Item

                    if (collision.gameObject.layer == 10)
                    {
                        if (Inventory.AddItem(collision.gameObject.GetComponent<Item>()))
                            Destroy(collision.gameObject);
                    }
                }

                // TODO : Move this method to CanBuildBridge class !
                private void OnTriggerStay2D(Collider2D collision)
                {
                        // 13 => CanBeBuilt

                    if (collision.gameObject.layer == 13 && UnityEngine.Input.GetKeyDown(KeyCode.E))
                    {
                        collision.gameObject.GetComponent<CanBuildBridge>().enabled = true;
                    }
                }
            }
        }
    }
}