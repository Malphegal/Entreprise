using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeBuilt : MonoBehaviour {

    private SpriteRenderer _bridge;
    private GameObject _ZONE;
    private SpriteRenderer _CHOIX1;
    private SpriteRenderer _CHOIX2;
    private GameObject _ARROW;
    private int arrowCounter = 0;

    private bool isInWindow = false;

    private void Update()
    {
        if (isInWindow)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {

                if ((arrowCounter = (arrowCounter + 1) % 2) == 0)
                {
                    _bridge.sprite = _CHOIX1.sprite;
                    _ARROW.transform.position = new Vector3(_ARROW.transform.position.x, 0 + 0.7221F, _ARROW.transform.position.z);
                }
                else
                {
                    _bridge.sprite = _CHOIX2.sprite;
                    _ARROW.transform.position = new Vector3(_ARROW.transform.position.x, -0.5F + 0.7221F, _ARROW.transform.position.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if ((arrowCounter = arrowCounter - 1 == -1 ? 1 : 0) == 0)
                {
                    _bridge.sprite = _CHOIX1.sprite;
                    _ARROW.transform.position = new Vector3(_ARROW.transform.position.x, 0, _ARROW.transform.position.z);
                }
                else
                {
                    _bridge.sprite = _CHOIX2.sprite;
                    _ARROW.transform.position = new Vector3(_ARROW.transform.position.x, -0.5F, _ARROW.transform.position.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("player").GetComponent<Controller>().enabled = true;
                GameObject.Find("player").GetComponent<Jump>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                _bridge.color = Color.white;
                Destroy(_ZONE);
                Destroy(GetComponent<ParticleSystem>());
                Destroy(this);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isInWindow = false;
                _ZONE.SetActive(false);
                arrowCounter = 0;
                GameObject.Find("player").GetComponent<Controller>().enabled = true;
                GameObject.Find("player").GetComponent<Jump>().enabled = true;
                _bridge.color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void Awake()
    {
        _bridge = GetComponent<SpriteRenderer>();
        _ZONE = transform.GetChild(0).gameObject;
        _CHOIX1 = gameObject._Find("CHOIX1").GetComponent<SpriteRenderer>();
        _CHOIX2 = gameObject._Find("CHOIX2").GetComponent<SpriteRenderer>();
        _ARROW = gameObject._Find("ARROW");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && Input.GetKeyDown(KeyCode.E))
        {
            _bridge.color = new Color(1, 1, 1, 0.75F);
            _ZONE.SetActive(true);
            GameObject.Find("player").GetComponent<Controller>().enabled = false;
            GameObject.Find("player").GetComponent<Jump>().enabled = false;
            StartCoroutine(WaitAFrame());
        }
    }

    private IEnumerator WaitAFrame()
    {
        yield return new WaitForEndOfFrame();
        isInWindow = true;
    }
}
