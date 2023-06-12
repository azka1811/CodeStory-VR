using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


public class OculusMovement : MonoBehaviour
{
    public XRNode inputSource;
    public float speed = 1;

    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController character;
    OculusControls controls;

    public AudioSource walkSound;
    bool playWalkSound = false;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
        GameObject oculusControls = GameObject.Find("OculusControlManager");
        controls = oculusControls.GetComponent<OculusControls>();

    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
     
    }

    private void FixedUpdate()
    {
        if (playWalkSound && !walkSound.isPlaying)
        {
            walkSound.Play();
        }
        if (!playWalkSound && walkSound.isPlaying)
        {
            walkSound.Stop();
        }
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        if(controls.triggerClick == 1) {
            float s =(float)( speed * 1.5);
            character.Move(direction * Time.fixedDeltaTime * s);
        }
        else
        {
            character.Move(direction * Time.fixedDeltaTime * speed);
        }

        if(direction.x != 0 || direction.z != 0)
        {
            if (!walkSound.isPlaying)
            {
                playWalkSound = true;
            }
        }
        else
        {
            playWalkSound = false;

        }
       

    }
}
