using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class QuestButtonInfo : MonoBehaviour
{
    public GameObject player;
    QuestSystem questSystem;
    Quest quest;
    QuestNarrative narr;
    NarrativeSystem narrSys;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PLAYER");
        questSystem = player.GetComponent<QuestSystem>();
        quest = transform.parent.parent.gameObject.GetComponent<Quest>();
        narr = quest.GetComponent<QuestNarrative>();
        narrSys = player.GetComponent<NarrativeSystem>();

       // Debug.Log(narr);
      //  Debug.Log(narrSys);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        Debug.Log("info clicked");
        if (narr.anims.Length == 0)
        {
            //text only
            narrSys.StartNarrative();
            narrSys.currentText = narr.lines;
            narrSys.ShowText();

            narrSys.NextLine();
            
        }
        else
        {
            narrSys.StartNarrative();
            narrSys.currentText = narr.lines;
            // narrSys.image = narr.image;
            narrSys.anims = narr.anims;
            narrSys.imageIndex = narr.imageIndex;
            narrSys.ShowText();
            narrSys.ShowImage();

            narrSys.NextLine();
        }

    }
}
