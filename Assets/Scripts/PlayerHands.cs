using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private GameObject playerTorch;
    [SerializeField] private GameObject playerLighter;
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private Transform playerView;
    [SerializeField] private Light playerLight;
    [SerializeField] private bool isPlayerLightOn;
    [SerializeField] private float torchLightRange = 8f;
    [SerializeField] private float lighterLightRange = 3f;
    [SerializeField] private float torchLightIntensity = 5.5f;
    [SerializeField] private float lighterLightIntensity = 1f;
    [SerializeField] private float throwForce = 1200f;
    private RightHandItem currentHandItem = RightHandItem.Lighter;

    public RightHandItem CurrentHandItem => currentHandItem;
    public bool IsPlayerLightOn => isPlayerLightOn;

    private void Update()
    {
        UpdateRightHand();
        UpdatePlayerLight();
        if (Input.GetKeyDown(KeyCode.F) && currentHandItem == RightHandItem.Torch)
        {
            ThrowTorch();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UseRightHand();
        }
    }

    private void UpdateRightHand()
    {
        if (currentHandItem == RightHandItem.Torch)
        {
            playerTorch.SetActive(true);
            playerLighter.SetActive(false);
        }
        else if (currentHandItem == RightHandItem.Lighter)
        {
            playerTorch.SetActive(false);
            playerLighter.SetActive(true);
        }
        else
        {
            playerTorch.SetActive(false);
            playerLighter.SetActive(false);
        }
    }

    private void UpdatePlayerLight()
    {
        float lightRange = currentHandItem switch
        {
            RightHandItem.Torch => torchLightRange,
            RightHandItem.Lighter => lighterLightRange,
            _ => 0f
        };
        float lightIntensity = currentHandItem switch
        { 
            RightHandItem.Torch => torchLightIntensity,
            RightHandItem.Lighter => lighterLightIntensity,
            _ => 0f
        };
        playerLight.range = lightRange;
        playerLight.intensity = lightIntensity;
        playerLight.enabled = isPlayerLightOn;
    }

    private void UseRightHand()
    {
        if (!isPlayerLightOn)
        {
            //DŸwiêk próby zapalenia zapalniczki
        }
    }

    public void TakeTorch(bool isTorchLighted)
    {
        currentHandItem = RightHandItem.Torch;
        isPlayerLightOn = isTorchLighted;
    }

    public void TakeOffTorch()
    {
        currentHandItem = RightHandItem.Lighter;
        isPlayerLightOn = false;
    }

    private void ThrowTorch()
    {
        Vector3 pos = new Vector3(playerView.position.x, playerView.position.y - 0.5f, playerView.position.z);
        Quaternion rotation = Quaternion.Euler(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        GameObject torch = Instantiate(torchPrefab, pos, rotation);
        Torch torchScript = torch.GetComponent<Torch>();
        torchScript.SetLight(isPlayerLightOn);
        torch.GetComponent<Rigidbody>().AddForce(playerView.forward * throwForce);
        TakeOffTorch();
    }
}

public enum RightHandItem
{
    Torch,
    Lighter,
    Empty
}
