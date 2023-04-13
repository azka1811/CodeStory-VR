using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndModule : MonoBehaviour
{
    [SerializeField]
    GameObject connector;

    [SerializeField]
    TextMeshPro text;

    string save = "";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        string val = connector.GetComponent<DetectConnectionEnd>().value;
        if (save != val && val != "" && val != null)
        {
            text.text = val;
            save = val;
            Debug.Log("true " + val);
        }
        // text.text = "hello";
    }
}
