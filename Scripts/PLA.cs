using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Personalized Learning Algorithm
public class PLA : MonoBehaviour
{

    public string[] categories = { "VAR", "ATH", "CON" };
    public float[] theta;

    public float a = 0.1f;
    public float b = 0.2f;
    public float c = 0.2f;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        // 120s, 1 rt, 1c
        // 1.2 + 0.2 + 0.2 = 1.6
        // 140s, 2rt, 2c
        // 1.2 + 0.2 + 0.4 = 2.2
        // 1.9

        // 60s, 0rt
        // 0.6 + 0 = 0.6
        // 120s, 1rt = 1.4
        // 1

        theta = new float[15];
        for (int i = 0; i < categories.Length; i++)
        {
            theta[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void updateTheta(string type, float time, int retries, int complexity)
    {

        Debug.Log("UPDATING THETA: " + type +" time: " + time + " retries: " + retries + " cmplx: " + complexity);
        int index = findIndex(categories, type);

        if (index == -1)
        {
            Debug.Log("ERROR UPDATING THETA"); return;
        }

        float val = a * time + b * retries + c * complexity;

        if (theta[index] == 0)
        {
            theta[index] = val;
        }
        else
        {
            theta[index] = (theta[index] + val) / 2;
        }

    }


    int findIndex(string[] array, string find)
    {
        print("finding "+ find);
        for (int i = 0; i < array.Length; i++)
        {
            print("'" + array[i] + "'" + " '" + find+ "'");

            if (array[i].Equals(find) == true)
            {
                return i;
            }
        }
        return -1;
    }
}
