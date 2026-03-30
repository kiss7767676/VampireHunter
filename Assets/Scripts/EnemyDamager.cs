using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDamager : MonoBehaviour
{

    public float damageAmount;
    public float lifeTime, growSpeed = 4f;
    private Vector3 targetSize;

    public bool destroyParent;

    public bool shouldKnockBack;

    public bool damageOverTime;
    public float TimeBetweenDamage;
    private float damageCounter;

    private List<EnemyMovement> enemiesInRange = new List<EnemyMovement>();

    public bool destroyOnImpact;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }

        }

        if (damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;

            if (damageCounter <= 0)
            {
                damageCounter = TimeBetweenDamage;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyMovement>().TakeDamage(damageAmount, shouldKnockBack);

                if (destroyOnImpact == true)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyMovement>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true)
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyMovement>());
            }
        }
    }
}

