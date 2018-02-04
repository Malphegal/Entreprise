using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

        // FIELDS

    private GameObject _characterSheet;
    private Inventory _inventory;

    private Controller _controller;

    private bool _inCharacterSheet  = false;
    private bool _inInventory       = false;

    private void Awake()
    {
        GameObject HUD = GameObject.Find("----------- HUD -----------");
        _controller = GetComponent<Controller>();

        _characterSheet = HUD._Find("characterSheet")._Find("characterSheet_Panel");
        _inventory = HUD._Find("inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
            // -------- Character sheet --------

        if (Input.GetKeyDown(KeyCode.C))
        {
            _characterSheet.SetActive(_inCharacterSheet ^= true);
        }

            // -------- Inventory --------

        if (Input.GetKeyDown(KeyCode.I))
        {
            _controller.enabled = !(_inventory.enabled = _inInventory ^= true);
        }

            // -------- DEBUG --------

        if (Input.GetKeyDown(KeyCode.M))
            GetComponent<PlayerStat>().GotHit(Random.Range(4, 9));


            // DEBUG: Move it to the right class
        if (Input.GetKeyDown(KeyCode.N))
            StartCoroutine(Attack());
    }

    // TODO: Move it to the right class
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

}
