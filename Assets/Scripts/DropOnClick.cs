using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.IO;

public class DropOnClick : MonoBehaviour
{
    public Rigidbody box;

    public Rigidbody ball;

    // Start is called before the first frame update
    void Start()
    {
        ball.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("CLICK");
            ball.useGravity = !ball.useGravity;
        }
    }
}
