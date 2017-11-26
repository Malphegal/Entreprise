using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe permettant le déplacement du modèle du joueur.
/// </summary>
public class Controller : MonoBehaviour {

        // FIELDS

    Rigidbody _rb;
    byte _speed;
    bool _isJumping;
    bool _canJump;
    float _jumpPower;

        // METHODS

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = 10;
        _isJumping = false;
        _canJump = true;
        _jumpPower = 850;

        print("I'm awake");
    }

    void Start () {
        print("I'm started");
	}
	
	void Update () {
        if (Input.GetButtonDown("Jump") && !_isJumping && _canJump)
        {
            _isJumping = true;
            _canJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rb.MovePosition(_rb.position + transform.TransformDirection(direction) * _speed * Time.deltaTime);

        if (_isJumping)
        {
            _rb.AddForce(transform.up * _jumpPower);
            _isJumping = false;
        }
        _rb.AddForce(Physics.gravity * GetComponent<Rigidbody>().mass);
    }

    /* TODO: Vérifier si la collision se fait avec un tag de terrain */
    private void OnCollisionEnter(Collision collision)
    {
        _canJump = true;
    }
}
