using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public int progress;
    public Quest current;
    public GameObject canvas;
    QuestUI ui;

    public GameObject[] allQuests;

    public PLA pla;

    // Start is called before the first frame update
    void Start()
    {
        //load from data
        ui = canvas.GetComponent<QuestUI>();
        canvas.SetActive(false);

        allQuests = new GameObject[200];
        for (int i = 0; i < 200; i++)
        {
            allQuests[i] = GameObject.Find("QUEST_" + (i + 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (current != null)
        {
            ui.questName.text = current.questName;
            ui.questTimer.text = getTimeFormatted(current.timeRemaining);
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public string getTimeFormatted(float time)
    {
        int mins = (int)(time / 60);
        string minsStr = "";
        if (mins < 10)
        {
            minsStr = "0" + mins;
        }
        else
        {
            minsStr = "" + mins;
        }
        int secs = (int)(time - (mins * 60));
        string secsStr = "";
        if (secs < 10)
        {
            secsStr = "0" + secs;
        }
        else
        {
            secsStr = "" + secs;
        }
        return minsStr + ":" + secsStr;
    }

    public void updateThetaPLA(string type, float time, int retries, int complexity)
    {
        pla.updateTheta(type, time, retries, complexity);
    }
}
