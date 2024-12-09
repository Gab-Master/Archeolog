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
    }

    private void UpdateRightHand()
    {
        if (currentHandItem == RightHandItem.Torch)
        {
            playerTorch.SetActive(true);
        }
        else if (currentHandItem == RightHandItem.Lighter)
        {

        }
        else
        {
            playerTorch.SetActive(false);
        }
    }

    private void UpdatePlayerLight()
    {
        playerLight.enabled = isPlayerLightOn;
    }

    public void TakeTorch(bool isTorchLighted)
    {
        currentHandItem = RightHandItem.Torch;
        isPlayerLightOn = isTorchLighted;
    }

    public void TakeOffTorch()
    {
        currentHandItem = RightHandItem.Empty;
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
