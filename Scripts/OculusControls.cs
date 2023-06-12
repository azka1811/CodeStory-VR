using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;
public class OculusControls : MonoBehaviour
{
    public InputActionProperty grip;
    public InputActionProperty trigger;
    public InputActionProperty button_A;

    public float gripClick;
    public float triggerClick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gripClick = grip.action.ReadValue<float>();
        triggerClick = trigger.action.ReadValue<float>();
    }
}
