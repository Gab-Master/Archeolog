using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacting : MonoBehaviour
{
    [SerializeField] private Image centerDot;
    [SerializeField] private float pickingDistanse = 2f;
    private Ray ray;
    
    void Update()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit) & hit.collider.tag == "CanInteract" & hit.distance <= pickingDistanse) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picked");
            }
            else
            {
                centerDot.enabled = true;
            }
        }
        else
        {
            centerDot.enabled = false;
        }
    }
}
