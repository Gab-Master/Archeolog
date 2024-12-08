using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField] private bool isTorchOn = false;
    [SerializeField] private Light TorchLight;

    public bool IsTorchOn => isTorchOn;

    private void Update()
    {
        LightingTorch();
    }

    private void LightingTorch()
    {
        TorchLight.enabled = isTorchOn;
    }

    public void SetLight(bool isTorchLighted)
    {
        isTorchOn = isTorchLighted;
    }
    
    public void Take()
    {
        Destroy(gameObject);
    }
}
