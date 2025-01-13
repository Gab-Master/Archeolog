using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightBlinking : MonoBehaviour
{
    [SerializeField] private Light lightArea;
    [SerializeField] private float minIntensity, maxIntensity;
    [SerializeField] private float minRange, maxRange;
    [SerializeField] private float currIntensity, currRange;

    public void Start()
    {
        currIntensity = lightArea.intensity;
        currRange = lightArea.range;
    }

    public void Update()
    {
        currIntensity = Random.Range(minIntensity, maxIntensity);
        currRange = Random.Range(minRange, maxRange);
        lightArea.intensity = currIntensity;
        lightArea.range = currRange;
    }

    public void setIntensityProperties(float newMinIntensity, float newMaxIntensity)
    {
        minIntensity = newMinIntensity;
        maxIntensity = newMaxIntensity;
    }
    public void setRangeProperties(float newMinRange, float newMaxRange)
    {
        minRange = newMinRange;
        maxRange = newMaxRange;
    }
}
