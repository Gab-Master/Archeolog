using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable, ICanBeLighted
{
    [SerializeField] private SoundHolder soundHolder;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private ParticleSystem lightParticles;
    [SerializeField] private Light torchLight;
    private AudioClip torchFireSound;

    [ContextMenu("Torch light ON")] private void LightOn() { torchLight.enabled = true; }
    [ContextMenu("Torch light OFF")] private void LightOff() { torchLight.enabled = false; }

    public bool IsLighted => torchLight.enabled;

    private void Start()
    {
        torchFireSound = soundHolder.GetAudioClip("torchFire");
        soundSource.clip = torchFireSound;
        SetLight(torchLight.enabled);
    }

    public void SetLight(bool isTorchLighted)
    {
        torchLight.enabled = isTorchLighted;
        if (torchLight.enabled)
        {
            lightParticles.Play();
            soundSource.Play();
        }
        else
        {
            lightParticles.Stop();
            soundSource.Stop();
        }
    }
    
    public void Take()
    {
        Destroy(gameObject);
    }

    public void Interact(GameObject interacter)
    {
        PlayerHands playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem != RightHandItem.Torch)
        {
            playerHands.TakeTorch(torchLight.enabled);
            Take();
        }
    }

    public void LightIt(GameObject interacter)
    {
        PlayerHands playerHands = interacter.GetComponent<PlayerHands>();
        if(playerHands.CurrentHandItem != RightHandItem.Empty && playerHands.IsPlayerLightOn)
        {
            SetLight(true);
            playerHands.FlickerON();
        }
    }
    

}
