using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LivingBeing.Player.Stats;
using Items.Objects;

namespace LivingBeing
{
    namespace Player
    {
        namespace Inputt
        {
            public class PlayerAction : MonoBehaviour
            {
                    // FIELDS

                private PlayerStat _playerStat;

                    // METHODS

                private void Awake()
                {
                    _playerStat = GetComponent<PlayerStat>();
                }

                /* Eat food in inventory at index indexInInventory */
                public void Eat(Food food)
                {
                    _playerStat.UpdateHunger(food.hungerValue);
                    _playerStat.UpdateThirst(food.thirstValue);
                }
            }
        }
    }
}