using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DamageNumber : MonoBehaviour
{

    public TextMeshProUGUI damageText;

    public float lifeTime;
    private float lifeCounter;

    public float floatSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifeCounter = lifeTime;

    }

    // Update is called once per frame
    void Update()
    {

        lifeCounter -= Time.deltaTime;

        if (lifeCounter <= 0)
        {
            DamageNumberController.instance.PlaceInPool(this);

        }
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

    }

    public void Setup(int damageAmount)
    {
        lifeCounter = lifeTime;

        damageText.text = damageAmount.ToString();
    }

}
