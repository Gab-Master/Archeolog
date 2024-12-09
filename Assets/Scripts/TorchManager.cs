using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField] private bool isTorchOn = false;
    [SerializeField] private Light TorchLight;
    [SerializeField] private ParticleSystem fireParticles;

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        LightingTorch();
    }

    private void LightingTorch()
    {
        TorchLight.enabled = isTorchOn;
        var ps_emission = fireParticles.emission;
        ps_emission.enabled = isTorchOn;
    }
}
