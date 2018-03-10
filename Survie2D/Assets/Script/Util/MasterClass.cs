using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items.Objects;

namespace NomDuJeu2D
{
    namespace Util
    {
        public static class MasterClass
        {
                // FIELDS

            public static string NameOfThePlayer { get; set; }

                // STATIC METHODS

            /* Find a children of a specific GameObject */
            public static GameObject _Find(this GameObject parent, string name)
            {
                Transform[] transforms = parent.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in transforms)
                    if (t.name == name)
                        return t.gameObject;
                return null;
            }

            /* Flip the current transform x */
            public static void FlipTransform(this Transform transform)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            
            // TODO: Choose the right language instead of "French"
            [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
            public static void InitializeTheGame()
            {
                // DEBUG :
                NameOfThePlayer = "Akuumy";

                Lang.DefineLanguage(System.IO.Directory.GetFiles(".", "lang.fr.xml", System.IO.SearchOption.AllDirectories)[0], "French");
            }

            [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
            public static void InitializeAwakes()
            {
                RuneManagement.InitRunes();
                ToolManagement.InitTools();
            }
        }
    }
}