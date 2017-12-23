using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe permettant le déplacement du modèle du joueur.
/// </summary>
public sealed class Controller : MonoBehaviour {

    /* Différent types d'object sur lesquels on peut intéragir */
    enum KindOfInteractObject
    {
        EdibleFood
    }

    #region FIELD

    private Rigidbody _rb;  // RigibBody du joueur

    private byte    _speed      = 3;        // Vitesse du joueur
    private float   _jumpPower  = 450;      // Hauteur de saut du joueur
    private bool    _isJumping  = false;    // Le joueur est-il en train de sauter ?
    private bool    _canJump    = true;     // Le joueur peut-il sauter ?

    private float _distanceCamera       = 2F;   // Distance camera par rapport au joueur (Min -8, Max 6 ; Mathf.Clamp)
    private float _distanceCameraSave   = 2F;   // Distance sauvegarde, pour zoom dezoom

    Vector3 screenPosForRayCast;  // Pour le raycast d'un objet 'Interact' : position de la caméra
    RaycastHit hit;     // Pour le raycast d'un objet 'Interact' : avons nous touché grace à ray

    private UnityEngine.UI.Text _txtCanPerformAnAction; // Le texte des actions que le joueur peut faire

    #endregion

    #region METHODS

    private void Awake()
    {
            // Lang

        Lang.DefineLanguage(System.IO.Directory.GetFiles(".", "lang.fr.xml", System.IO.SearchOption.AllDirectories)[0], "French");

            // Init

        _rb = GetComponent<Rigidbody>();

        screenPosForRayCast = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        _txtCanPerformAnAction = GameObject.Find("txtCanPerformAnAction").GetComponent<UnityEngine.UI.Text>();
    }
	
	private void Update ()
    {
            // Permet d'intéragir avec un objet 'Interact' à une distance de 'maxDistanceHit'

        int maxDistanceHit = 5;
        int layerMask = 1 << 9; // 9 == 'Interact'
        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPosForRayCast), out hit, maxDistanceHit, layerMask)){
            // TODO: Traiter la valeur de retour
            // Quel est le type de l'objet sur lequel on peut intéragir ?
            WhichInteractObjectRayCastHitIs(hit);
        }
        else
            _txtCanPerformAnAction.text = "";

            // Saut à l'aide de la touche de saut

        if (Input.GetButtonDown("Jump") && !_isJumping && _canJump)
        {
            _isJumping = true;
            _canJump = false;
        }

        if (Input.GetButtonDown("Display stats"))
            DisplayUI();
        if (Input.GetButtonUp("Display stats"))
            StartCoroutine("FadeOutUI");
    }

    private void FixedUpdate()
    {
            // Permet de faire un déplacement

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rb.MovePosition(_rb.position + transform.TransformDirection(direction) * _speed * Time.deltaTime);

            // Si on saute, on ne peux plus resauter

        if (_isJumping)
        {
            _rb.AddForce(transform.up * _jumpPower);
            _isJumping = false;
        }

            // Facteur de gravité

        _rb.AddForce(Physics.gravity * GetComponent<Rigidbody>().mass);
    }

    #endregion

    #region COLLISION

    /* TODO: Vérifier si la collision se fait avec un tag de terrain, et pas n'importe quoi */
    private void OnCollisionEnter(Collision collision)
    {
        _canJump = true;
    }

    // TODO: Changer le 'E' en la touche que le joueur choisira
    // TODO: Changer le text à l'écran en fonction de la langue
    private KindOfInteractObject WhichInteractObjectRayCastHitIs(RaycastHit raycastHit)
    {
        switch (raycastHit.collider.gameObject.tag)
        {
            case "EdibleFood":
                _txtCanPerformAnAction.text = "E - Pick up " + raycastHit.collider.gameObject.name;
                return KindOfInteractObject.EdibleFood;
            case "AA": case "BB":
                return KindOfInteractObject.EdibleFood;
            default:
                return KindOfInteractObject.EdibleFood;
        }
    }

    #endregion

    #region UI

    /* Afficher l'UI à l'écran */
    void DisplayUI()
    {
        StopCoroutine("FadeOutUI");
        GameObject.Find("--------------- UI ---------------")._Find("UIInGame").GetComponent<CanvasGroup>().alpha = 1;
    }

    /* Faire disparaître l'UI progressivement */
    IEnumerator FadeOutUI()
    {
        CanvasGroup UI = GameObject.Find("UIInGame").GetComponent<CanvasGroup>();
        float i = 1;
        while (i > 0)
        {
            UI.alpha = i -= Time.deltaTime / 1.6F;
            yield return null;
        }
        yield break;
    }

    #endregion
}
