using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float health;
    public float maxHealth = 100;
    public float mana;
    public float maxMana = 200;
    public float regenCooldown = 3;
    public float healCooldown = 5;
    private float regenTime = 0;
    private float healTime = 0;
    public bool canRegenMana = true;
    public bool canHeal = true;
    public HealthBar healthBar;
    public ManaBar manaBar;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMax(maxHealth);
        manaBar.SetMax(maxMana);
        Debug.Log("Max mana: " + maxMana.ToString());
        mana = maxMana;
    }

    public void TakeDamage(float damage)
    {
        //implement camra shake here
        health -= damage;
        healthBar.SetHealth(health);
        canHeal = false;
        healTime = Time.time + healCooldown;
        Debug.Log("took damage in pstats");
    }

    public void Heal(float heal)
    {
        if (health < maxHealth)
        {
            health += heal;
            healthBar.SetHealth(health); 
        }
    }

    public void SpendMana(float spentmana)
    {
        mana -= spentmana;
        manaBar.SetMana(mana);
        canRegenMana = false;
        regenTime = Time.time + regenCooldown;
    }

    public void RegenMana(float regainedmana)
    {
        if (mana < maxMana)
        {
            mana += regainedmana;
            manaBar.SetMana(mana);
        }
    }



    void Update()
    {
        if (Time.time >= regenTime)
        {
            canRegenMana = true;
        }

        if (Time.time >= healTime)
        {
            canHeal = true;
        }

        if (canRegenMana) 
        {
            RegenMana(.1f);
        }

        if (canHeal)
        {
            Heal(0.1f);
        }
        /*
        if (Input.GetKeyDown("q"))
        {
            TakeDamage(20);
        }*/
    }
}
