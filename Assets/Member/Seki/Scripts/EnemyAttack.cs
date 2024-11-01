using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int AttackDamage = 0;

    /// <summary>
    /// çUåÇóÕäiî[
    /// </summary>
    /// <param name="damage"></param>
    public void SetAttackDamage(int damage)
    {
        AttackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("ÉAÉ^ÉbÉN");
        PlayerTest player = collision.gameObject.GetComponent<PlayerTest>();
        player.ReceivedDamage(AttackDamage);
    }
}
