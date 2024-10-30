using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Heal, Item2 }
    public ItemType itemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.Heal:
                    // Healメソッドを呼び出す
                    other.GetComponent<HpManager>().Heal();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
