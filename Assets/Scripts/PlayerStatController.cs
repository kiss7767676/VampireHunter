using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{

    public static PlayerStatController instance;
    private void Awake()
    {
        instance = this;
    }

    public List<PlayerStatValue> moveSpeed, health, pickUpRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickUpRangeLevelCount;

    public int moveSpeedLevel, healthLevel, pickUpRangeLevel, maxWeaponsLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }

        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }

        for (int i = pickUpRange.Count - 1; i < pickUpRangeLevelCount; i++)
        {
            pickUpRange.Add(new PlayerStatValue(pickUpRange[i].cost + pickUpRange[1].cost, pickUpRange[i].value + (pickUpRange[1].value - pickUpRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UiController.instance.levelUpPanel.activeSelf == true)
        {
            UpdateDisplay();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void UpdateDisplay()
    {

        if (moveSpeedLevel < moveSpeed.Count - 1)
        {
            UiController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        }
        else
        {
            UiController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }
        if (healthLevel < health.Count - 1)
        {
            UiController.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UiController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }
        if (pickUpRangeLevel < pickUpRange.Count - 1)
        {
            UiController.instance.pickUpRangeUpgradeDisplay.UpdateDisplay(pickUpRange[pickUpRangeLevel + 1].cost, pickUpRange[pickUpRangeLevel].value, pickUpRange[pickUpRangeLevel + 1].value);
        }
        else
        {
            UiController.instance.pickUpRangeUpgradeDisplay.ShowMaxLevel();
        }
        if (maxWeaponsLevel < maxWeapons.Count - 1)
        {
            UiController.instance.MaxWeaponsUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponsLevel + 1].cost, maxWeapons[maxWeaponsLevel].value, maxWeapons[maxWeaponsLevel + 1].value);
        }
        else
        {
            UiController.instance.MaxWeaponsUpgradeDisplay.ShowMaxLevel();
        }
    }
    public void PurchaseMoveSpeed()
    {
        moveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateDisplay();

        PlayerMovement.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
    }

    public void PurchaseHealth()
    {
        healthLevel++;
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        UpdateDisplay();

        PlayerHealth.instance.maxHealth = health[healthLevel].value;
        PlayerHealth.instance.currentHealth += health[healthLevel].value - health[healthLevel - 1].value;


    }

    public void PurchasePickUpRange()
    {
        pickUpRangeLevel++;
        CoinController.instance.SpendCoins(pickUpRange[pickUpRangeLevel].cost);
        UpdateDisplay();

        PlayerMovement.instance.pickupRange = pickUpRange[pickUpRangeLevel].value;

    }

    public void PurchaseMaxWeapons()
    {
        maxWeaponsLevel++;
        CoinController.instance.SpendCoins(maxWeapons[maxWeaponsLevel].cost);
        UpdateDisplay();

        PlayerMovement.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].value);

    }
}

[System.Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;

    public PlayerStatValue(int newCost, float newValue)
    {
        cost = newCost;
        value = newValue;
    }


}