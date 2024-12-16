using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchStand : MonoBehaviour, IInteractable, ICanBeLighted
{
    [SerializeField] private GameObject torchObject;
    private Torch torch;

    [ContextMenu("Torch light ON")] private void LightOn() { torch.SetLight(true); }
    [ContextMenu("Torch light OFF")] private void LightOff() { torch.SetLight(false); }
    [ContextMenu("With torch")] private void WithTorch() { torchObject.SetActive(true); torch.SetLight(false); }
    [ContextMenu("Without torch")] private void WithoutTorch() { torch.SetLight(false); torchObject.SetActive(false); }

    private void Start()
    {
        torch = GetComponent<Torch>();
    }

    public void PutTorch(bool isTorchLighted)
    {
        torchObject.SetActive(true);
        torch.SetLight(isTorchLighted);
    }

    public void TakeTorch()
    {
        torch.SetLight(false);
        torchObject.SetActive(false);
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
                playerHands.TakeTorch(torch.IsLighted);
                TakeTorch();
            }
        }
    }

    public void LightIt(GameObject interacter)
    {
        PlayerHands playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem != RightHandItem.Empty && playerHands.IsPlayerLightOn)
        {
            torch.SetLight(true);
            playerHands.FlickerON();
        }
    }
}
