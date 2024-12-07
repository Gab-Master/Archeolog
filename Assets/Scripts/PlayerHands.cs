using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private GameObject Torch;
    [SerializeField] private bool isHoldingTorch;

    private void Update()
    {
        UpdateTorchState();
    }

    private void UpdateTorchState()
    {
        Torch.SetActive(isHoldingTorch);
    }
}
