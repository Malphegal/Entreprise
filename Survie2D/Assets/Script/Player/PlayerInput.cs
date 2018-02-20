using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

        // FIELDS

    private GameObject _characterSheet;
    private GameObject _skillTree;
    private Inventory _inventory;
    private Controller _controller;

    private static bool _inCharacterSheet  = false;
    private static bool _inInventory       = false;
    private static bool _inSkillTree       = false;

    public static bool AvailableForNewMenu {
        get { return !(_inInventory || _inSkillTree || _inCharacterSheet); } } // Can't open another window if a menu if already opened

    public static bool RuneMode {
        get { return _inCharacterSheet; } }

    private bool _attacking = false;

    // TODO: Another class ?
    public GameObject arrowProjectile;

    private void Awake()
    {
        GameObject HUD = GameObject.Find("----------- HUD -----------");

        _controller     = GetComponent<Controller>();
        _characterSheet = HUD._Find("characterSheet")._Find("characterSheet_Panel");
        _inventory      = HUD._Find("inventory").GetComponent<Inventory>();
        _skillTree      = HUD._Find("skillTree_Panel");
    }
    
    private void Update()
    {
            // -------- Character sheet --------

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (AvailableForNewMenu && !_inCharacterSheet)
                _characterSheet.SetActive(!(_controller.enabled = !(_inCharacterSheet = true)));
            else if (_inCharacterSheet)
                _characterSheet.SetActive(!(_controller.enabled = !(_inventory.enabled = _inCharacterSheet = false)));
        }

            // -------- Inventory --------

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (AvailableForNewMenu && !_inInventory)
                _controller.enabled = !(_inventory.enabled = _inInventory = true);
            else if (_inInventory)
                _controller.enabled = !(_inventory.enabled = _inInventory = false);
        }

            // -------- SkillTree --------

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (AvailableForNewMenu && !_inSkillTree)
                _skillTree.SetActive(!(_controller.enabled = !(_inSkillTree = true)));
            else if (_inSkillTree)
                _skillTree.SetActive(!(_controller.enabled = !(_inSkillTree = false)));
        }

            // -------- DEBUG --------

        if (Input.GetKeyDown(KeyCode.M))
            GetComponent<PlayerStat>().GotHit(Random.Range(30, 60));

            // TODO: Move it to the right class
        if (Input.GetKeyDown(KeyCode.N) && !_attacking)
            StartCoroutine(Attack());

            // TODO: Move it to the right class
        if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(RangedAttack());

            // DEBUG: Remove it !
        if (Input.GetKeyDown(KeyCode.L))
            _skillTree.GetComponent<SkillTree>().SkillTreeCoins += 1;
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
        Instantiate(arrowProjectile, new Vector3(player.transform.position.x + (player.GetComponent<Controller>().IsFacingRight ? 0.5F : -0.5F)
            , player.transform.position.y, player.transform.position.z), new Quaternion());
    }
}