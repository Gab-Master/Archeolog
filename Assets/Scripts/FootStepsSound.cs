using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FootStepsSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float defaultWalkSoundDelay = 0.7f;
    [SerializeField] private float defaultSprintSoundDelay = 0.4f;

    private float timeToPlayNextSound;
    private List<AudioClip> walkingSounds;
    private List<AudioClip> runningSounds;
    private SoundHolder soundHolder;

    private void Awake()
    {
        soundHolder = GameObject.FindWithTag("soundsHolder").GetComponent<SoundHolder>();
    }

    private void Start()
    {
        walkingSounds = soundHolder.GetAudioList("walking");
        runningSounds = soundHolder.GetAudioList("running");
        timeToPlayNextSound = 0.5f;
    }

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
        soundSource.clip = sounds[index];
        soundSource.Play();
    }
}
