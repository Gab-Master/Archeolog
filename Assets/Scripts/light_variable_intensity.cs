using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_variable_intensity : MonoBehaviour
{
    public Light light;
    public int seed;
    public float start_intensity;
    private float curr_intensity;
    public float change_rate;
    public float min_intensity;
    public float max_intensity;
    private System.DateTime seed_init;

    void Start()
    {
        light = GetComponent<Light>();
        curr_intensity = start_intensity;
        Random.InitState(seed + (int)seed_init.Millisecond);
    }

    void Update()
    {
        curr_intensity += Random.Range(change_rate*(-1),change_rate);
        if (curr_intensity < min_intensity)
        {
            curr_intensity = min_intensity;
        }
        else if (curr_intensity > max_intensity)
        {
            curr_intensity = max_intensity;
        }
        light.intensity = curr_intensity;
    }
}
