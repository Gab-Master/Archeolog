using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource walkingSource;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<AudioClip> walkingSounds;
    [SerializeField] private List<AudioClip> runningSounds;
    [SerializeField] private float defaultWalkSoundDelay = 0.5f;
    [SerializeField] private float defaultSprintSoundDelay = 0.5f;
    private float timeToPlayNextSound = 0.5f;

    void Update()
    {
        timeToPlayNextSound -= Time.deltaTime;
        float playerSpeed = rb.velocity.magnitude;
        if (timeToPlayNextSound < 0 && playerSpeed > 0.1f)
        {
            timeToPlayNextSound = 0.5f;
            if(playerMovement.CurrentMovement == MovementType.Sprinting)
            {
                PlayRandomSound(runningSounds);
                timeToPlayNextSound = defaultSprintSoundDelay;
            }
            else if(playerMovement.CurrentMovement == MovementType.Walking)
            {
                PlayRandomSound(walkingSounds);
                timeToPlayNextSound = defaultWalkSoundDelay;
            }
        }
    }

    private void PlayRandomSound(List<AudioClip> sounds)
    {
        int index = Random.Range(0, sounds.Count-1);
        walkingSource.clip = sounds[index];
        walkingSource.Play();
    }
}
