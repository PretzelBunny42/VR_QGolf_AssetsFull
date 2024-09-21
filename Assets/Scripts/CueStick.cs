using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CueStick : MonoBehaviour
{
    public GameObject cueBall;  // Reference to the cue ball
    public float forceMultiplier = 100f;  // Multiplier for the force to be applied

    private Rigidbody cueRb;
    private Rigidbody cueBallRb;

    public InputAction RightTriggerButton;
    public InputAction LeftTriggerButton;

    void Start()
    {
        cueRb = GetComponent<Rigidbody>();
        cueBallRb = cueBall.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        RightTriggerButton.Enable();
        LeftTriggerButton.Enable();
    }

    private void OnDisable()
    {
        RightTriggerButton.Disable();
        LeftTriggerButton.Disable();
    }

    void Update()
    {
        Debug.Log(RightTriggerButton.ReadValue<float>());
        
        if (RightTriggerButton.ReadValue<float>() > 0f)
        {
            cueRb.constraints = RigidbodyConstraints.FreezePosition;

            if (LeftTriggerButton.ReadValue<float>() > 0f)
            {
                cueRb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
            }
        }

        //// Handle cue movement (for simplicity, using arrow keys)
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //cueRb.AddForce(movement * forceMultiplier * Time.deltaTime);

        //// Add a hit force when space is pressed
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ApplyHitForce();
        //}
    }

    void ApplyHitForce()
    {
        // Calculate the direction from the cue to the ball
        Vector3 direction = cueBall.transform.position - transform.position;
        direction.Normalize();

        // Apply force to the cue ball
        cueBallRb.AddForce(direction * forceMultiplier, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the cue collides with the cue ball, apply force
        if (collision.gameObject == cueBall)
        {
            ApplyHitForce();
        }
    }
}

