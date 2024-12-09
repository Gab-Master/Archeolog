using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    [SerializeField] private Light torchLight;
    private light_variable_intensity lvi;

    [ContextMenu("Torch light ON")] private void LightOn() { torchLight.enabled = true; }
    [ContextMenu("Torch light OFF")] private void LightOff() { torchLight.enabled = false; }

    public void SetLight(bool isTorchLighted)
    {
        torchLight.enabled = isTorchLighted;
    }
    
    public void Take()
    {
        Destroy(gameObject);
    }

    public void Interact(GameObject interacter)
    {
        var playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem != RightHandItem.Torch)
        {
            playerHands.TakeTorch(torchLight.enabled);
            Take();
        }
    }

    public void LightIt(GameObject interacter)
    {
        var playerHands = interacter.GetComponent<PlayerHands>();
        if(playerHands.CurrentHandItem != RightHandItem.Empty && playerHands.IsPlayerLightOn)
        {
            SetLight(true);
        }
    }
    

}
