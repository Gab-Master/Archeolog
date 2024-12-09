using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacting : MonoBehaviour
{
    [SerializeField] private Image centerDot;
    [SerializeField] private float pickingDistanse = 2f;
    
    void Update()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit) && 
            hit.collider.TryGetComponent(out IInteractable interactableObject) && 
            hit.distance <= pickingDistanse) 
        {
            centerDot.enabled = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                interactableObject.Interact(this.gameObject);
            } 
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                interactableObject.LightIt(this.gameObject);
            }
        }
        else
        {
            centerDot.enabled = false;
        }
    }
}
