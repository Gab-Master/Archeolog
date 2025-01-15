using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform movingObject;

    [SerializeField] private float speed = 4;

    private bool isOpening = false;

    public void Open()
    {
        isOpening = true;
        Debug.Log("open");
    }

    private void Update()
    {
        var currenTarget = isOpening ? target : transform;
        movingObject.position = Vector3.Lerp(movingObject.position, currenTarget.position, speed * Time.deltaTime);
    }
}
