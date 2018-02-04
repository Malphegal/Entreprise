using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillTree {

        // FIELDS

    private static SkillTreeBranch[] _allBranches;

        // STATIC METHODS

        // TODO:
    public static void InitSkillTree()
    {
        _allBranches = new SkillTreeBranch[]
        {
            new SkillTreeBranch(10),
            new SkillTreeBranch(10),
            new SkillTreeBranch(10)
        };
    }

}
