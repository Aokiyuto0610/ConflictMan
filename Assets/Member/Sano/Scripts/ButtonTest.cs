using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{

    public void Titel()
    {
        FadeManager.Instance.LoadScene("Title", 1.0F);
    }
}
