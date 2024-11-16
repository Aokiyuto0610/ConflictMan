using UnityEngine;

public class Aoki_EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    HpManager_aoki hpManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                //player.PlayerTakeDamage(damage);
                Debug.Log("�ɂ�");
            }
        }


        if (collision.CompareTag("Player"))
        {
            hpManager.TakeDamage();
            Debug.Log("�v���C���[�ɍU��");
        }
        else if (collision.CompareTag("Hp"))
        {
            hpManager.TakeDamageIfFarFromPlayer();
            Debug.Log("Hp�ɍU��");
        }
    }
}
