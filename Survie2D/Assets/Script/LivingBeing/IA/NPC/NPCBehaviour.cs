using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LivingBeing
{
    namespace AI
    {
        namespace NPC
        {
            public class NPCBehaviour : AIDefaultBehaviour
            {
                protected override IEnumerator Die()
                {
                    Destroy(gameObject);
                    yield return null;
                }

                protected override void AI()
                {
                    return;
                }
            }
        }
    }
}