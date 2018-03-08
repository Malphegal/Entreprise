using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Items
{
    namespace Objects
    {
        public static class ToolManagement
        {
                // STATIC FIELDS

            private static byte _nbOfTools = 0;
            private static bool[] _myTools;

                // STATIC METHODS

            /* Set all static values */
            public static void InitTools()
            {
                _nbOfTools = 0;
                _myTools = new bool[] { false, false, false, false, false };
            }

            /* Return true if toolToAdd has been added */
            public static bool AddTool(Tool toolToAdd)
            {
                    // Don't add the same tool twice

                if (_myTools[(int)toolToAdd.typeOfTool])
                    return false;

                _myTools[(int)toolToAdd.typeOfTool] = true;
                _nbOfTools++;
                return true;
            }
        }
    }
}