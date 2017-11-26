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
        _speed      = 3;
        _jumpPower  = 450;
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
    /* TODO: Modifier le 'E' en la lettre que le joueur choisiera */
    #region Vérifier s'il existe un objet sur lequel on peut intéragir en face de nous
    private void OnTriggerEnter(Collider other)
    {
            // Est-ce un objet collectable ?

        /* TODO: Modifier le 'E' en la lettre que le joueur choisiera */
        if (other.CompareTag("CollectableObject"))
            _txtCanPerformAnAction.text = "E - Pick up " + other.gameObject.GetComponent<EdibleFood>().foodName;
    }

    /* TODO: Modifier le 'E' en la lettre que le joueur choisiera */
    private void OnTriggerStay(Collider other)
    {
            // Est-ce un objet collectable ?

        /* TODO: Modifier le 'E' en la lettre que le joueur choisiera */
        if (other.CompareTag("CollectableObject") && Input.GetKeyDown(KeyCode.E))
        {
            /*EdibleFood ef = other.GetComponent<EdibleFood>();
            Player.Instance().Eat(ef.hungerRegen, ef.thirstRegen, ef.nbOfTicks);*/
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
            // Était-ce un objet collectable ?

        if (other.CompareTag("CollectableObject"))
            _txtCanPerformAnAction.text = string.Empty;
    }
    #endregion
}
