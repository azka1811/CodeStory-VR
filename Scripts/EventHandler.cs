using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private RaycastHit

            hit,
            hitPrev;

    [SerializeField]
    private LayerMask interactMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    [Min(1)]
    private float hitRange = 5;

    NarrativeSystem narrSys;

    // Start is called before the first frame update
    void Start()
    {
        narrSys = GetComponent<NarrativeSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug
            .DrawRay(playerCameraTransform.position,
            playerCameraTransform.forward * hitRange,
            Color.red);

        //Debug.Log("Running");
        if (narrSys.showingNarrative)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                narrSys.NextLine();
            }
        }
        else if (
             Physics
                 .Raycast(playerCameraTransform.position,
                 playerCameraTransform.forward,
                 out hit,
                 hitRange,
                 interactMask)
         )
        {
            hit
                .transform
                .gameObject
                .GetComponent<Highlight>()?
                .ToggleHighlight(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Clicked Q");

                hit.transform.gameObject.GetComponent<QuestButtonStart>()?.Click();
                hit.transform.gameObject.GetComponent<QuestButtonInfo>()?.Click();
                hit.transform.gameObject.GetComponent<BuyButton>()?.Click();

            }

            hitPrev = hit;
        }
        else
        {
            hitPrev.collider?.GetComponent<Highlight>()?.ToggleHighlight(false);
        }
    }
}
