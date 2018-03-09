using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NomDuJeu2D.Util;
using Player.Input;

namespace Player
{
    namespace Stats
    {
        public class PlayerStat : MonoBehaviour
        {
                // ENUMS

            private enum UIUpdate
            {
                Health,
                Energy,
                Hunger,
                Thirst
            }

                // FIELDS

            private int _currentHealth = 100;
            private int _maxHealth = 100;
            private int _currentHunger = 100;
            private int _maxHunger = 100;
            private int _currentThirst = 100;
            private int _maxThirst = 100;
            private int _currentEnergy = 100;
            private int _maxEnergy = 100;

            private bool _stopNaturalRegen = false;

            private long _naturalRegenCounter = 0;
            private float _timeBeforeNaturalRegen = 1F;
            private float _timeBeforeHungerDecrease = 3F;
            private float _timeBeforeThirstDecrease = 2F;

            private Image _HUDHealth;
            private Text _HUDHealthValue;
            private Image _HUDEnergy;
            private Text _HUDEnergyValue;
            private Image _HUDHunger;
            private Text _HUDHungerValue;
            private Image _HUDThirst;
            private Text _HUDThirstValue;

            private bool _canBeHit = true;

            private IEnumerator naturalRegen;

            private int _baseAttackValue = 10;
            public float AdditionalPercentageAttackValue { get; set; }
            public int AttackValue { get { return (int)(_baseAttackValue * (AdditionalPercentageAttackValue / 100)); } }

            private int _baseDefenceValue = 4;
            public float AdditionalPercentageDefenceValue { get; set; }
            public int DefenceValue { get { return (int)(_baseDefenceValue * (AdditionalPercentageDefenceValue / 100)); } }

                // METHODS

            private void Awake()
            {
                GameObject HUD = GameObject.Find("----------- HUD -----------");

                _HUDHealth = HUD._Find("health").GetComponent<Image>();
                _HUDHealthValue = HUD._Find("healthText").GetComponent<Text>();
                _HUDEnergy = HUD._Find("energy").GetComponent<Image>();
                _HUDEnergyValue = HUD._Find("energyText").GetComponent<Text>();
                _HUDHunger = HUD._Find("hunger").GetComponent<Image>();
                _HUDHungerValue = HUD._Find("hungerText").GetComponent<Text>();
                _HUDThirst = HUD._Find("thirst").GetComponent<Image>();
                _HUDThirstValue = HUD._Find("thirstText").GetComponent<Text>();

                _HUDHealthValue.text = _currentHealth + " / " + _maxHealth + " " + Lang.GetString("ui.stat.health");
                _HUDEnergyValue.text = _currentEnergy + " / " + _maxEnergy + " " + Lang.GetString("ui.stat.energy");
                _HUDHungerValue.text = _currentHunger + " / " + _maxHunger + " " + Lang.GetString("ui.stat.hunger");
                _HUDThirstValue.text = _currentThirst + " / " + _maxThirst + " " + Lang.GetString("ui.stat.thirst");

                AdditionalPercentageAttackValue = 100;
                AdditionalPercentageDefenceValue = 100;

                naturalRegen = NaturalRegen();
                StartCoroutine(naturalRegen);
            }

            // TODO: Don't regen' in a combat ?
            private IEnumerator NaturalRegen()
            {
                while (!_stopNaturalRegen)
                {
                    yield return new WaitForSecondsRealtime(1F);
                    _naturalRegenCounter++;
                    if (_naturalRegenCounter % _timeBeforeNaturalRegen == 0 && _currentHealth < _maxHealth)
                    {
                        _currentHealth++;
                        UpdateUI(UIUpdate.Health);
                    }

                    if (_naturalRegenCounter % _timeBeforeHungerDecrease == 0)
                    {
                        _currentHunger--;
                        UpdateUI(UIUpdate.Hunger);
                    }
                    if (_naturalRegenCounter % _timeBeforeThirstDecrease == 0)
                    {
                        _currentThirst--;
                        UpdateUI(UIUpdate.Thirst);
                    }
                }
            }

