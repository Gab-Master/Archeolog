using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private Transform playerView;
    [SerializeField] private GameObject Torch;
    [SerializeField] private bool isTorchOn;
    [SerializeField] private bool isHoldingTorch = false;
    [SerializeField] private Light playerLight;
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private float throwForce = 1200f;

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
        Vector3 pos = new Vector3(playerView.position.x, playerView.position.y - 0.5f, playerView.position.z);
        Quaternion rotation = Quaternion.Euler(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        GameObject torch = Instantiate(torchPrefab, pos, rotation);
        TorchManager torchScript = torch.GetComponent<TorchManager>();
        torchScript.SetLight(isTorchOn);
        torch.GetComponent<Rigidbody>().AddForce(playerView.forward * throwForce);
        TakeOffTorch();
    }
}
