using UnityEngine;

public class Aoki_EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    HpManager hpManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PlayerTakeDamage(damage);
                Debug.Log("itai");
            }
        }


        if (collision.CompareTag("Player"))
        {
            hpManager.TakeDamage();
        }
        else if (collision.CompareTag("Hp"))
        {
            hpManager.TakeDamageIfFarFromPlayer();
        }
    }
}
