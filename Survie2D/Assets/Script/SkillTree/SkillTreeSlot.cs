using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeSlot : MonoBehaviour {

        // FIELDS

    private int _id;

    public string skillName;

    public bool Unlocked { get; private set; }

        // METHODS

    private void Awake()
    {
        _id = int.Parse(name.Split('t')[1]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
                // Unlock this skill
            
            GetComponentInParent<SkillTree>().AllUnlocks(_id, skillName);

                // Display the image

            GetComponent<UnityEngine.UI.Image>().color = Color.white;

                // Disable this component

            Unlocked = true;
            enabled = false;
        }
    }
}
