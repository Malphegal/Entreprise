﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    namespace Objects
    {
            // TODO: Don't make this class static, and and a Tool panel which contains this class
        public static class ToolManagement
        {
                // STATIC FIELDS

            private static byte _nbOfTools = 0;
            private static bool[] _myTools;

                // PROPERTIES

            public static int ToolIndex { get { return _nbOfTools - 1; } }

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

                    // Add the tool in the static array

                _myTools[(int)toolToAdd.typeOfTool] = true;
                _nbOfTools++;

                return true;
            }
        }
    }
}