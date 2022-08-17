    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{      

    Animator animator;
    public HealthBar healthBar;

    private void Awake()
    {
        animator = GetComponent<Animator>();        
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

        animator.Play("GetHit");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Death");
            
        }
    }
}
