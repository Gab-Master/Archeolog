using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float walkingMaxSpeed = 2f, sneakingMaxSpeed = 1f, sprintingMaxSpeed = 4f;
    [SerializeField] private Rigidbody rb;
    private float horizontalInput, verticalInput;
    private Vector3 moveDirection;
    private MovementType currentMovement = MovementType.Walking;

    public MovementType CurrentMovement => currentMovement;

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

        currentMovement = MovementType.Walking;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMovement = MovementType.Sprinting;
        } 
        else if (Input.GetKey(KeyCode.C))
        {
            currentMovement = MovementType.Sneaking;
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

        maxSpeed = currentMovement switch
        {
            MovementType.Sprinting => sprintingMaxSpeed,
            MovementType.Sneaking => sneakingMaxSpeed,
            _ => walkingMaxSpeed
        };

        if (currentMovement == MovementType.Sprinting) 
        {
            maxSpeed = sprintingMaxSpeed;
        }
        else if (currentMovement == MovementType.Walking)
        {
            maxSpeed = walkingMaxSpeed;
        }
        else if (currentMovement == MovementType.Sneaking)
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

public enum MovementType
{
    Walking,
    Sprinting,
    Sneaking
}
