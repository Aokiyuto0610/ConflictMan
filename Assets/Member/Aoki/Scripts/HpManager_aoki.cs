using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager_aoki : MonoBehaviour
{
    [SerializeField] 
    private int maxHealth;
    private int currentHealth;

    [SerializeField] 
    private Image[] lifeUI;
    [SerializeField] 
    private Sprite fullHeart;
    [SerializeField] 
    private Sprite emptyHeart;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateLifeUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateLifeUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < lifeUI.Length; i++)
        {
            if (i < currentHealth)
            {
                lifeUI[i].sprite = fullHeart;
            }
            else
            {
                lifeUI[i].sprite = emptyHeart;
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
    }
}
