using System;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{

    public static ExperienceLevelController instance;
    public void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public ExpPickup pickup;

    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;

    public List<Weapon> weaponsToUpgrade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UiController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
        SFXManager.instance.PlaySFXPitched(2);

    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];

        currentLevel++;

        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }


        UiController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;



        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerMovement.instance.assignedWeapons);

        if (availableWeapons.Count > 0)
        {
            int selected = UnityEngine.Random.Range(0, availableWeapons.Count);

            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (PlayerMovement.instance.assignedWeapons.Count + PlayerMovement.instance.fullyLevelweapons.Count < PlayerMovement.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerMovement.instance.unnassignedWeapons);
        }


        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = UnityEngine.Random.Range(0, availableWeapons.Count);

                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UiController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }


        for (int i = 0; i < UiController.instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UiController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UiController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatController.instance.UpdateDisplay();

    }
}
