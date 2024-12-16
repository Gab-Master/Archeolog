using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource lighterSparkSound;
    [SerializeField] private AudioSource lighterFireSound;
    [SerializeField] private ParticleSystem sparkParticles;
    [SerializeField] private ParticleSystem torchParticles;
    [SerializeField] private ParticleSystem lighterParticles;
    [SerializeField] private GameObject playerTorch;
    [SerializeField] private GameObject playerLighter;
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private Transform playerView;
    [SerializeField] private Light playerLighterLight;
    [SerializeField] private Light playerTorchLight;
    [SerializeField] private bool isPlayerLightOn;
    //[SerializeField] private float torchLightRange = 8f;
    //[SerializeField] private float lighterLightRange = 3f;
    //[SerializeField] private float torchLightIntensity = 5.5f;
    //[SerializeField] private float lighterLightIntensity = 1f;
    [SerializeField] private float throwForce = 1200f;
    [SerializeField] private int lighterChance = 5;
    private RightHandItem currentHandItem = RightHandItem.Lighter;
    private bool interaction = false;

    public RightHandItem CurrentHandItem => currentHandItem;
    public bool IsPlayerLightOn => isPlayerLightOn;


    private void Start()
    {
        isPlayerLightOn = false;
    }

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
        //float lightRange = currentHandItem switch
        //{
        //    RightHandItem.Torch => torchLightRange,
        //    RightHandItem.Lighter => lighterLightRange,
        //    _ => 0f
        //};
        //float lightIntensity = currentHandItem switch
        //{
        //    RightHandItem.Torch => torchLightIntensity,
        //    RightHandItem.Lighter => lighterLightIntensity,
        //    _ => 0f
        //};
        ////playerLight.range = lightRange;
        ////playerLight.intensity = lightIntensity;
        //playerLight.enabled = isPlayerLightOn;
        if (isPlayerLightOn) 
        {
            switch (currentHandItem)
            {
                case RightHandItem.Torch:
                    playerTorchLight.enabled = true;
                    playerLighterLight.enabled = false;
                    torchParticles.Play();
                    lighterParticles.Stop();
                    if (!fireSound.isPlaying){fireSound.Play();}
                    lighterFireSound.Stop();
                    break;
                case RightHandItem.Lighter:
                    playerTorchLight.enabled = false;
                    playerLighterLight.enabled = true;
                    if (!lighterFireSound.isPlaying) { lighterFireSound.Play(); }
                    torchParticles.Stop();
                    lighterParticles.Play();
                    fireSound.Stop();
                    break;
            }
        }
        else
        {
            fireSound.Stop();
            lighterParticles.Stop();
            torchParticles.Stop();
            lighterFireSound.Stop();
            playerTorchLight.enabled = false;
            playerLighterLight.enabled = false;
        }
    }

    private void UseRightHand()
    {
        if (!isPlayerLightOn)
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
