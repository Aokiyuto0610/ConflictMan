using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleMove : MonoBehaviour
{
    public float speed = 1000.0f;

    public void Move(Vector3 angle)
    {
        transform.rotation = Quaternion.Euler(angle);

        GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }

}
