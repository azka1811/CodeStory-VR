using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEngine.InputSystem;
public class DragDrop : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    // [SerializeField]
    // private GameObject pickUpUI;
    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    private GameObject inHandItem;

    private Rigidbody inHandItemRB;

    private GameObject inHandItem2;

    private Rigidbody inHandItem2RB;

    private RaycastHit hit;

    private Transform orgParent1;

    private Transform orgParent2;

    [SerializeField]
    [Min(1)]
    float moveSpeed = 100f;


    // private void Start()
    // {
    // }
    private void Drop()
    {
        inHandItemRB.useGravity = true;

        inHandItemRB.drag = 1;
        inHandItemRB.constraints = RigidbodyConstraints.None;
        inHandItem.transform.parent = orgParent1;
        inHandItem = null;

        if (inHandItem2 != null)
        {
            inHandItem2RB.useGravity = true;

            inHandItem2RB.drag = 1;
            inHandItem2RB.constraints = RigidbodyConstraints.None;
            inHandItem2.transform.parent = orgParent2;
            inHandItem2 = null;
        }
    }

    private void PickUp(GameObject g)
    {
        if (g.GetComponent<Rigidbody>())
        {
            inHandItemRB = g.GetComponent<Rigidbody>();
            inHandItemRB.useGravity = false;

            inHandItemRB.drag = 10;
            inHandItemRB.constraints = RigidbodyConstraints.FreezeRotation;
            orgParent1 = inHandItemRB.transform.parent;
            inHandItemRB.transform.parent = pickUpParent;
            inHandItem = g;

            if (
                g.GetComponent<Variable>() &&
                g.GetComponent<Variable>().constantInside != null
            )
            {
                inHandItem2 = g.GetComponent<Variable>().constantInside;
                inHandItem2RB = inHandItem2.GetComponent<Rigidbody>();

                inHandItem2RB.useGravity = false;

                inHandItem2RB.drag = 10;
                inHandItem2RB.constraints = RigidbodyConstraints.FreezeRotation;
                orgParent2 = inHandItem2RB.transform.parent;
                inHandItem2RB.transform.parent = pickUpParent;
            }
            else
            {
                inHandItem2 = null;
                inHandItem2RB = null;
            }
        }
    }

    private void Move()
    {
        if (
            Vector3
                .Distance(pickUpParent.position,
                inHandItem.transform.position) >
            0.1f
        )
        {
            Vector3 moveDirection =
                (pickUpParent.position - inHandItem.transform.position);
            inHandItemRB.AddForce(moveDirection * moveSpeed);

            if (inHandItem2RB != null)
            {
                inHandItem2RB.AddForce(moveDirection * moveSpeed);
            }
        }
    }

    private void Update()
    {
       // Debug
         //   .DrawRay(playerCameraTransform.position,
         //   playerCameraTransform.forward * hitRange,
          //  Color.red);


        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);

            // pickUpUI.SetActive(false);
            // Debug.Log("CAN SEE");
        }
        if (inHandItem == null)
        {
            if (
                Physics
                    .Raycast(playerCameraTransform.position,
                    playerCameraTransform.forward,
                    out hit,
                    hitRange,
                    pickableLayerMask)
            )
            {
                hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                 
                    //Debug.Log("E");
                    PickUp(hit.transform.gameObject);
                }
                // pickUpUI.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Drop();
            }
            else
            {
                Move();
            }
            return;
        }
    }
}
