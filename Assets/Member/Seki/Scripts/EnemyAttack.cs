using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int AttackDamage = 0;


    void SetAttackDamage(int damage)
    {
        AttackDamage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
