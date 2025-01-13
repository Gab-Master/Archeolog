using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVariableIntensity : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private Vector3 startPos;

    [Space]
    [SerializeField] private float minIntensity = 4;
    [SerializeField] private float maxIntensity = 6;

    [Space]
    [SerializeField] private float minRange = 4;
    [SerializeField] private float maxRange = 7;

    [SerializeField] private float minMove = -0.4f;
    [SerializeField] private float maxMove = 0.4f;

    [SerializeField] private float minChangeRate = 0.05f;
    [SerializeField] private float maxChangeRate = 0.2f;

    private float changeTimer = 0;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (!light.enabled)
        {
            return;
        }

        changeTimer -= Time.deltaTime;

        if (changeTimer < 0)
        {
            changeTimer = Random.Range(minChangeRate, maxChangeRate);

            transform.localPosition = startPos + new Vector3(0, Random.Range(minMove, maxMove), 0);


            light.range = Random.Range(minRange, maxRange);
            light.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
