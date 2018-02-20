﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour {
    
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

    public Rune.Color RightHandRune { get; set; }
    public Rune.Color LeftHandRune { get; set; }
    public Rune.Color DistanceRune { get; set; }
    public Rune.Color ClothesRune { get; set; }

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

        _HUDHealthValue.text = _currentHealth + " / " + _maxHealth + " " + Lang.GetString("ui.currenthealth");
        _HUDEnergyValue.text = _currentEnergy + " / " + _maxEnergy + " " + Lang.GetString("ui.currentenergy");
        _HUDHungerValue.text = _currentHunger + " / " + _maxHunger + " " + Lang.GetString("ui.currenthunger");
        _HUDThirstValue.text = _currentThirst + " / " + _maxThirst + " " + Lang.GetString("ui.currentthirst");

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

    // TODO: Add a sound which depends of who hits the player
    public void GotHit(int damage)
    {
        if (_canBeHit)
        {
            _canBeHit = false;
            _currentHealth = Mathf.Max(_currentHealth - Mathf.Max(damage - DefenceValue, 1), 0);
            UpdateUI(UIUpdate.Health);

            StartCoroutine(StartBlinking());

            if (_currentHealth == 0)
                StartCoroutine(Die());
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
    // TODO: A class Die, with smooth "YOU DIED !" which appears on screen
    private IEnumerator Die()
    {
        StartCoroutine(naturalRegen);

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

        while (deathTxt.fontSize < 50)
        {
            deathTxt.fontSize++;
            deathImg.color = new Color(deathImg.color.r, deathImg.color.g, deathImg.color.b, deathImg.color.a + 0.05F);
            yield return new WaitForSeconds(0.2F);
        }

        // TODO: Insta reload ?
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    /* Eat food in inventory at index indexInInventory */
    public void Eat(Food food)
    {
        _currentHunger = Mathf.Min(_maxHunger, _currentHunger + food.hungerValue);
        _currentThirst = Mathf.Min(_maxThirst, _currentThirst + food.thirstValue);
        UpdateUI(UIUpdate.Hunger);
        UpdateUI(UIUpdate.Thirst);
    }

    /* Update stats on screen, used after any modification of stats */
    private void UpdateUI(UIUpdate UIUpdate)
    {
        switch (UIUpdate)
        {
            case UIUpdate.Health:
                _HUDHealthValue.text = _currentHealth + " / " + _maxHealth + " " + Lang.GetString("ui.currenthealth");
                _HUDHealth.color = new Color(1 - ((float)_currentHealth / _maxHealth), ((float)_currentHealth / _maxHealth), 0);
                break;
            case UIUpdate.Energy:
                _HUDEnergyValue.text = _currentEnergy+ " / " + _maxEnergy + " " + Lang.GetString("ui.currentenergy");
                _HUDEnergy.color = new Color(1 - ((float)_currentEnergy / _maxEnergy), ((float)_currentEnergy / _maxEnergy), 0);
                break;
            case UIUpdate.Hunger:
                _HUDHungerValue.text = _currentHunger + " / " + _maxHunger + " " + Lang.GetString("ui.currenthunger");
                _HUDHunger.color = new Color(1 - ((float)_currentHunger / _maxHunger), ((float)_currentHunger / _maxHunger), 0);
                break;
            case UIUpdate.Thirst:
                _HUDThirstValue.text = _currentThirst + " / " + _maxThirst + " " + Lang.GetString("ui.currentthirst");
                _HUDThirst.color = new Color(1 - ((float)_currentThirst / _maxThirst), ((float)_currentThirst / _maxThirst), 0);
                break;
        }
    }
}