using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{   

    public HealthBar healthBar;

    PlayerAnimatorManager animatorManager;

    private void Awake()
    {
        animatorManager = GetComponent<PlayerAnimatorManager>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        healthBar.SetCurrentHealth(currentHealth);

        animatorManager.PlayTargetAnimation("GetHit", true);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            animatorManager.PlayTargetAnimation("Death", true);
        }
    }
}
