using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe permettant le déplacement du modèle du joueur.
/// </summary>
public class Controller : MonoBehaviour {

        // FIELDS

    private Rigidbody _rb;

    private byte _speed;
    private bool _isJumping;
    private bool _canJump;
    private float _jumpPower;

    private UnityEngine.UI.Text _txtCanPerformAnAction;

        // METHODS

    private void Awake()
    {
        _rb         = GetComponent<Rigidbody>();
        _speed      = 4;
        _jumpPower  = 250;
        _isJumping  = false;
        _canJump    = true;

        _txtCanPerformAnAction = GameObject.Find("TxtCanPerformAnAction").GetComponent<UnityEngine.UI.Text>();
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

    /* TODO: Vérifier si la collision se fait avec un tag de terrain, et pas n'importe quoi */
    private void OnCollisionEnter(Collision collision)
    {
        _canJump = true;
    }

    /* TODO: Si on sort du trigger d'un object collectable, mais qu'il y a encore un autre objet collectable,
             il faut non pas mettre le 'txtCanPerformAnAction.text' à vide, mais le remplir avec l'autre objet */
    #region Vérifier s'il existe un objet sur lequel on peut intéragir en face de nous
    private void OnTriggerEnter(Collider other)
    {
            // Est-ce une nourriture collectible ?

        if (other.CompareTag("EdibleFood"))
            _txtCanPerformAnAction.text = "Ramasser " + other.gameObject.GetComponent<EdibleFood>().nameOfFood;
    }

    private void OnTriggerExit(Collider other)
    {
            // Était-ce une nourriture collectible ?

        if (other.CompareTag("EdibleFood"))
            _txtCanPerformAnAction.text = string.Empty;
    }
    #endregion
}
