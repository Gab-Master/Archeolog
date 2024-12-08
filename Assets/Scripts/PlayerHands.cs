using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private GameObject Torch;
    [SerializeField] private bool isTorchOn;
    [SerializeField] private bool isHoldingTorch = false;
    [SerializeField] private Light playerLight;

    public bool IsHoldingTorch => isHoldingTorch;
    public bool IsTorchOn => isTorchOn;

    private void Update()
    {
        UpdateTorchState();
        UpdatePlayerLight();
    }

    private void UpdateTorchState()
    {
        Torch.SetActive(isHoldingTorch);
    }

    private void UpdatePlayerLight()
    {
        playerLight.enabled = isTorchOn;
    }

    public void TakeTorch(bool isTorchLighted)
    {
        isHoldingTorch = true;
        isTorchOn = isTorchLighted;
    }

    public void TakeOffTorch()
    {
        isHoldingTorch = false;
        isTorchOn = false;
    }
}
