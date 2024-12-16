using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interacting : MonoBehaviour
{
    [SerializeField] private Image centerDot;
    [SerializeField] private float pickingDistanse = 2f;
    private bool isLookingToFire = false;
    private IOutline lastOutlined;

    public bool IsLookingToFire => isLookingToFire;
    void Update()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        centerDot.enabled = false;
        isLookingToFire = false;
        if (!lastOutlined.IsUnityNull())
        {
            lastOutlined.UnShowOutline();
        }
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.distance <= pickingDistanse)
        {
            if(hit.collider.TryGetComponent(out IOutline outline))
            {
                outline.ShowOutline();
                lastOutlined = outline;
            }
            if (hit.collider.TryGetComponent(out IInteractable interactableObject))
            {
                centerDot.enabled = true;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    interactableObject.Interact(this.gameObject);
                }
            }
            if (hit.collider.TryGetComponent(out ICanBeLighted canBeLightedObject))
            {
                centerDot.enabled = true;
                isLookingToFire = true;
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    canBeLightedObject.LightIt(this.gameObject);
                }
            }
        }
    }
}
