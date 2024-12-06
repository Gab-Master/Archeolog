using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField] private bool isTorchOn;
    [SerializeField] private Light TorchLight;

    void Update()
    {
        if (isTorchOn)
        {
            TorchLight.enabled = true;
        }
        else
        {
            TorchLight.enabled = false;
        }
    }
}
