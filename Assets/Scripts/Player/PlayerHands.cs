using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private GameObject playerTorch, playerLighter;
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private Transform playerView;
    [SerializeField] private Interacting interacting;
    [SerializeField] private float throwForce = 1200f;
    [SerializeField] private Lighter lighter;
    private LightController torchLight, lighterLight;
    
    private RightHandItem currentHandItem;

    public RightHandItem CurrentHandItem => currentHandItem;
    public bool IsPlayerLightOn => torchLight.IsLighted || lighterLight.IsLighted;


    private void Start()
    {
        torchLight = playerTorch.GetComponent<LightController>();
        lighterLight = playerLighter.GetComponent<LightController>();
        torchLight.SetLight(false);
        lighterLight.SetLight(false);
        currentHandItem = RightHandItem.Lighter;
    }

    private void Update()
    {
        UpdateRightHand();
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
            torchLight.SetLight(torchLight.IsLighted);
            lighterLight.SetLight(false);
            playerLighter.SetActive(false);
        }
        else if (currentHandItem == RightHandItem.Lighter)
        {
            playerLighter.SetActive(true);
            lighterLight.SetLight(lighterLight.IsLighted);
            torchLight.SetLight(false);
            playerTorch.SetActive(false);
        }
        else
        {
            torchLight.SetLight(false);
            lighterLight.SetLight(false);
            playerTorch.SetActive(false);
            playerLighter.SetActive(false);
        }
    }

    private void UseRightHand()
    {
        if (!IsPlayerLightOn)
        {
            if (currentHandItem == RightHandItem.Torch) 
            {
                torchLight.SetLight(lighter.TryLightOn(false));
            }
            else if(currentHandItem == RightHandItem.Lighter)
            {
                lighterLight.SetLight(lighter.TryLightOn(true));
            }
        }
        else if (!interacting.IsLookingToFire && currentHandItem == RightHandItem.Lighter)
        {
            lighterLight.SetLight(false);
        }
    }

    public void TakeTorch(bool isTorchLighted)
    {
        currentHandItem = RightHandItem.Torch;
        torchLight.SetLight(isTorchLighted);
    }

    public void TakeOffTorch()
    {
        currentHandItem = RightHandItem.Lighter;
        torchLight.SetLight(false);
    }

    private void ThrowTorch()
    {
        Vector3 pos = new Vector3(playerView.position.x, playerView.position.y - 0.5f, playerView.position.z);
        Quaternion rotation = Quaternion.Euler(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        GameObject newTorch = Instantiate(torchPrefab, pos, rotation);
        LightController newTorchLight = newTorch.GetComponent<LightController>();
        newTorchLight.SetLight(torchLight.IsLighted);
        newTorch.GetComponent<Rigidbody>().AddForce(playerView.forward * throwForce);
        TakeOffTorch();
    }
}
