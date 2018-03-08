using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Input;
using NomDuJeu2D.Util;
using Items.Objects;
using Items.InventoryManagement;

namespace Environnement
{
    namespace Bridge
    {
        public class CanBuildBridge : MonoBehaviour
        {
                // ENUMS

            private enum ResourcesIndex
            {
                wood,
                straw
            }

                // FIELDS

            public GameObject[] allBridges;
            private GameObject _firstChild;
            private GameObject[] _allNewBridge;

            private Controller _controller;
            //private Features.Inventory _inventory;

            private bool _firstFrameSkiped = false;
            private int _currentSelectedBridge = 0;
            private bool _canChangeCurrentBridgeSelected = true;

            private bool _isInChoicePhase = false;
            private bool _isInBuildPhase = false;

            private bool _playerInRange = false;

            private int[] _currentResourcesRequired;
            public Sprite[] allResourcesSprites;

            public int totalNumberOfRequiredItems; // ProgressBar
            public int totalNumberOfRequiredItemsLeft; // ProgressBar

                // METHODS

            private void Awake()
            {
                _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
                _firstChild = transform.GetChild(0).gameObject;

                _allNewBridge = new GameObject[allBridges.Length];
                for (int i = 0; i < allBridges.Length; i++)
                {
                    _allNewBridge[i] = new GameObject("bridge_" + i);
                    _allNewBridge[i].transform.SetParent(_firstChild.transform);
                    _allNewBridge[i].transform.localScale = new Vector3(0.4F, 0.5F, 1);
                    _allNewBridge[i].transform.localPosition = new Vector3(4 + i * 12, 0.5F, 0);

                    SpriteRenderer sr = _allNewBridge[i].AddComponent<SpriteRenderer>();
                    sr.sprite = allBridges[i].GetComponent<SpriteRenderer>().sprite;
                    sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                }
            }

            // TODO: TEST CONSTRUCTION
            float test = 0F;
            private void Update()
            {
                if (PlayerInput.AvailableForNewMenu)
                {
                        // -- TEST CONSTRUCTION --

                    if (Input.GetKey(KeyCode.J) && _playerInRange)
                    {
                        Vector3 vec3 = gameObject._Find("progressBar_Construction").transform.localScale;

                        if ((gameObject._Find("progressBar_Resource").transform.localScale.x) <= vec3.x)
                            return;

                        vec3 = new Vector3((test += 0.001F) / 10F, vec3.y, vec3.z);

                        if (vec3.x > gameObject._Find("progressBar_Resource").transform.localScale.x)
                            vec3.x = gameObject._Find("progressBar_Resource").transform.localScale.x;

                        if (vec3.x >= 0.1F)
                            BridgeBuilt();

                        gameObject._Find("progressBar_Construction").transform.localScale = vec3;
                        gameObject._Find("percentConstruction").GetComponent<TextMesh>().text = Mathf.RoundToInt(vec3.x * 1000) + " %";

                        return;
                    }

                        // ---------------- SELECT THE BRIDGE ----------------

                    if (_firstFrameSkiped && _isInChoicePhase && !_isInBuildPhase)
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            UnselectInChoicePhase();
                        }

                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            if (_currentSelectedBridge == 0 || !_canChangeCurrentBridgeSelected)
                                return;

                            _currentSelectedBridge--;
                            if (_canChangeCurrentBridgeSelected)
                            {
                                _canChangeCurrentBridgeSelected = false;
                                StartCoroutine(Translate(true));
                            }
                        }

                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            if (_currentSelectedBridge == _allNewBridge.Length - 1 || !_canChangeCurrentBridgeSelected)
                                return;

                            _currentSelectedBridge++;
                            if (_canChangeCurrentBridgeSelected)
                            {
                                _canChangeCurrentBridgeSelected = false;
                                StartCoroutine(Translate(false));
                            }
                        }

                        if (Input.GetKeyDown(KeyCode.E) && _canChangeCurrentBridgeSelected)
                        {
                            UnselectInChoicePhase();
                            EnableUnderConstruction();
                            return;
                        }
                    }

                        // ---------------- SEND RESOURCES TO BUILD THE BRIDGE ----------------

                    if (_isInBuildPhase && Input.GetKeyDown(KeyCode.E) && !_isInChoicePhase)
                    {
                        Item[] allItemsRequired = GetComponentsInChildren<Item>(true);
                        for (int i = 0; i < allItemsRequired.Length; i++)
                        {
                                // If the current item is not required anymore

                            if (!allItemsRequired[i].gameObject.activeInHierarchy)
                                continue;

                                // Remove the current required item

                            //int remainingQuantity = _inventory.RemoveItem(allItemsRequired[i], _currentResourcesRequired[i]);
                            int remainingQuantity = Inventory.RemoveItem(allItemsRequired[i], _currentResourcesRequired[i]);
                            totalNumberOfRequiredItemsLeft -= _currentResourcesRequired[i] - remainingQuantity; // Total number of remaining items
                            _currentResourcesRequired[i] = remainingQuantity; // Remaining number of the current object

                                // Move the progressBar

                            Vector3 vec3 = gameObject._Find("progressBar_Resource").transform.localScale;
                            vec3 = new Vector3(((totalNumberOfRequiredItems - totalNumberOfRequiredItemsLeft) / (float)totalNumberOfRequiredItems) / 10F, vec3.y, vec3.z);
                            gameObject._Find("progressBar_Resource").transform.localScale = vec3;
                            gameObject._Find("percentResource").GetComponent<TextMesh>().text = Mathf.RoundToInt(vec3.x * 1000) + " %";

                                // If this Item is not required anymore

                            if (remainingQuantity == 0)
                            {
                                allItemsRequired[i].transform.parent.gameObject.SetActive(false);

                                for (int j = i + 1; j < allItemsRequired.Length; j++)
                                    allItemsRequired[j].transform.parent.Translate(new Vector3(-1, 0, 0));
                            }
                            else
                                allItemsRequired[i].transform.parent.GetComponentInChildren<TextMesh>().text = remainingQuantity.ToString();
                        }
                    }

