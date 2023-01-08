using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    Transform mainCamera;

    [SerializeField]
    Transform miniMapCamera;

    public float yOff = 15f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        miniMapCamera.position =
            new Vector3(mainCamera.position.x, yOff, mainCamera.position.z);
        miniMapCamera.rotation = Quaternion.Euler(90f, mainCamera.eulerAngles.y, 0);
    }
}
