using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private string      _npcName;
    private string[]    _dialogues;
    public int          numberOfDialogues;
    private int         _currentDialogue = -1;
    private float       _remaningTime = 3F;
    private bool        _coroutineIsRunning = false;

    TextMesh textMesh;

    public void Awake()
    {
        _npcName = GetComponentInParent<NPC>().nameOfEnemy;
        _dialogues = new string[numberOfDialogues];

        for (int i = 0; i < numberOfDialogues; i++)
            _dialogues[i] = Lang.GetString("npc.dialogue." + _npcName + "." + i);

        GameObject floatingObject = new GameObject();
        textMesh = floatingObject.AddComponent<TextMesh>();
        textMesh.offsetZ = -1;
        textMesh.characterSize = 0.07F;
        textMesh.fontSize = 40;
        textMesh.color = Color.red;
        floatingObject.name = "floatingDialogue_" + _npcName;
        floatingObject.transform.parent = transform;
        floatingObject.transform.localPosition = new Vector3(0, 1.5F, 1);
        StartCoroutine(RemoveFloatingText());
    }

    private string GetNextDialogue()
    {
        return _dialogues[_currentDialogue = (_currentDialogue + 1) % numberOfDialogues]; // à tester
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && Input.GetKeyDown(KeyCode.E))
        {
            textMesh.text = GetNextDialogue();
            if (!_coroutineIsRunning)
            {
                _coroutineIsRunning = true;
                StartCoroutine(RemoveFloatingText());
            }
            _remaningTime = 3F;
        }
    }

    // TODO: Each NPC has its own thread ?
    private IEnumerator RemoveFloatingText()
    {
        while (_coroutineIsRunning)
        {
            yield return new WaitForSeconds(0.1F);
            _remaningTime = Mathf.Max(_remaningTime - 0.1F, 0);
            if (_remaningTime == 0)
            {
                _coroutineIsRunning = false;
                textMesh.text = string.Empty;
            }
        }
    }
}