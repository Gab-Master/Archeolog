using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchStand : MonoBehaviour
{
    [SerializeField] private GameObject torchObject;
    [SerializeField] private Light Light;
    [SerializeField] private bool hasTorch = true;
    [SerializeField] private bool isLightOn = false;

    public bool IsTorchOn => isLightOn;
    public bool HasTorch => hasTorch;

    void Update()
    {
        UpdateTorchStand();
    }

    private void UpdateTorchStand()
    {
        torchObject.SetActive(hasTorch);
        Light.enabled = isLightOn;
    }

    public void PutTorch(bool isTorchLighted)
    {
        hasTorch = true;
        isLightOn = isTorchLighted;
    }

    public void TakeTorch()
    {
        hasTorch = false;
    }
}
