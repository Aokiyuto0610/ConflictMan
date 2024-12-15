using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapTest : MonoBehaviour
{
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public int count;

    void Start()
    {
        count = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            count++;
            if (count > 3)
            {
                count = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            count--;
            if (count < 1)
            {
                count = 3;
            }
        }

        if (count == 1)
        {
            map1.SetActive(true);
            map2.SetActive(false);
            map3.SetActive(false);
        }

        if (count == 2)
        {
            map1.SetActive(false);
            map2.SetActive(true);
            map3.SetActive(false);
        }

        if (count == 3)
        {
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(true);
        }
    }
}
