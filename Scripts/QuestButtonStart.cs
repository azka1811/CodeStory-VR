using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButtonStart : MonoBehaviour
{
    public GameObject player;
    QuestSystem questSystem;
    Quest quest;
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
        if (questSystem.current == null && (questSystem.progress <= quest.questNum || quest.isPersonal))
        {
            questSystem.current = quest;
            quest.UnFreeze();
        }
    }
}
