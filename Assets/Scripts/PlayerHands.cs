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
    [SerializeField] private GameObject torchPrefab;

    public bool IsHoldingTorch => isHoldingTorch;
    public bool IsTorchOn => isTorchOn;

    private void Update()
    {
        UpdateTorchState();
        UpdatePlayerLight();
        if (Input.GetKeyDown(KeyCode.F) & IsHoldingTorch)
        {
            ThrowTorch();
        }
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

    private void ThrowTorch()
    {
        GameObject torch = Instantiate(torchPrefab, transform.localPosition, Quaternion.identity);
        TorchManager torchScript = torch.GetComponent<TorchManager>();
        torchScript.SetLight(isTorchOn);
        TakeOffTorch();
    }
}
