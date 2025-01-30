using UnityEngine;

public class ObjectGroundChecker: MonoBehaviour
{
    // 接地しているかどうかを示すフラグ
    private bool isGrounded = true;

    // 地面として認識するタグ
    private string groundTag = "Floor";

    /// <summary>
    /// 接地しているかどうかを取得するプロパティ
    /// </summary>
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake() 
    {
        // 初期状態では接地していると設定
        isGrounded = true;
    }

    /// <summary>
    /// トリガーに接触したときに呼ばれるコールバック関数
    /// </summary>
    /// <param name="other">接触したコライダー</param>
    private void OnCollisionEnter2D(Collision2D other) 
    {
        // 接触したオブジェクトのタグが地面タグと一致する場合、接地状態にする
        if(other.gameObject.tag == groundTag)
        {
            isGrounded = true;
            Debug.LogWarning("接地");
        }
    }

    /// <summary>
    /// トリガーから離れたときに呼ばれるコールバック関数
    /// </summary>
    /// <param name="other">離れたコライダー</param>
    private void OnCollisionExit2D(Collision2D other)
    {
        // 離れたオブジェクトのタグが地面タグと一致する場合、接地状態を解除する
        if(other.gameObject.tag == groundTag)
        {
            isGrounded = false;
            Debug.LogWarning("離れた");
        }
    }
}
