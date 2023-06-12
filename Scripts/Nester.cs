using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using System;
public class Nester : MonoBehaviour
{
    [SerializeField]
    GameObject

            inp1,
            inp2,
            outp1,
            outp2,
            res;

    [SerializeField]
    string type;

    public TextMeshPro

            textA,
            textB;

    public string result = "";
    public int loopA, loopB, loopNest;

    DetectConnectionNester
            inp1C,
            inp2C,
            outp1C,
            outp2C,
            resC;

    // Start is called before the first frame update
    void Start()
    {
        inp1C = inp1.GetComponent<DetectConnectionNester>();
        inp2C = inp2.GetComponent<DetectConnectionNester>();
        outp1C = outp1.GetComponent<DetectConnectionNester>();
        outp2C = outp2.GetComponent<DetectConnectionNester>();
        resC = res.GetComponent<DetectConnectionNester>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inp1C.value != null && inp1C.value != "")
        {
            textA.text = inp1C.value.Substring(1, inp1C.value.Length - 1);
            try
            {
                outp1C.value = inp1C.value;
                loopA = Convert.ToInt32(textA.text);
            }
            catch { }
        }
        else
        {
            textA.text = "A";
        }
        if (inp2C.value != null && inp2C.value != "")
        {
            textB.text = inp2C.value.Substring(1, inp2C.value.Length - 1);
            try
            {
                outp2C.value = inp2C.value;
                loopB = Convert.ToInt32(textB.text);
            }
            catch { }
        }
        else
        {
            textB.text = "B";
        }
        if (inp1C.value != null && inp2C.value != null)
        {
            loopNest = loopA * loopB;
            result = "L" + loopNest;
        }
    }
}
