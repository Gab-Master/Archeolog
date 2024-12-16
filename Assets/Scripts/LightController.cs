using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private SoundHolder soundHolder;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private ParticleSystem lightParticles;
    [SerializeField] private Light lightSource;
    [SerializeField] private string fireSoundName;
    private AudioClip fireSound;

    [ContextMenu("Torch light ON")] private void LightOn() { lightSource.enabled = true; }
    [ContextMenu("Torch light OFF")] private void LightOff() { lightSource.enabled = false; }

    public bool IsLighted => lightSource.enabled;

    private void Start()
    {
        fireSound = soundHolder.GetAudioClip(fireSoundName);
        soundSource.clip = fireSound;
        SetLight(lightSource.enabled);
    }

    public void SetLight(bool isTorchLighted)
    {
        lightSource.enabled = isTorchLighted;
        if (lightSource.enabled)
        {
            lightParticles.Play();
            if (!soundSource.isPlaying)
            {
                soundSource.Play();
            }
        }
        else
        {
            lightParticles.Stop();
            soundSource.Stop();
        }
    }

    
}
