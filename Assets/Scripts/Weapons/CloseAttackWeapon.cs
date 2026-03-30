using UnityEngine;

public class CloseAttackWeapon : Weapon
{

    public EnemyDamager damager;
    private float attackCounter, direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        attackCounter -= Time.deltaTime;

        if (attackCounter <= 0)
        {
            attackCounter = stats[weaponLevel].timeBetweenAttacks;
            direction = Input.GetAxisRaw("Horizontal");

            Vector3 spawnPosition = transform.position;

            if (direction != 0)
            {
                if (direction > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                    spawnPosition += Vector3.right; // adjust the offset as needed
                }
                else
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    spawnPosition += Vector3.left; // adjust the offset as needed
                }
            }

            Instantiate(damager, spawnPosition, damager.transform.rotation, transform).gameObject.SetActive(true);

            for (int i = 1; i < stats[weaponLevel].amount; i++)
            {
                float rot = 360f / stats[weaponLevel].amount * i;

                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.rotation.eulerAngles.z + rot), transform).gameObject.SetActive(true);


            }

            SFXManager.instance.PlaySFXPitched(10);
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        attackCounter = 0f;
    }
}
