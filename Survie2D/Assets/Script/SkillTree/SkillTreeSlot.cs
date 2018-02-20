using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeSlot : MonoBehaviour {

        // FIELDS

    private int _id;

    public string skillName;
    public int price;

    private SkillTree _skillTree;

    public bool Unlocked { get; private set; }

        // METHODS

    private void Awake()
    {
        _id = int.Parse(name.Split('t')[1]);
        _skillTree = GetComponentInParent<SkillTree>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _skillTree.SkillTreeCoins >= price)
        {
                // Unlock this skill
            
            _skillTree.AllUnlocks(_id, skillName);
            _skillTree.SkillTreeCoins -= price;

                // Display the image

            GetComponent<UnityEngine.UI.Image>().color = Color.white;

                // Disable this component

            Unlocked = true;
            enabled = false;
        }
    }
}
