using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int _attackDamage = 0;

    [SerializeField] EnemyState _enemyState;

    /// <summary>
    /// çUåÇóÕäiî[
    /// </summary>
    /// <param name="damage"></param>
    public void SetAttackDamage(int damage)
    {
        _attackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i <_enemyState._notEnemyAttackTag.Length; i++)
        {
            if (_enemyState._notEnemyAttackTag[i] == collision.tag)
            {
                //Debug.Log("return");
                return;
            }
        }
        //Debug.Log("ÉAÉ^ÉbÉN");
        PlayerTest player = collision.gameObject.GetComponent<PlayerTest>();
        player.ReceivedDamage(_attackDamage);
    }
}
