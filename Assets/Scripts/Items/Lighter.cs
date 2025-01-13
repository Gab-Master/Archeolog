using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private ParticleSystem sparkParticles;
    [SerializeField] private int lighterChance = 5;
    private SoundHolder soundHolder;

    private void Awake()
    {
        soundHolder = GameObject.FindWithTag("soundsHolder").GetComponent<SoundHolder>();
    }

    void Start()
    {
        soundSource.clip = soundHolder.GetAudioClip("lighterSparks");
    }

    public bool TryLightOn(bool isHoldingLighter)
    {
        if (isHoldingLighter) 
        {
            sparkParticles.Play();
        }
        soundSource.Play();
        int badLuck = Random.Range(1, lighterChance);
        return badLuck != 1;
    }
}
