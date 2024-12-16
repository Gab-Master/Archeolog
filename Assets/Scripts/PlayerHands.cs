using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private GameObject playerTorch, playerLighter;
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private Transform playerView;
    [SerializeField] private float throwForce = 1200f;
    [SerializeField] private int lighterChance = 5;
    private LightController torchLight, lighterLight;
    private RightHandItem currentHandItem;
    private bool interaction = false;

    public RightHandItem CurrentHandItem => currentHandItem;
    public bool IsPlayerLightOn => torchLight.IsLighted || lighterLight.IsLighted;


    private void Start()
    {
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
        playerTorch.SetActive(false);
        playerLighter.SetActive(false);
        if (currentHandItem == RightHandItem.Torch)
        {
            playerTorch.SetActive(true);
            torchLight.SetLight(true);
            lighterLight.SetLight(false);
        }
        else if (currentHandItem == RightHandItem.Lighter)
        {
            playerLighter.SetActive(true);
            lighterLight.SetLight(true);
            torchLight.SetLight(false);
        }
    }

    private void UseRightHand()
    {
        if (!IsPlayerLightOn)
        {
            sparkParticles.Play();
            lighterSparkSound.Play();
            float badLuck = Random.Range(1, lighterChance);
            if (badLuck != 1)
            {
                isPlayerLightOn = true;
            }
        }
        else if(currentHandItem == RightHandItem.Lighter)
        {
            if (interaction)
            {
                interaction = false;
            }
            else
            {
                isPlayerLightOn = false;
            }
        }
    }

    public void FlickerON()
    {
        isPlayerLightOn = true;
        interaction = true;
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
