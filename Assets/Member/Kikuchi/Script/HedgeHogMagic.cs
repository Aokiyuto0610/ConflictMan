using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HedgeHogMagic : ConflictMagicBase
{
    [SerializeField]
    private GameObject needle;

    [SerializeField]
    private float shotSpeed = 10.0f;

    private float[] shotAngles = {0.0f, 10.0f, 20.0f};

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        Debug.Log("HedgeHogMagic OnTriggerEnter");
    }

    protected override void ConflictMagicCast()
    {
        NeedleShot();
    }

    private void NeedleShot()
    {
        foreach (var angle in shotAngles)
        {
            // 発射方向を計算（transform.right を基準に回転）
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

            // プレハブの生成
            var shot = Instantiate(needle, transform.position, Quaternion.identity);

            // Rigidbody2D を取得して速度を設定
            var rb = shot.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction.normalized * shotSpeed;
            }
            else
            {
                Debug.LogWarning("生成されたオブジェクトにRigidbody2Dがアタッチされていません。");
            }
        }
    }
}
