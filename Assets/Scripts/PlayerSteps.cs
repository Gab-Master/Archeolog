using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioClip[] walkingSounds; // Array for walking footstep sounds
    public AudioClip[] runningSounds; // Array for running footstep sounds
    public float walkStepInterval = 0.5f; // Time between walking steps
    public float runStepInterval = 0.3f;  // Time between running steps
    public float minVelocity = 0.5f;      // Minimum velocity to trigger sounds

    private Rigidbody rb;                 // Reference to the Rigidbody
    private AudioSource audioSource;      // Reference to the AudioSource
    private float stepTimer;              // Timer to track steps
    private bool isRunning;               // To track if the player is running

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        stepTimer = walkStepInterval; // Default to walking interval
    }

    void Update()
    {
        // Check the player's speed
        float speed = rb.velocity.magnitude;

        // Determine if the player is running (Shift key pressed)
        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (speed > minVelocity) // If the player is moving
        {
            stepTimer -= Time.deltaTime; // Count down the timer

            if (stepTimer <= 0f) // If it's time for a step
            {
                if (isRunning)
                {
                    PlayFootstep(runningSounds); // Use running sounds
                    stepTimer = runStepInterval; // Use running interval
                }
                else
                {
                    PlayFootstep(walkingSounds); // Use walking sounds
                    stepTimer = walkStepInterval; // Use walking interval
                }
            }
        }
        else
        {
            stepTimer = isRunning ? runStepInterval : walkStepInterval; // Reset the timer if not moving
        }
    }

    void PlayFootstep(AudioClip[] soundPool)
    {
        if (soundPool.Length > 0)
        {
            int randomIndex = Random.Range(0, soundPool.Length);
            audioSource.PlayOneShot(soundPool[randomIndex]);
        }
    }
}
