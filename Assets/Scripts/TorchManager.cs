using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField] private bool isTorchOn;
    [SerializeField] private Light TorchLight;

    private void Update()
    {
        LightingTorch();
    }

    private void LightingTorch()
    {
        TorchLight.enabled = isTorchOn;
    }
}
