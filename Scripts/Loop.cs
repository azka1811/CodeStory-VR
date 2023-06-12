using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Loop : MonoBehaviour
{
    [SerializeField]
    GameObject inp, outp;

    public string result;
    public int loop;
    DetectConnectionLoop inp_c, outp_c;

    public TextMeshPro resText;
    // Start is called before the first frame update
    void Start()
    {
        inp_c = inp.GetComponent<DetectConnectionLoop>();
        outp_c = outp.GetComponent<DetectConnectionLoop>();

        resText.text = "...\nTimes";
        result = "";
    }
    public void updateVal()
    {
        if (result != null && result != "")
        {
            if (result.Length > 1)
            {

                if (result[0] == 'L')
                {
                    print(result);
                    print(result.Length);
                    string times = result.Substring(1, result.Length-1).Trim();
                    try
                    {
                        loop = Convert.ToInt32(times);
                        resText.text = times + "\nTimes";
                    }
                    catch { }

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        updateVal();
    }
}
