using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectConnection : MonoBehaviour
{
    [SerializeField]
    float hitRange = 1.0f;

    [SerializeField]
    LayerMask mask;

    RaycastHit hit;

    [SerializeField]
    GameObject attached;

    Transform parent;

    Rigidbody attachedRB;

    public AudioSource connectSound;


    // Start is called before the first frame update
    void Start()
    {
        connectSound.mute = true ;
    }

    // Update is called once per frame
    void Update()
    {
        Debug
            .DrawRay(transform.position, transform.right * hitRange, Color.red);

        if (attached == null)
        {
            if (
                Physics
                    .SphereCast(transform.position,
                    0.1f,
                    transform.right,
                    out hit,
                    hitRange,
                    mask)
            )
            // ,hitRange
            {
                if (hit.collider.CompareTag("PipeCon"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        parent = hit.collider.gameObject.transform.parent;
                        attached = hit.collider.gameObject;
                        attached.transform.position =
                            transform.position + transform.right * 0.3f;
                        attachedRB = attached.GetComponent<Rigidbody>();
                        connectSound.mute = false;

                        connectSound.Play();
                      //  Debug.Log(attachedRB.gameObject.name);

                        // attached.GetComponent<Rigidbody>().useGravity = false;
                        // attached.transform.parent = parent;
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
                transform.position + transform.right * 0.3f;
            attached.transform.rotation = transform.rotation;
            if (attachedRB == null)
            {
                attachedRB = attached.GetComponent<Rigidbody>();
            }
            attachedRB.constraints =
                RigidbodyConstraints.FreezePosition |
                RigidbodyConstraints.FreezeRotation;

            // attached.transform.parent = parent;

            attached
            .transform
            .parent
            .gameObject
            .GetComponent<Connector>()?
            .SetValue(transform
                .parent?
                .gameObject
                .transform
                .parent?
                .gameObject
                .transform
                .parent?
                .gameObject
                .GetComponent<Variable>()?
                .val);
          

            /*
             Debug
                 .Log(attached
                     .transform
                     .parent
                     .gameObject
                     .GetComponent<Connector>()
             .GetValue());*/
        }
    }
}
