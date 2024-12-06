using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerView;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private PlayerMovement playerMovement;
    //Variables for rotating camera
    [SerializeField] private float sensX, sensY;
    private float xRotation, yRotation;
    //Variables for Headbob
    [SerializeField] private float sprintingAmount, sprintingFrequency;
    [SerializeField] private float walkingAmount, walkingFrequency;
    [SerializeField] private float sneakingAmount, sneakingFrequency;
    [SerializeField] private float smooth;
    private Vector3 startingPos;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startingPos = playerCamera.localPosition;
    }

    private void Update()
    {
        RotateCamera();
        HeadBobTrigger();
        StopHeadBob();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        playerView.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void HeadBobTrigger()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float inputMagnitude = new Vector3(horizontal, 0, vertical).magnitude;

        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }

    private void StartHeadBob()
    {
        float frequency = 0.002f;
        float amount = 10f;

        if (playerMovement.isSprinting)
        {
            amount = sprintingAmount;
            frequency = sprintingFrequency;
        } 
        else if (playerMovement.isWalking)
        {
            amount = walkingAmount;
            frequency = walkingFrequency;
        }
        else if (playerMovement.isSneaking)
        {
            amount = sneakingAmount;
            frequency = sneakingFrequency;
        }

        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        playerCamera.localPosition += pos;
    }

    private void StopHeadBob()
    {
        if (playerCamera.localPosition == startingPos) return;
        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, startingPos, Time.deltaTime);
    }
}
