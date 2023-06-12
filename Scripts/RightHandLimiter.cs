using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandLimiter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.localPosition.z < 4)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 4);
        }

    }
}
