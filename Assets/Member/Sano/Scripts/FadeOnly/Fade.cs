using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    [SerializeField]
    private Image _image = null;

    private void Reset()
    {
        _image = GetComponent<Image>();
    }

    /// フェードイン
    public void FadeIn(float duration, Action on_completed = null)
    {
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, on_completed, true));
    }

 
    /// フェードアウト
    public void FadeOut(float duration, Action on_completed = null)
    {
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, on_completed));
    }

    /// <summary>
    /// 時間経過でアルファ値を「0」から「1」に変更
    /// </summary>
    private IEnumerator ChangeAlphaValueFrom0To1OverTime(
        float duration,
        Action on_completed,
        bool is_reversing = false
    )
    {
        if (!is_reversing) _image.enabled = true;

        var elapsed_time = 0.0f;
        var color = _image.color;

        while (elapsed_time < duration)
        {
            var elapsed_rate = Mathf.Min(elapsed_time / duration, 1.0f);
            color.a = is_reversing ? 1.0f - elapsed_rate : elapsed_rate;
            _image.color = color;

            yield return null;
            elapsed_time += Time.deltaTime;
        }

        if (is_reversing) _image.enabled = false;
        if (on_completed != null) on_completed();
    }
}