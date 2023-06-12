using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectConnectionScreen : MonoBehaviour
{
    [SerializeField]
    float hitRange = 5.0f;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    GameObject port;

    RaycastHit hit;

    [SerializeField]
    GameObject attached;

    Transform parent;

    Rigidbody attachedRB;

    TextMeshPro screenText;
    public AudioSource connectSound;
    OculusControls controls;

    // Start is called before the first frame update
    void Start()
    {
        connectSound.mute = true;
        screenText = GetComponentInChildren<TextMeshPro>(); GameObject oculusControls = GameObject.Find("OculusControlManager");
        controls = oculusControls.GetComponent<OculusControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug
            .DrawRay(port.transform.position,
            -port.transform.right * hitRange,
            Color.red);

        if (
            Physics
                .SphereCast(port.transform.position,
                0.1f,
                -port.transform.right,
                out hit,
                hitRange,
                mask)
        )
        {
            if (hit.collider.CompareTag("PipeCon"))
            {
                port.GetComponent<Highlight>()?.ToggleHighlight(true);
                //if (Input.GetKeyDown(KeyCode.E))
                if (controls.gripClick == 1)
                {
                    parent = hit.collider.gameObject.transform.parent;
                    attached = hit.collider.gameObject;
                    attached.transform.position =
                        port.transform.position + -port.transform.right * 0.3f;
                    attachedRB = attached.GetComponent<Rigidbody>();
                    //Debug.Log(attachedRB.gameObject.name);

                    // attached.GetComponent<Rigidbody>().useGravity = false;
                    // attached.transform.parent = parent;
                    connectSound.mute = false;
                    connectSound.Play();

                }
            }
        }
        else
        {
            port.GetComponent<Highlight>()?.ToggleHighlight(false);
        }

        if (attached != null)
        {
            attached.transform.position =
                port.transform.position + -port.transform.right * 0.3f;
            attached.transform.rotation = port.transform.rotation;
            if (attachedRB == null)
            {
                attachedRB = attached.GetComponent<Rigidbody>();
            }
            attachedRB.constraints =
                RigidbodyConstraints.FreezePosition |
                RigidbodyConstraints.FreezeRotation;

            // attached.transform.parent = parent;
            screenText.text =
                attached
                    .transform
                    .parent
                    .gameObject
                    .GetComponent<Connector>()?
                    .GetValue();
        }
    }
}
