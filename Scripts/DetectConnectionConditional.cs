using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectConnectionConditional : MonoBehaviour
{
    [SerializeField]
    float hitRange = 1.0f;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float direction = 1.0f;

    [SerializeField]
    string io = "out";

    RaycastHit hit;

    [SerializeField]
    GameObject attached;

    Transform parent;

    Rigidbody attachedRB;

    public string value = null;
    public bool result = false;
    public AudioSource connectSound;
    OculusControls controls;

    // Start is called before the first frame update
    void Start()
    {
        connectSound.mute = true; GameObject oculusControls = GameObject.Find("OculusControlManager");
        controls = oculusControls.GetComponent<OculusControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug
            .DrawRay(transform.position, (transform.right * direction) * hitRange, Color.red);

        if (attached == null)
        {
            if (
                Physics
                    .SphereCast(transform.position,
                    0.1f,
                    transform.right * direction,
                    out hit,
                    hitRange,
                    mask)
            )
            // ,hitRange
            {
                if (hit.collider.CompareTag("PipeCon"))
                {
                    //if (Input.GetKeyDown(KeyCode.E))
                    if (controls.gripClick == 1)
                    {
                        parent = hit.collider.gameObject.transform.parent;
                        attached = hit.collider.gameObject;
                        if (attached.transform.position != transform.position + (direction * transform.right) * 0.3f)
                        {
                            attached.transform.position = transform.position + (direction * transform.right) * 0.3f;
                        }

                        attachedRB = attached.GetComponent<Rigidbody>();
                        //Debug.Log(attachedRB.gameObject.name);

                        // attached.GetComponent<Rigidbody>().useGravity = false;
                        // attached.transform.parent = parent;
                        connectSound.mute = false;
                        connectSound.Play();

                    }
                    GetComponent<Highlight>()?.ToggleHighlight(true);
                }
            }
            else
            {
                GetComponent<Highlight>()?.ToggleHighlight(false);
            }
        }
        if (attached != null)
        {
            GetComponent<Highlight>()?.ToggleHighlight(true);
            attached.transform.position =
                transform.position + ((transform.right * direction) * 0.3f);
            attached.transform.rotation = transform.rotation;
            if (attachedRB == null)
            {
                attachedRB = attached.GetComponent<Rigidbody>();
            }
            attachedRB.constraints =
                RigidbodyConstraints.FreezePosition |
                RigidbodyConstraints.FreezeRotation;

            // attached.transform.parent = parent;
            if (io == "pass2")
            {
                value = transform.parent.gameObject.GetComponent<Conditional>()?.textB.text;
                attached
               .transform
               .parent?
               .gameObject
               .GetComponent<Connector>()?
               .SetValue(value);

            }
            if (io == "pass1")
            {
                value = transform.parent.gameObject.GetComponent<Conditional>()?.textA.text;
                attached
               .transform
               .parent?
               .gameObject
               .GetComponent<Connector>()?
               .SetValue(value);

            }
            else if (io == "out")
            {
                result = transform.parent.gameObject.GetComponent<Conditional>().result;
                string res_text;
                if (result == false)
                {
                    res_text = "false";
                }
                else
                {
                    res_text = "true";
                }
                attached
               .transform
               .parent?
               .gameObject
               .GetComponent<Connector>()?
               .SetValue(res_text);

            }
            else if (io == "in")
            {
                value = attached.transform.parent.gameObject.GetComponent<Connector>()?.GetValue();
            }


            //    Debug
            //       .Log(attached
            //          .transform
            //        .parent
            //      .gameObject
            //    .GetComponent<Connector>()
            // .GetValue());
        }
    }
}
