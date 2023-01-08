using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class Operator : MonoBehaviour
{
    [SerializeField]
    GameObject inp1, inp2, outp;

    [SerializeField]
    string type;


    public string res;

    DetectConnectionOperator inp1_c, inp2_c, outp_c;

    // Start is called before the first frame update
    void Start()
    {
        inp1_c = inp1.GetComponent<DetectConnectionOperator>();
        inp2_c = inp2.GetComponent<DetectConnectionOperator>();
        outp_c = outp.GetComponent<DetectConnectionOperator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inp1_c.value != null && inp2_c.value != null)
        {
            // Debug.Log("val1 " + inp1_c.value);
            // Debug.Log("val2 " + inp2_c.value);
            float val1 = 0, val2 = 0;
            try
            {
                val1 = float.Parse(inp1_c.value, CultureInfo.InvariantCulture.NumberFormat);
                val2 = float.Parse(inp2_c.value, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch
            {

            }
            float val3 = 0;
            if (type == "+")
            {
                //float val = float.Parse(inp1_c.value) + float.Parse(inp2_c.value);
                val3 = val1 + val2;
                //Debug.Log(outp_c.value);
            }
            else if (type == "x")
            {
                val3 = val1 * val2;

            }
            else if (type == "/")
            {

                val3 = val1 / val2;
            }
            else if (type == "-")
            {
                val3 = val1 - val2;

            }
            try
            {
                outp_c.value = val3.ToString();
                res = outp_c.value;

            }
            catch
            {

            }
        }

    }
}
