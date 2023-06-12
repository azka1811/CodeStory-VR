using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectConnectionPrinter : MonoBehaviour
{
    [SerializeField]
    float hitRange = 1.0f;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float direction = 1.0f;

    [SerializeField]
    string type = "out";

    RaycastHit hit;

    [SerializeField]
    GameObject attached;

    Transform parent;

    Rigidbody attachedRB;

    public AudioSource connectSound;

    public TextMeshPro text;

    public int loopVal = 0;

    public string value;

    public string fullValue; OculusControls controls;


    // Start is called before the first frame update
    void Start()
    {
        connectSound.mute = true;
        value = ""; GameObject oculusControls = GameObject.Find("OculusControlManager");
        controls = oculusControls.GetComponent<OculusControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug
            .DrawRay(transform.position,
            (transform.right * direction) * hitRange,
            Color.red);

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
                    //    Debug.Log("I SEE");
                    // if (Input.GetKeyDown(KeyCode.E))
                    if (controls.gripClick == 1)
                    {
                        parent = hit.collider.gameObject.transform.parent;
                        attached = hit.collider.gameObject;
                        if (
                            attached.transform.position !=
                            transform.position +
                            (direction * transform.right) * 0.3f
                        )
                        {
                            attached.transform.position =
                                transform.position +
                                (direction * transform.right) * 0.3f;
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
            /*if (type == "")
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
            else*/
            if (type == "in")
            {
                value =
                    attached
                        .transform
                        .parent
                        .gameObject
                        .GetComponent<Connector>()?
                        .GetValue();
                // string value = text.text.Trim();
                /* value = value.Trim();
                 attached
                .transform
                .parent?
                .gameObject
                .GetComponent<Connector>()?
                .SetValue("L" + value);
             */
            }
            else if (type == "pow")
            {
                //value = attached.transform.parent.gameObject.GetComponent<Connector>()?.GetValue();
                value =
                    attached
                        .transform
                        .parent
                        .gameObject
                        .GetComponent<Connector>()?
                        .GetValue();
                try
                {
                    value = value.Trim();
                }
                catch
                {
                }
                if (value[0] != 'L')
                {
                    value = "";
                    return;
                }

                value = value.Substring(1);
            }
            else if (type == "out")
            {
                //value = attached.transform.parent.gameObject.GetComponent<Connector>()?.GetValue();
                //Debug.Log(fullValue);
                attached
                    .transform
                    .parent?
                    .gameObject
                    .GetComponent<Connector>()?
                    .SetValue(fullValue);
            }
            if (loopVal != 0 && (value != "" && value != "_"))
            {
                //   Debug.Log("asd");
                string[] vals = new string[loopVal];
                for (int i = 0; i < loopVal; i++)
                {
                    vals[i] = value;
                }
                fullValue = string.Join(", ", vals);
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
