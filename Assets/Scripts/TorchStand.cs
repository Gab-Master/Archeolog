using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchStand : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private ParticleSystem lightParticles;
    [SerializeField] private GameObject torchObject;
    [SerializeField] private Light lightArea;

    [ContextMenu("Torch light ON")] private void LightOn() { lightArea.enabled = true; }
    [ContextMenu("Torch light OFF")] private void LightOff() { lightArea.enabled = false; }
    [ContextMenu("With torch")] private void WithTorch() { torchObject.SetActive(true); }
    [ContextMenu("Without torch")] private void WithoutTorch() { torchObject.SetActive(false); }

    private void Start()
    {
        if (lightArea.enabled)
        {
            lightParticles.Play();
            fireSound.Play();

        }
        else
        {
            lightParticles.Stop();
            fireSound.Stop();
        }
    }

    public void PutTorch(bool isTorchLighted)
    {
        torchObject.SetActive(true);
        lightArea.enabled = isTorchLighted;
        if (lightArea.enabled)
        {
            lightParticles.Play();
            fireSound.Play();
        }
        else
        {
            lightParticles.Stop();
            fireSound.Stop();
        }
    }

    public void TakeTorch()
    {
        torchObject.SetActive(false);
    }

    public void Interact(GameObject interacter)
    {
        var playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem == RightHandItem.Torch)
        {
            if (!torchObject.activeSelf)
            {
                PutTorch(playerHands.IsPlayerLightOn);
                playerHands.TakeOffTorch();
            }
        }
        else
        {
            if (torchObject.activeSelf)
            {
                playerHands.TakeTorch(lightArea.enabled);
                TakeTorch();
            }
        }
    }

    public void LightIt(GameObject interacter)
    {
        var playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem != RightHandItem.Empty && playerHands.IsPlayerLightOn)
        {
            lightArea.enabled = true;
            fireSound.Play();
            lightParticles.Play();
            playerHands.FlickerON();
        }
    }
}
