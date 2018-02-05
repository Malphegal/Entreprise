using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

        // FIELDS

    private float _maxSpeed = 10f;
    private float _jumpVelocity = 5;
    private bool _facingRight = true;

    private Rigidbody2D _rb;

    public bool IsFacingRight { get { return _facingRight; } }

        // METHODS

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
            // -------- Mouvement --------

        float move = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(move * _maxSpeed, _rb.velocity.y);
        if ((move > 0 && !_facingRight) || (move < 0 && _facingRight))
            Flip();

        if (Input.GetButtonDown("Jump"))
                _rb.velocity = Vector2.up * _jumpVelocity;
    }

    /* Change the Sprite orientation */
    private void Flip()
    {
        _facingRight ^= true;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // TODO: Move this method to another class
    // TODO: Use a proper reference with Inventory
    private void OnTriggerEnter2D(Collider2D collision)
    {
            // 10 => Item
        
        if (collision.gameObject.layer == 10)
        {
            if (GameObject.Find("inventory").GetComponent<Inventory>().AddItem(collision.gameObject.GetComponent<Item>()))
                Destroy(collision.gameObject);
        }
    }
}