using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class NarrativeSystem : MonoBehaviour
{
    public GameObject imagePanel, textPanel;
    public TextMeshProUGUI nText;
    public GameObject nImage;
    public string[] currentText;
    public Sprite[] image;
    public int[] imageIndex;


    public int imageIdx, lineIdx;

    string txt;

    int imageCounter;

    public VideoPlayer video;
    public VideoClip[] anims;

    public bool showingNarrative = false;

    public void StartNarrative()
    {
        showingNarrative = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        imagePanel.SetActive(false);
        lineIdx = 0;
        imageCounter = 0;
        textPanel.SetActive(true);
        StartNarrative();
        NextLine();
    }

    // Update is called once per frame
    void Update()
    {
        //vr.Integrate();
    }

    public void ShowImage()
    {
        imagePanel.SetActive(true);
    }

    public void ShowText()
    {
        textPanel.SetActive(true);
    }

    public void HideImage()
    {
        imagePanel.SetActive(false);

    }

    public void HideText()
    {
        textPanel.SetActive(false);
    }

    public void NextLine()
    {
        HideImage();
        if (anims.Length > 0)
        {
            if (imageCounter < anims.Length)
            {
                if (lineIdx == imageIndex[imageCounter])
                {
                    video.clip = anims[imageCounter];
                    ShowImage();
                    imageCounter++;
                }
            }
        }


        if (lineIdx >= currentText.Length)
        {
            HideText();
            HideImage();
            lineIdx = 0;
            imageIdx = 0;
            imageCounter = 0;
            currentText = null;
            showingNarrative = false;
           // playedNarrative = true;
            return;
        }

        txt = currentText[lineIdx];

        nText.text = txt;

        lineIdx += 1;
    }
}
