using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjStatus : MonoBehaviour
{
    [SerializeField] public int _bounce;
    public bool moveflag;
    Rigidbody __rb;

    // Start is called before the first frame update
    void Start()
    {
        __rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_bounce <= 0)
        {
            Vector2 objGravity = new Vector2(0, -10f * 2);
            __rb.AddForce(objGravity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall")) ;
        {
            _bounce--;
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            _bounce = 0;
        }
        if(other.gameObject.CompareTag("Conflict"))
        {
            if(moveflag == true)
            {

            }
            else
            {

            }
        }
    }
}
