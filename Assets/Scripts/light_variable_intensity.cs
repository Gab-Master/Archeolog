using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_variable_intensity : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private int seed;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float startIntensity;
    [SerializeField] private float startRange;
    private float currIntensity;
    private float currRange;
    [SerializeField] private float changeRate;
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    [SerializeField] private float maxMove;
    [SerializeField] private float minMove;
    private float flicker;

    private System.DateTime seed_init;

    void Start()
    {
        light = GetComponent<Light>();
        currIntensity = startIntensity;
        currRange = startRange;
        Random.InitState(seed + (int)seed_init.Millisecond);
        flicker = Random.Range(minMove, maxMove);
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = startPos + new Vector3(0,flicker,0);
        flicker = Random.Range(minMove, maxMove);
        currIntensity += Random.Range(changeRate*(-1),changeRate);
        currRange += Random.Range(changeRate * (-1), changeRate);
        currIntensity = Mathf.Clamp(currIntensity, minIntensity, maxIntensity);
        light.intensity = currIntensity;
        currRange = Mathf.Clamp(currRange, minRange, maxRange);
        light.range = currRange;
    }
}
