using System.Collections;
using System.Collections.Generic;
using Unity.Engine;

public class SkillsTreeCase : MonoBehaviour
{
    public string name;
    public string description;
    public bool unlocked;

    private List<SkillsTreeCase> _children;

    public void UnlockSkill()
    {
        unlocked = true;
    }
}