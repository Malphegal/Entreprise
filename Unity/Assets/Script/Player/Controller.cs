using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Classe permettant le déplacement du modèle du joueur.
/// </summary>
public sealed class Controller : MonoBehaviour {

    /* Différent types d'object sur lesquels on peut intéragir */
    enum InteractObject
    {
        CollectableObject,
        LivingEntity
    }

    #region FIELD

    private Rigidbody _rb;  // RigibBody du joueur

    private byte    _speed      = 3;        // Vitesse du joueur
    private float   _jumpPower  = 450F;      // Hauteur de saut du joueur
    private bool    _isJumping  = false;    // Le joueur est-il en train de sauter ?
    private bool    _canJump    = true;     // Le joueur peut-il sauter ?

    //private float _distanceCamera       = 2F;   // Distance camera par rapport au joueur (Min -8, Max 6 ; Mathf.Clamp)
    //private float _distanceCameraSave   = 2F;   // Distance sauvegarde, pour zoom dezoom

    Vector3 screenPosForRayCast;  // Pour le raycast d'un objet 'Interact' : position de la caméra
    RaycastHit hit;     // Pour le raycast d'un objet 'Interact' : avons nous touché grace à ray

    private UnityEngine.UI.Text _txtCanPerformAnAction; // Le texte des actions que le joueur peut faire

    #endregion

    #region METHODS

    private void Awake()
    {
        // DEBUG:
        Lang.DefineLanguage(System.IO.Directory.GetFiles(".", "lang.fr.xml", System.IO.SearchOption.AllDirectories)[0], "French");

            // Init

        _rb = GetComponent<Rigidbody>();

        screenPosForRayCast = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        _txtCanPerformAnAction = GameObject.Find("txtCanPerformAnAction").GetComponent<UnityEngine.UI.Text>();
    }
	
	private void Update ()
    {
            // Permet d'intéragir avec un objet 'Interact' à une distance de 'maxDistanceHit'

        int maxDistanceHit = 10;
        int layerMask = 1 << 9; // 9 == 'Interact'
        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPosForRayCast), out hit, maxDistanceHit, layerMask)){
            // Quel est le type de l'objet sur lequel on peut intéragir ?
            InteractObject tagOfObject = WhichInteractObjectRayCastHitIs(hit);
            _txtCanPerformAnAction.text = CanPerformAnActionValue(tagOfObject, hit);

            if (tagOfObject == InteractObject.LivingEntity)
                if (Input.GetKeyDown(KeyCode.M))
                    hit.collider.gameObject.GetComponent<LivingEntity>().GotHit(Random.Range(7, 18));

            if (tagOfObject == InteractObject.CollectableObject)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Inventory.AddItem(hit.collider.gameObject.GetComponent<Item>()))
                        foreach (Component c in hit.collider.gameObject.GetComponents<Component>())
                        {
                            System.Reflection.PropertyInfo pi;
                            if ((pi = c.GetType().GetProperty("enabled")) != null)
                                pi.SetValue(c, c == c.GetComponent<Item>(), null);
                        }
                    else
                        print("Inventory FULL");
                }
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

        if (Input.GetKeyDown(KeyCode.P))
            GetComponent<PlayerAttack>().GotHit(Random.Range(7, 18));

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject.Find("--------------- UI ---------------")._Find("inventory").SetActive(!GameObject.Find("--------------- UI ---------------")._Find("inventory").activeInHierarchy);
            // TODO: Open the inventory menu
        }
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

    private InteractObject WhichInteractObjectRayCastHitIs(RaycastHit raycastHit)
    {
        switch (raycastHit.collider.gameObject.tag)
        {
            case "CollectableObject":
                return InteractObject.CollectableObject;
            case "LivingEntity":
                return InteractObject.LivingEntity;
            default:
                return (InteractObject)(-1);
        }
    }

    // TODO: Changer le 'E' en la touche que le joueur choisira
    private string CanPerformAnActionValue(InteractObject interactObject, RaycastHit raycastHit)
    {
        switch (interactObject)
        {
            case InteractObject.CollectableObject:
                return "E - " + Lang.GetString("controller.action.pickup") + " " + Lang.GetString(raycastHit.collider.gameObject.GetComponent<Item>().itemName);
            case InteractObject.LivingEntity:
                return raycastHit.collider.gameObject.name;
            default:
                return "";
        }
    }

    #endregion

    #region UI

    /* Afficher l'UI à l'écran */
    void DisplayUI()
    {
        StopCoroutine("FadeOutUI");
        GameObject.Find("--------------- UI ---------------")._Find("pnlStats").GetComponent<CanvasGroup>().alpha = 1;
    }

    /* Faire disparaître l'UI progressivement */
    IEnumerator FadeOutUI()
    {
        CanvasGroup UI = GameObject.Find("pnlStats").GetComponent<CanvasGroup>();
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
