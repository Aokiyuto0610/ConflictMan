using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int AttackDamage = 0;

    /// <summary>
    /// �U���͊i�[
    /// </summary>
    /// <param name="damage"></param>
    public void SetAttackDamage(int damage)
    {
        AttackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("�A�^�b�N");
        PlayerTest player = collision.gameObject.GetComponent<PlayerTest>();
        player.ReceivedDamage(AttackDamage);
    }
}
