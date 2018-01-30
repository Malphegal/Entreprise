using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

        // FIELDS

    private float _maxSpeed = 10f;
    private bool _facingRight = true;
    [Range(1, 10)]
    public float jumpVelocity;

    private Rigidbody2D _rb;

    private bool inInventory = false;

        // METHODS

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
            // Mouvement

        if (!inInventory)
        {
            float move = Input.GetAxis("Horizontal");

            _rb.velocity = new Vector2(move * _maxSpeed, _rb.velocity.y);
            if ((move > 0 && !_facingRight) || (move < 0 && _facingRight))
                Flip();

            if (Input.GetButtonDown("Jump"))
                _rb.velocity = Vector2.up * jumpVelocity;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Inventory.HighlightNextSlot();
                if (InventoryOptions.CurrentlyInOptionWindow)
                    InventoryOptions.OpenOptions(false);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Inventory.HighlightPreviousSlot();
                if (InventoryOptions.CurrentlyInOptionWindow)
                    InventoryOptions.OpenOptions(false);
            }
        }

        // TODO: Change it to the right key (another class too ?)
        if (Input.GetKeyDown(KeyCode.N))
            StartCoroutine(Attack());

        // DEBUG : Remove it !
        if (Input.GetKeyDown(KeyCode.M))
            GetComponent<PlayerStat>().GotHit(Random.Range(4, 9));

            // Inventory

        // TODO: Put the right key
        if (Input.GetKeyDown(KeyCode.I))
        {
            inInventory ^= true;
            Inventory.HighlightSlots();
        }

            // Open options 

        if (inInventory && !InventoryOptions.CurrentlyInOptionWindow && Input.GetKeyDown(KeyCode.E))
            InventoryOptions.OpenOptions(true);
    }

    /* Change the sprite orientation */
    void Flip()
    {
        _facingRight ^= true;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

        // TODO: Move this method to another class
    private IEnumerator Attack()
    {
        Transform arme = transform.GetChild(0);

        arme.GetComponent<PolygonCollider2D>().enabled = true;

        while (arme.rotation.z > -.132)
        {
            arme.Rotate(0, 0, -10);
            yield return new WaitForSeconds(0.04F);
        }

        while (arme.rotation.z < 0.13)
        {
            arme.Rotate(0, 0, 10);
            yield return new WaitForSeconds(0.04F);
        }

        arme.GetComponent<PolygonCollider2D>().enabled = false;

        yield return null;
    }

    // TODO: Move this method to another class
    // TODO: Check => The same item is added twice sometimes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 10 => Item

        if (collision.gameObject.layer == 10)
        {
            Inventory.AddItem(collision.gameObject.GetComponent<Item>());
            DestroyObject(collision.gameObject);
        }
    }
}