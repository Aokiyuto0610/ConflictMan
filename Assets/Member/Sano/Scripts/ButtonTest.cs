using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{

    private void OnClickTitel()
    {
        FadeManager.Instance.LoadScene("Title", 1.0F);
    }
}
