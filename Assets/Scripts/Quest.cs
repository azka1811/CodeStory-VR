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

    public bool isComplete = false;

    private bool rewardAdded = false;

    [SerializeField]
    private Player playerRef;

    [SerializeField]
    private TextMeshPro questNameText;

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

    // Start is called before the first frame update
    void Start()
    {
        soundPlayedWin = false;
        isWrong = false;
        questNameText.text = questName;
        questRequirementText.text = "Quest: " + questRequirement;
        questHintText.text = "Hint: " + questHint;
    }

    // Update is called once per frame
    void Update()
    {
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
                tick.SetActive(true);
                if (rewardAdded == false)
                {
                    playerRef.SetMoney(playerRef.GetMoney() + reward);
                    rewardAdded = true;
                    isComplete = true;
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
