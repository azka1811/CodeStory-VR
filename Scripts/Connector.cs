using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public string value;

    void Start()
    {
        value = "_";
    }

    void Update()
    {
    }

    public void SetValue(string val)
    {
        value = val;
    }

    public string GetValue()
    {
        return value;
    }
}
