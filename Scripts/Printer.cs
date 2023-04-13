using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class Printer : MonoBehaviour
{
    public DetectConnectionPrinter inp;
    public DetectConnectionPrinter outp;
    public DetectConnectionPrinter pow;
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pow.value != "" && pow.value != "_" && pow.value != null)
        {
            if (inp.value != "" && inp.value != "_")
            {
                int loopVal = int.Parse(pow.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                outp.loopVal = loopVal;
                inp.loopVal = loopVal;
                outp.value = inp.value;
            }
        }

        if(inp.fullValue != "" && inp.fullValue != "_")
        {
            text.text = inp.fullValue;
        }
    }
}
