using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA.Behaviour;

namespace IA
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