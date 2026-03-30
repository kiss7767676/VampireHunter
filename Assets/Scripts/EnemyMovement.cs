using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Numerics;


public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D theRigidBody;
    public float moveSpeed, damage;
    private Transform target;

    public float hitWaitTime = 0.5f;
    private float hitCounter;
    public float health = 10f;
    private float knockBackCounter;

    public float knockBackTime = 0.1f;

    public int expToGive = 1;

    public int coinValue = 1;
    public float coinDropRate = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        target = PlayerHealth.instance.transform;

        moveSpeed = UnityEngine.Random.Range(moveSpeed * 0.8f, moveSpeed * 1.2f);


    }

    private void Update()
    {
        if (PlayerMovement.instance.gameObject.activeSelf == true)
        {


            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if (knockBackCounter <= 0)
                {
                    moveSpeed = Math.Abs(moveSpeed * 0.5f);
                }
            }
        }
        else
        {
            theRigidBody.velocity = UnityEngine.Vector2.zero;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerMovement.instance.gameObject.activeSelf)
        {
            theRigidBody.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
        else
        {
            theRigidBody.velocity = UnityEngine.Vector2.zero;
        }

        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            player.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }

    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            Destroy(gameObject);
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);

            if (UnityEngine.Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            SFXManager.instance.PlaySFXPitched(0);
        }
        else
        {
            SFXManager.instance.PlaySFXPitched(1);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);

    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }


}


