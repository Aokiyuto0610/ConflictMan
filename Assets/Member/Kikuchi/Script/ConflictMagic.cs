using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictMagicBase : MonoBehaviour
{
    private bool isFirst = false;
    private float waitTime = 0f;
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Renga") return;
        if(isFirst) return;
        isFirst = true;
        other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ConflictMagicCast();
        SoundManager.Instance.PlaySE(SESoundData.SE.Action);
    }

    protected virtual void Update()
    {
        if(isFirst)
        {
            waitTime += Time.deltaTime;
            if(waitTime > 1.0f)
            {
                isFirst = false;
                waitTime = 0f;
            }
        }
    }

    protected virtual void ConflictMagicCast(){}

    public void ResetFirst()
    {
        isFirst = false;
    }
}