            // TODO: The namespace player AND AIDefaultBehaviour should be contained in a larger namespace, which contains ILivingBeing
            // TODO: Add a sound which depends of who hits the player
            public void GotHit(int damage)
            {
                if (_canBeHit)
                {
                    _canBeHit = false;
                    _currentHealth = _currentHealth - Mathf.Max(damage - DefenceValue, 1);
                    UpdateUI(UIUpdate.Health);

                    if (_currentHealth <= 0)
                    {
                        StopAllCoroutines();
                        StartCoroutine(Die());
                    }
                    else
                        StartCoroutine(StartBlinking());
                }
            }

            /* Blinks the player to show that he got hit */
            private IEnumerator StartBlinking()
            {
                // An enemy can no longer hits the player

                Physics2D.IgnoreLayerCollision(9, 8, true);

                // Blinks the player

                float timeLeft = 1F;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                while (timeLeft > 0)
                {
                    spriteRenderer.color = new Color(1, 0.5F, 0.5F);
                    yield return new WaitForSeconds(0.1F);
                    spriteRenderer.color = Color.white;
                    yield return new WaitForSeconds(0.1F);
                    timeLeft -= 0.2F;
                }

                // An enemy can now hits the player again

                Physics2D.IgnoreLayerCollision(9, 8, false);
                _canBeHit = true;
            }

            // TODO: Insta reload ?
            // TODO: A class Die ?
            private IEnumerator Die()
            {
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<Controller>());
                Destroy(GetComponent<Jump>());

                foreach (Collider2D col in GetComponents<Collider2D>())
                    Destroy(col);

                Destroy(GetComponent<Collider2D>());

                GameObject deathGO = GameObject.Find("death");
                deathGO.transform.GetChild(0).gameObject.SetActive(true);

                Image deathImg = deathGO.GetComponent<Image>();
                Text deathTxt = deathGO.GetComponentInChildren<Text>();

                deathTxt.text = Lang.GetString("player.death");

                    // A fade-in black screen

                int remainingTime = 255;
                while (remainingTime > 0)
                {
                    deathImg.color = new Color(0, 0, 0, deathImg.color.a + 2 / 255F);
                    remainingTime -= 2;

                    if (remainingTime % 3 == 0)
                        deathTxt.fontSize++;

                    yield return new WaitForSeconds(0.02F);
                }

                    // Black screen for 2 secs for reloading the game

                yield return new WaitForSeconds(2F);

                // TODO: Insta reload ?
                MasterClass.InitializeTheGame();
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                MasterClass.InitializeAwakes();
            }

            public void UpdateHunger(int hungerValue)
            {
                _currentHunger = Mathf.Min(_maxHunger, _currentHunger + hungerValue);
                UpdateUI(UIUpdate.Hunger);
            }

            public void UpdateThirst(int thirstValue)
            {
                _currentThirst = Mathf.Min(_maxThirst, _currentThirst + thirstValue);
                UpdateUI(UIUpdate.Thirst);
            }

            /* Update stats on screen, used after any modification of stats */
            private void UpdateUI(UIUpdate UIUpdate)
            {
                switch (UIUpdate)
                {
                    case UIUpdate.Health:
                        _HUDHealthValue.text = _currentHealth + " / " + _maxHealth + " " + Lang.GetString("ui.stat.health");
                        _HUDHealth.color = new Color(1 - ((float)_currentHealth / _maxHealth), ((float)_currentHealth / _maxHealth), 0);
                        break;
                    case UIUpdate.Energy:
                        _HUDEnergyValue.text = _currentEnergy + " / " + _maxEnergy + " " + Lang.GetString("ui.stat.energy");
                        _HUDEnergy.color = new Color(1 - ((float)_currentEnergy / _maxEnergy), ((float)_currentEnergy / _maxEnergy), 0);
                        break;
                    case UIUpdate.Hunger:
                        _HUDHungerValue.text = _currentHunger + " / " + _maxHunger + " " + Lang.GetString("ui.stat.hunger");
                        _HUDHunger.color = new Color(1 - ((float)_currentHunger / _maxHunger), ((float)_currentHunger / _maxHunger), 0);
                        break;
                    case UIUpdate.Thirst:
                        _HUDThirstValue.text = _currentThirst + " / " + _maxThirst + " " + Lang.GetString("ui.stat.thirst");
                        _HUDThirst.color = new Color(1 - ((float)_currentThirst / _maxThirst), ((float)_currentThirst / _maxThirst), 0);
                        break;
                }
            }
        }
    }
}