using System.Collections;
using System.Collections.Generic;
using Unity.Engine;

public class SkillsTree : MonoBehaviour
{
    public string name;
    public int NumberOfCases { get { return _allCases.Count(); } }

    private List<SkillsTreeCase> _allCases;
    private SkillsTreeCase _originCase;
}