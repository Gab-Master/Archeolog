using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    [SerializeField] private SoundHolder soundHolder;
    [SerializeField] private ParticleSystem torchParticles;
    [SerializeField] private Light playerTorchLight;
    [SerializeField] private AudioSource soundSource;
    private AudioClip torchFireSound;

    void Start()
    {
        torchFireSound = soundHolder.GetAudioClip("torchFire");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
