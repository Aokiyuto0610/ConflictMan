using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSE : MonoBehaviour
{
    [SerializeField] private string[] allowedTags = { "Player" }; // 許可されたタグ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 接触したオブジェクトのタグを取得
        string tag = collision.gameObject.tag;
        SoundManager.Instance.PlaySE(SESoundData.SE.Walltuch);

        // 許可されたタグに含まれている場合は何もしない
        if (IsAllowedTag(tag))
        {
            return;
        }

        // 許可されたタグ以外が触れたときの処理
        Debug.Log($"禁止タグオブジェクトが触れた: {tag}");
        // 例えば、Destroy(collision.gameObject) や、警告音再生など
    }

    private bool IsAllowedTag(string tag)
    {
        foreach (string allowedTag in allowedTags)
        {
            if (tag == allowedTag)
            {
                return true;
            }
        }
        return false;
    }
}