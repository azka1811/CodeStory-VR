using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public float time = 3;
    public GameObject image;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            SceneManager.LoadScene("csvr_01");
        }
        else
        {
            float val = Mathf.Sin(time*2) * 0.5f +0.5f;
            img.color = new Color(val, val, val);
        }
    }
}
