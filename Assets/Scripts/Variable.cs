using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Variable : MonoBehaviour
{
    TextMeshPro boxText;

    bool updated = false;

    public GameObject constantInside;

    public string val;

    // Start is called before the first frame update
    void Start()
    {
        boxText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updated)
        {
            boxText.text = val;
            updated = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Const"))
        {
            GameObject objHit = other.gameObject;
            constantInside = objHit;
            //Debug.Log(objHit.tag);
            TextMeshPro txt =
                objHit.GetComponentInChildren<TextMeshPro>(true) as TextMeshPro;
            //Debug.Log(txt.text);
            val = txt.text;
            updated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Const"))
        {
            constantInside = null;
            boxText.text = "--";
            val = "--";
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
