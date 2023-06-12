using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelButton : MonoBehaviour
{
    public GameObject player;
    QuestSystem questSystem;
    Quest quest;
    public SceneManage manager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PLAYER");
        questSystem = player.GetComponent<QuestSystem>();
        quest = transform.parent.parent.gameObject.GetComponent<Quest>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        Debug.Log("I was clicked");
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "csvr_01")
        {
            if (player.GetComponent<Player>().GetMoney() >= 200)
            {
                Debug.Log("I HAVE MONEY");
                manager.LoadWorld2();
            }
        }
        else
        {
            manager.LoadWorld1();

        }
    }
}
