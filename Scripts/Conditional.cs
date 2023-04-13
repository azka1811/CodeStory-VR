using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Conditional : MonoBehaviour
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

    public bool result = false;

    DetectConnectionConditional

            inp1C,
            inp2C,
            outp1C,
            outp2C,
            resC;

    // Start is called before the first frame update
    void Start()
    {
        inp1C = inp1.GetComponent<DetectConnectionConditional>();
        inp2C = inp2.GetComponent<DetectConnectionConditional>();
        outp1C = outp1.GetComponent<DetectConnectionConditional>();
        outp2C = outp2.GetComponent<DetectConnectionConditional>();
        resC = res.GetComponent<DetectConnectionConditional>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inp1C.value != null)
        {
            textA.text = inp1C.value;
        }
        if (inp2C.value != null)
        {
            textB.text = inp2C.value;
        }
        if (inp1C.value != null && inp2C.value != null)
        {
            // Debug.Log("val1 " + inp1C.value);
            // Debug.Log("val2 " + inp2C.value);
            float
                val1 = -999999,
                val2 = -999999;
            try
            {
                val1 =
                    float
                        .Parse(inp1C.value,
                        CultureInfo.InvariantCulture.NumberFormat);
                val2 =
                    float
                        .Parse(inp2C.value,
                        CultureInfo.InvariantCulture.NumberFormat);
            }
            catch
            {
            }
            outp1C.value = inp1C.value;
            outp2C.value = inp2C.value;
            if (val1 != -999999 && val2 != -999999)
            {
                if (type == ">")
                {
                    if (val1 > val2)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    print(val1 + " " + val2 + " " + result);
                }
                else if (type == "<")
                {
                    if (val1 < val2)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if (type == "=")
                {
                    if (val1 == val2)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else
            {
                result = false;
            }
        }
    }
}
