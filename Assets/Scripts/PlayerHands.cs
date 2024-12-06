using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] GameObject Torch;
    [SerializeField] private bool isHoldingTorch;

    private void Update()
    {
        HoldingTorch();
    }

    private void HoldingTorch()
    {
        if (isHoldingTorch)
        {
            Torch.SetActive(true);
        }
        else
        {
            Torch.SetActive(false);
        }
    }
}
