using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkingSpeed = 7.5f;

    public float runningSpeed = 11.5f;

    public float jumpSpeed = 8.0f;

    public float gravity = 20.0f;

    public Camera playerCamera;

    public float lookSpeed = 2.0f;

    CharacterController characterController;

    Vector3 moveDirection = Vector3.zero;

    public bool canMove = true;

    public AudioSource walkSound;
    bool playWalkSound = false;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Debug.Log("playWalkSound = " + playWalkSound);
        if (playWalkSound && !walkSound.isPlaying)
        {
            walkSound.Play();
        }
        if(!playWalkSound && walkSound.isPlaying)
        {
            walkSound.Stop();
        }
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if(!walkSound.isPlaying){ 
                playWalkSound = true;
            }
        }
        else
        {
            playWalkSound = false;

        }
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward =
            playerCamera.transform.TransformDirection(Vector3.forward);
        Vector3 right =
            playerCamera.transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX =
            canMove
                ? (isRunning ? runningSpeed : walkingSpeed) *
                Input.GetAxis("Vertical")
                : 0;
        float curSpeedY =
            canMove
                ? (isRunning ? runningSpeed : walkingSpeed) *
                Input.GetAxis("Horizontal")
                : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded
        )
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            walkSound.Stop();
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            // transform.rotation = playerCamera.transform.rotation;
            //--
            // rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            // rotationX = playerCamera.transform.rotation.x;
            // playerCamera.transform.localRotation =
            //     Quaternion.Euler(rotationX, 0, 0);
            // transform.rotation *=
            //     Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
