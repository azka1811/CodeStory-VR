using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string[] expectedVal;

    public string questName = "QUEST";

    public string questRequirement = "Do XYZ";

    public string questHint = "";

    public int reward = 10;

    public int complexity;

    public string type;

    public bool isComplete = false;

    private bool rewardAdded = false;

    [SerializeField]
    private Player playerRef;

    public TextMeshPro questNameText;

    [SerializeField]
    private TextMeshPro questRequirementText;

    [SerializeField]
    private TextMeshPro questHintText;

    [SerializeField]
    TextMeshPro endText;

    [SerializeField]
    TextMeshPro endText2 = null;

    string[] vals;

    public AudioSource win;

    public AudioSource lose;

    private bool haveWon = false;

    private bool isWrong = false;

    private bool soundPlayedWin = false;

    private bool soundPlayedWrong = false;

    private string wrongList = "";

    public GameObject tick;

    public int questNum;

    QuestSystem questSystem;

    [SerializeField]
    bool canPlay;

    [SerializeField]
    GameObject questItems;

    [SerializeField]
    Highlight questDetH;

    Color colorOrange;
    Color colorGray;
    Color colorGreen;

    public float timeForQuest = 600;

    public float timeRemaining;
    public bool timerIsRunning = false;

    int retries = 0;
    Transform[] objects = new Transform[100];

    QuestNarrative questNarrative;

    public bool isPersonal;

    public void setEverythingPLA()
    {
        soundPlayedWin = false;
        isWrong = false;
        questRequirementText.text = "Quest: " + questRequirement;
        questHintText.text = "" + questHint;
        //questNum = 1;
        questSystem = playerRef?.GetComponent<QuestSystem>();
        Freeze();
        questName = "Personal Quest";
        questNameText.text = questName;
        timeRemaining = timeForQuest;

        int children = questItems.transform.childCount;
        for (int i = 0; i < children; ++i)
            objects[i] = questItems.transform.GetChild(i);

        complexity = reward / 10;
        isPersonal = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        isPersonal = false;
        soundPlayedWin = false;
        isWrong = false;
        questRequirementText.text = "Quest: " + questRequirement;
        questHintText.text = "Hint: " + questHint;
        //questNum = 1;
        questSystem = playerRef?.GetComponent<QuestSystem>();
        Freeze();
        questName = "LOCKED";
        questNameText.text = questName;
        colorOrange = new Color(0.750f, 0.326f, 0.0225f);
        colorGray = new Color(0.1f, 0.1f, 0.1f);
        colorGreen = new Color(0.146f, 0.430f, 0.293f);

        timeRemaining = timeForQuest;

        int children = questItems.transform.childCount;
        for (int i = 0; i < children; ++i)
            objects[i] = questItems.transform.GetChild(i);

        questNarrative = GetComponent<QuestNarrative>();

        complexity = reward / 10;

    }


    public void Freeze()
    {
        int children = questItems.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            GameObject g = questItems.transform.GetChild(i).gameObject;
            Rigidbody r = g.GetComponent<Rigidbody>();
            if (r)
            {
                int layerM = LayerMask.NameToLayer("Default");
                g.layer = layerM;
                r.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    public void ResetItems()
    {
        int children = questItems.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            // questItems.transform.GetChild(i).gameObject.transform.position = objects[i].position;
            // questItems.transform.GetChild(i).gameObject.transform.rotation = objects[i].transform.rotation;

        }

    }

    public void UnFreeze()
    {
        int children = questItems.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            GameObject g = questItems.transform.GetChild(i).gameObject;
            Rigidbody r = g.GetComponent<Rigidbody>();
            Debug.Log(g);
            if (r)
            {
                int layerM = LayerMask.NameToLayer("pickable");
                g.layer = layerM;
                r.constraints = RigidbodyConstraints.None;
            }
        }
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                // Debug.Log("Time has run out!");
                timeRemaining = timeForQuest;
                timerIsRunning = false;
                //ResetItems();
                Freeze();
                questSystem.current = null;
                retries += 1;
            }
        }

        if (isComplete)
        {
            questDetH.color = colorGreen;
            timerIsRunning = false;

        }

        if (questSystem.current == null)

            if (canPlay == false)
            {
                if (questSystem.progress >= questNum - 1)
                {
                    canPlay = true;
                    if (isPersonal)
                    {
                        questName = "Personal Quest";
                    }
                    else
                    {
                        questName = "Quest " + questNum;
                    }
                    questNameText.text = questName;
                    questDetH.color = colorOrange;
                }
            }

        questDetH.ToggleHighlight(true);


        string v1 = endText.text;
        if (endText2 != null)
        {
            string v2 = endText2.text;
            vals = new string[2];
            vals[0] = v1.Trim();
            vals[1] = v2.Trim();
        }
        else
        {
            if (v1 != "" && v1 != "_")
            {
                vals = v1.Split(", ");
            }
        }

        if (haveWon && !soundPlayedWin)
        {
            win.Play();
            soundPlayedWin = true;
        }
        else if (isWrong && !soundPlayedWrong)
        {
            lose.Play();
            soundPlayedWrong = true;
        }

        if (v1 != null && v1 != "_" && v1 != "" && !soundPlayedWin)
        {
            bool isWin = true;
            for (int i = 0; i < expectedVal.Length; i++)
            {
                //                Debug
                //    .Log("i " +
                //    i +
                //  " - ex " +
                //    expectedVal[i] +
                // " got " +
                //  vals[i]);
                if (expectedVal[i].Trim() != vals[i].Trim())
                {
                    isWin = false;
                }
            }
            if (isWin)
            {
                // Debug.Log("WON");
                haveWon = true;
                questSystem.updateThetaPLA(type, timeForQuest - timeRemaining, retries, complexity);
                questSystem.current = null;
                tick.SetActive(true);
                if (rewardAdded == false)
                {
                    playerRef.SetMoney(playerRef.GetMoney() + reward);
                    rewardAdded = true;
                    isComplete = true;
                    questSystem.progress++;

                }
            }
            else
            {
                // print("NOT");
                isWrong = true;
                if (wrongList == "")
                {
                    wrongList = v1;
                }
                else if (wrongList != v1)
                {
                    isWrong = true;
                    wrongList = v1;
                    soundPlayedWrong = false;
                    tick.SetActive(false);
                }
            }
        }
    }
}
