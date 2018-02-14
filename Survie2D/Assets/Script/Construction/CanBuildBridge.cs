using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Inventory _inventory;

    private bool _firstFrameSkiped = false;
    private int _currentSelectedBridge = 0;
    private bool _canChangeCurrentBridgeSelected = true;

    private bool _isInChoicePhase   = false;
    private bool _isInBuildPhase    = false;

    private bool _playerInRange     = false;

    private int[] _currentResourcesRequired;
    public Sprite[] allResourcesSprites;

        // METHODS

    private void Awake()
    {
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        _inventory = GameObject.Find("----------- HUD -----------")._Find("inventory").GetComponent<Inventory>();
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

        int newPanelCounter = 0; // x position of the new panels
        GameObject required_background = gameObject._Find("required_background"); // background which store all panels

            // Check if straw is required to build this bridge

        for (int i = 1; i < allResourcesSprites.Length; i++) // i = 1, because wood is already done
            if (_currentResourcesRequired[i] > 0)
            {
                GameObject resourceCounter = Instantiate(panel0, required_background.transform);
                resourceCounter._Find("item").GetComponent<SpriteRenderer>().sprite = allResourcesSprites[i];

                    // Careful ! The item will be known with his name, but not with other fields !
                resourceCounter._Find("item").GetComponent<Item>().itemName = "item.resource." + (ResourcesIndex)i;

                resourceCounter._Find("amount").GetComponent<TextMesh>().text = _currentResourcesRequired[i].ToString();
                resourceCounter.transform.localPosition = new Vector3(-1.45F + ++newPanelCounter, 0.27F, 0);
            }

            // Destroy unused components

        Destroy(GetComponent<ParticleSystem>());
        Destroy(_firstChild);
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

    // TODO: Move all panels on the right of the current panel
    private void Update()
    {
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

        if (_isInBuildPhase && Input.GetKeyDown(KeyCode.E) && !_isInChoicePhase)
        {
            Item[] allItemsRequired = GetComponentsInChildren<Item>(true);
            for (int i = 0; i < allItemsRequired.Length; i++)
            {
                    // If the current item is not required anymore

                if (!allItemsRequired[i].gameObject.activeInHierarchy)
                    continue;

                    // Remove the current required item

                int remainingQuantity = _inventory.RemoveItem(allItemsRequired[i], _currentResourcesRequired[i]);
                _currentResourcesRequired[i] = remainingQuantity;

                    // If this Item is not required anymore

                if (remainingQuantity == 0)
                {
                    allItemsRequired[i].transform.parent.gameObject.SetActive(false);
                        // TODO: Move all panels on the right of the current panel
                    for (int j = 0; j < allItemsRequired.Length; j++)
                        if (allItemsRequired[j].gameObject.activeInHierarchy)
                            return;

                    BridgeBuilt();
                    return;
                }

                allItemsRequired[i].transform.parent.GetComponentInChildren<TextMesh>().text = remainingQuantity.ToString();
            }
        }

        if (_playerInRange && !PlayerInput.InInventory && Input.GetKeyDown(KeyCode.E) && !_isInChoicePhase && !_isInBuildPhase)
            SelectInChoicePhase();
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