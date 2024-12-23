using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchStand : MonoBehaviour, IInteractable, ICanBeLighted, IOutline
{
    [SerializeField] private GameObject torchObject;
    [SerializeField] private Outline outline;
    private LightController torchLight;

    [ContextMenu("Torch light ON")] private void LightOn() { torchLight.SetLight(true); }
    [ContextMenu("Torch light OFF")] private void LightOff() { torchLight.SetLight(false); }
    [ContextMenu("With torch")] private void WithTorch() { torchObject.SetActive(true); torchLight.SetLight(false); }
    [ContextMenu("Without torch")] private void WithoutTorch() { torchLight.SetLight(false); torchObject.SetActive(false); }

    private void Start()
    {
        torchLight = torchObject.GetComponent<LightController>();
        UnShowOutline();
    }

    public void PutTorch(bool isTorchLighted)
    {
        torchObject.SetActive(true);
        torchLight.SetLight(isTorchLighted);
    }

    public void TakeTorch()
    {
        torchLight.SetLight(false);
        torchObject.SetActive(false);
    }
    public void ShowOutline()
    {
        outline.enabled = true;
    }

    public void UnShowOutline()
    {
        outline.enabled = false;
    }

    public void Interact(GameObject interacter)
    {
        PlayerHands playerHands = interacter.GetComponent<PlayerHands>();
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
                playerHands.TakeTorch(torchLight.IsLighted);
                TakeTorch();
            }
        }
    }

    public void LightIt(GameObject interacter)
    {
        PlayerHands playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem != RightHandItem.Empty && playerHands.IsPlayerLightOn)
        {
            torchLight.SetLight(true);
        }
    }
}
