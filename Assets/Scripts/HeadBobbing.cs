using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float sprintingAmount, sprintingFrequency;
    [SerializeField] private float walkingAmount, walkingFrequency;
    [SerializeField] private float sneakingAmount, sneakingFrequency;
    [SerializeField] private float smooth;
    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.localPosition;
    }

    private void Update()
    {
        HeadBob();
        StopHeadBob();
    }
    private void HeadBob()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;

        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }

    private void StartHeadBob()
    {
        float frequency = 0.002f;
        float amount = 10f;

        if (playerMovement.CurrentMovement == MovementType.Sprinting)
        {
            amount = sprintingAmount;
            frequency = sprintingFrequency;
        }
        else if (playerMovement.CurrentMovement == MovementType.Walking)
        {
            amount = walkingAmount;
            frequency = walkingFrequency;
        }
        else if (playerMovement.CurrentMovement == MovementType.Sneaking)
        {
            amount = sneakingAmount;
            frequency = sneakingFrequency;
        }

        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;
    }

    private void StopHeadBob()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, startingPos, Time.deltaTime);
    }
}
