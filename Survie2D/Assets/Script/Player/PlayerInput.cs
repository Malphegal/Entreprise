using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

        // FIELDS

    private Rigidbody2D _rb;

    private GameObject _characterSheet;
    private GameObject _skillTree;
    private Inventory _inventory;
    private Controller _controller;

    private bool _inCharacterSheet  = false;
    private bool _inInventory       = false; public static bool InInventory { get; private set; } // Used by CanBuildBridge
    private bool _inSkillTree       = false;

    private bool _attacking         = false;

    public GameObject arrowProjectile;

    private void Awake()
    {
        GameObject HUD = GameObject.Find("----------- HUD -----------");

        _rb             = GetComponent<Rigidbody2D>();

        _controller     = GetComponent<Controller>();
        _characterSheet = HUD._Find("characterSheet")._Find("characterSheet_Panel");
        _inventory      = HUD._Find("inventory").GetComponent<Inventory>();
        _skillTree      = HUD._Find("skillTree_Panel");
    }

    private void Update()
    {
        if(!_inSkillTree) { 

                // -------- Character sheet --------

            if (Input.GetKeyDown(KeyCode.C))
            {
                _characterSheet.SetActive(_inCharacterSheet ^= true);
            }

                // -------- Inventory --------

            if (Input.GetKeyDown(KeyCode.I))
            {
                _controller.enabled = !(_inventory.enabled = InInventory = _inInventory ^= true);
                _rb.velocity = Vector2.zero;
            }
        }

            // -------- SkillTree --------

        if (Input.GetKeyDown(KeyCode.V))
        {
            _inventory.enabled = InInventory = _inInventory = false;
            _characterSheet.SetActive(_inCharacterSheet = false);
            _skillTree.SetActive(_inSkillTree ^= true);
            _controller.enabled = !_inSkillTree;
        }

            // -------- DEBUG --------

        if (Input.GetKeyDown(KeyCode.M))
            GetComponent<PlayerStat>().GotHit(Random.Range(4, 9));

            // DEBUG: Move it to the right class
        if (Input.GetKeyDown(KeyCode.N) && !_attacking)
            StartCoroutine(Attack());

            // DEBUG: Move it to the right class
        if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(RangedAttack());
    }

    // TODO: Move it to the right class
    /* Attack with the melee weapon */
    private IEnumerator Attack()
    {
        _attacking = true;

        Transform arme = gameObject._Find("weapon").transform;

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

        _attacking = false;

        yield return null;
    }
    
    /* Prepare to shoot an arrow */
    private IEnumerator RangedAttack()
    {
        float timeLeft = 1F;
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(0.1F);
            if (!Input.GetKey(KeyCode.B))
                yield break;
            timeLeft -= 0.1F;
        }

        while (Input.GetKey(KeyCode.B))
            yield return new WaitForEndOfFrame();

        Arrow();
    }

    /* Shoot an arrow */
    private void Arrow()
    {
        GameObject player = GameObject.Find("player");
        GameObject arrow = Instantiate(arrowProjectile, new Vector3(player.transform.position.x + (player.GetComponent<Controller>().IsFacingRight ? 0.5F : -0.5F)
            , player.transform.position.y, player.transform.position.z), new Quaternion());
    }
}