using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField] private bool isTorchOn = false;
    [SerializeField] private Light TorchLight;
    [SerializeField] private ParticleSystem fire_particles;

    private void Update()
    {
        fire_particles = GetComponent<ParticleSystem>();
        LightingTorch();
    }

    private void LightingTorch()
    {
        TorchLight.enabled = isTorchOn;
        var ps_emission = fire_particles.emission;
        ps_emission.enabled = isTorchOn;
    }
}
