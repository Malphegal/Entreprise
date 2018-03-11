using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items.InventoryManagement;
using NomDuJeu2D.Util;
using LivingBeing.Player.Skill;

namespace LivingBeing
{
    namespace Player
    {
        namespace Inputt
        {
            public class PlayerInput : MonoBehaviour
            {
                    // FIELDS

                private GameObject _characterSheet;
                private GameObject _skillTree;
                private Inventory _inventory;
                private Controller _controller;

                // TODO: Another class ?
                public GameObject arrowProjectile;

                private static bool _inCharacterSheet = false;
                private static bool _inInventory = false;
                private static bool _inSkillTree = false;

                public static bool AvailableForNewMenu
                {
                    get { return !(_inInventory || _inSkillTree || _inCharacterSheet); }
                } // Can't open another window if a menu if already opened

                public static bool RuneMode
                {
                    get { return _inCharacterSheet; }
                } // Change the behaviour of the inventory if true

                private bool _canAttack = true;

                    // PROPERTIES

                public bool CanMove { get { return _canAttack; } }

                    // METHODS

                private void Awake()
                {
                    GameObject HUD = GameObject.Find("----------- HUD -----------");

                    _controller = GetComponent<Controller>();
                    _characterSheet = HUD._Find("characterSheet")._Find("characterSheet_Panel");
                    _inventory = HUD._Find("inventory").GetComponent<Inventory>();
                    _skillTree = HUD._Find("skillTree_Panel");
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
                        GetComponent<ILivingBeing>().GotHit(Random.Range(30, 60));

                    // TODO: Move it to the right class
                    if (Input.GetKeyDown(KeyCode.N) && _canAttack)
                        StartCoroutine(Attack());

                    // TODO: Move it to the right class
                    if (Input.GetKeyDown(KeyCode.B))
                        StartCoroutine(RangedAttack());

                    // DEBUG: Remove it !
                    if (Input.GetKeyDown(KeyCode.L))
                        _skillTree.GetComponent<SkillTree>().SkillTreeCoins += 1; // TODO: Make a static method to increment it
                }

                // TODO: Move it to the right class
                /* Attack with the melee weapon */
                private IEnumerator Attack()
                {
                    _canAttack = false;

                    Vector2 vec = GetComponent<Rigidbody2D>().velocity;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(vec.x / 3, vec.y);

                    Transform arme = gameObject._Find("weapon").transform;

                    arme.GetComponent<PolygonCollider2D>().enabled = true;

                    const float c_decresedTime = 0.00125F;
                    const float c_timeLeft = 0.012F;
                    const float c_translateValue = 0.05F;

                    float timeLeft = c_timeLeft;
                    while (timeLeft > 0)
                    {
                        arme.Translate(_controller.IsFacingRight ? c_translateValue : -c_translateValue, 0, 0, Space.World);
                        timeLeft -= c_decresedTime;
                        yield return new WaitForSeconds(c_decresedTime);
                    }

                    timeLeft = c_timeLeft;
                    while (timeLeft > 0)
                    {
                        arme.Translate(_controller.IsFacingRight ? -c_translateValue : c_translateValue, 0, 0, Space.World);
                        timeLeft -= c_decresedTime;
                        yield return new WaitForSeconds(c_decresedTime);
                    }

                    arme.GetComponent<PolygonCollider2D>().enabled = false;

                    _canAttack = true;
                }

                // TODO: Move it to the right class
                /* Prepare to shoot an arrow */
                private IEnumerator RangedAttack()
                {
                    float timeLeft = 1F;
                    while (timeLeft > 0)
                    {
                        yield return new WaitForSeconds(0.1F);
                        if (!UnityEngine.Input.GetKey(KeyCode.B))
                            yield break;
                        timeLeft -= 0.1F;
                    }

                    while (UnityEngine.Input.GetKey(KeyCode.B))
                        yield return new WaitForEndOfFrame();

                    Arrow();
                }

                /* Shoot an arrow */
                private void Arrow()
                {
                    Instantiate(arrowProjectile, new Vector3(gameObject.transform.position.x + (gameObject.GetComponent<Controller>().IsFacingRight ? 0.5F : -0.5F)
                        , gameObject.transform.position.y, gameObject.transform.position.z), new Quaternion());
                }
            }
        }
    }
}