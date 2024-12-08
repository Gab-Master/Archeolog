using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacting : MonoBehaviour
{
    [SerializeField] private Image centerDot;
    [SerializeField] private float pickingDistanse = 2f;
    [SerializeField] private PlayerHands playerHands;
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.collider.gameObject.layer == 12) //TorchStand layer
                {
                    TorchStand torchStand = hit.collider.gameObject.GetComponent<TorchStand>();
                    if (playerHands.IsHoldingTorch)
                    {
                        if (!torchStand.HasTorch)
                        {
                            torchStand.PutTorch(playerHands.IsTorchOn);
                            playerHands.TakeOffTorch();
                        }
                    }
                    else
                    {
                        playerHands.TakeTorch(torchStand.IsTorchOn);
                        torchStand.TakeTorch();
                    }
                }
                else if(hit.collider.gameObject.layer == 11) //Torch layer
                {
                    TorchManager torch = hit.collider.gameObject.GetComponent<TorchManager>();
                    if (!playerHands.IsHoldingTorch)
                    {
                        playerHands.TakeTorch(torch.IsTorchOn);
                        torch.Take();
                    }
                }
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
