using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("csvr_01");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("csvr_2");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("csvr_loading");
        }
    }

    public void LoadWorld2()
    {
        SceneManager.LoadScene("csvr_2");
    }

    public void LoadWorld1()
    {
        SceneManager.LoadScene("csvr_01");
    }
}
