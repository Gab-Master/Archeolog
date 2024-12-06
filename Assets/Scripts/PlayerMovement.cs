using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed, walkingMaxSpeed, sneakingMaxSpeed, sprintingMaxSpeed;
    [SerializeField] private Rigidbody rb;
    private float horizontalInput, verticalInput;
    private Vector3 moveDirection;
    public bool isSprinting, isSneaking, isWalking;

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            isWalking = false;
            isSneaking = false;
        } 
        else if (Input.GetKey(KeyCode.C))
        {
            isSprinting = false;
            isWalking = false;
            isSneaking = true;
        }
        else
        {
            isSprinting = false;
            isWalking = true;
            isSneaking = false;
        }
    }

    private void MovePlayer()
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float maxSpeed = 3;

        if (isSprinting) 
        {
            maxSpeed = sprintingMaxSpeed;
        }
        else if (isWalking)
        {
            maxSpeed = walkingMaxSpeed;
        }
        else if (isSneaking)
        {
            maxSpeed = sneakingMaxSpeed;
        }

        if(flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