                        // ---------------- WINDOW NOT DISPLAYED ----------------

                    if (_playerInRange && PlayerInput.AvailableForNewMenu && Input.GetKeyDown(KeyCode.E) && !_isInChoicePhase && !_isInBuildPhase)
                        SelectInChoicePhase();
                }
            }

            private IEnumerator Translate(bool right)
            {
                float timeLeft = 4;
                while (timeLeft > 0)
                {
                    yield return new WaitForSeconds(0.01F);
                    timeLeft -= 0.15F;
                    for (int i = 0; i < _allNewBridge.Length; i++)
                        _allNewBridge[i].transform.Translate(new Vector3(right ? ((_currentSelectedBridge + 1) < _allNewBridge.Length ? 0.44F : 0) : _currentSelectedBridge > 0 ? -0.44F : 0, 0, 0));
                }
                _canChangeCurrentBridgeSelected = true;
            }

            private void UnselectInChoicePhase()
            {
                _firstChild.SetActive(_firstFrameSkiped = false);
                _currentSelectedBridge = 0;
                _firstFrameSkiped = false;
                _controller.enabled = true;
                _isInChoicePhase = false;

                // Reset bridges

                for (int i = 0; i < _allNewBridge.Length; i++)
                    _allNewBridge[i].transform.localPosition = new Vector3(i * 4, 0, 0);
            }

            private void SelectInChoicePhase()
            {
                _isInChoicePhase = true;
                _firstChild.SetActive(true);
                _controller.enabled = false;
                StartCoroutine(WaitAFrame());
            }

            // TODO: Change Update() order instead ?
            /* To choose a bridge => E key, but to enable this component => E key aswell */
            private IEnumerator WaitAFrame()
            {
                yield return new WaitForEndOfFrame();
                _firstFrameSkiped = true;
            }

            private void EnableUnderConstruction()
            {
                _canChangeCurrentBridgeSelected = false;
                _isInBuildPhase = true;

                transform.GetChild(1).gameObject.SetActive(true); // construction_sign
                transform.GetChild(2).gameObject.SetActive(true); // required_background

                // TODO: Add the right amount of resources for the selected bridge

                ResourcesRequired rr = allBridges[_currentSelectedBridge].GetComponent<ResourcesRequired>();
                _currentResourcesRequired = new int[rr.AllResources.Length];
                for (int i = 0; i < rr.AllResources.Length; i++)
                    _currentResourcesRequired[i] = rr.AllResources[i];

                GameObject panel0 = gameObject._Find("panel0"); // original panel, the one for wood
                gameObject._Find("amount").GetComponent<TextMesh>().text = _currentResourcesRequired[0].ToString();

                totalNumberOfRequiredItems = _currentResourcesRequired[0]; // ProgressBar

                int newPanelCounter = 0; // x position of the new panels
                GameObject required_background = gameObject._Find("required_background"); // background which store all panels

                // Check if other resources is required to build this bridge

                for (int i = 1; i < allResourcesSprites.Length; i++) // i = 1, because wood is already done
                    if (_currentResourcesRequired[i] > 0)
                    {
                        GameObject resourceCounter = Instantiate(panel0, required_background.transform);
                        resourceCounter._Find("item").GetComponent<SpriteRenderer>().sprite = allResourcesSprites[i];

                        // Careful ! The item will be known with his name, but not with other fields !
                        resourceCounter._Find("item").GetComponent<Item>().itemName = "item.resource." + (ResourcesIndex)i;

                        resourceCounter._Find("amount").GetComponent<TextMesh>().text = _currentResourcesRequired[i].ToString();
                        resourceCounter.transform.localPosition = new Vector3(-1.45F + ++newPanelCounter, 0.27F, 0);

                        totalNumberOfRequiredItems += _currentResourcesRequired[i]; // ProgressBar
                    }

                // Enable the progress bar

                totalNumberOfRequiredItemsLeft = totalNumberOfRequiredItems;
                gameObject._Find("progressBar").SetActive(true);

                // Destroy unused components

                Destroy(GetComponent<ParticleSystem>());
                Destroy(_firstChild); // Selection panel of a bridge
            }

            /* Remove all unnecessary components, including this, and build the selected bridge */
            private void BridgeBuilt()
            {
                Destroy(GetComponent<CapsuleCollider2D>());

                GameObject finalBridge = Instantiate(allBridges[_currentSelectedBridge], transform);
                finalBridge.transform.localPosition = new Vector3(4, 0, 0);
                finalBridge.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;

                Destroy(transform.GetChild(2).gameObject); // The first panel which represent one kind of item required
                Destroy(transform.GetChild(1).gameObject); // contruction_sign
                Destroy(transform.GetChild(0).gameObject); // glass_background

                GameObject bridge = Instantiate(allBridges[0], transform);
                bridge.transform.localPosition = new Vector3(10, 0, 0);

                Destroy(this);
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.layer == 9)
                    _playerInRange = true;
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
                if (collision.gameObject.layer == 9)
                    _playerInRange = false;
            }
        }
    }
}