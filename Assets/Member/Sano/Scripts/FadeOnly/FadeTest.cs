using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeTest : MonoBehaviour
{
    [SerializeField]
    private Fade _fade = null;

    private void Start()
    {
        Action on_completed = () =>
        {
            StartCoroutine(Wait3SecondsAndFadeOut());
        };

        _fade.FadeIn(2.0f, on_completed);
    }

    private IEnumerator Wait3SecondsAndFadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        _fade.FadeOut(2.0f);
    }
}

