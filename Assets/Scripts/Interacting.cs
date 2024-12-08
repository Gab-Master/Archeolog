using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    private Ray ray;
    
    void Update()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit) & hit.collider.tag == "CanBePicked") 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picked");
            }
            else
            {
                Debug.Log("Show");
            }
        }
        else
        {
            Debug.Log("De-show");
        }
    }
}
