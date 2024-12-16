using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable, ICanBeLighted
{
    [SerializeField] private LightController torchLight;
    public void Take()
    {
        Destroy(gameObject);
    }

    public void Interact(GameObject interacter)
    {
        PlayerHands playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem != RightHandItem.Torch)
        {
            playerHands.TakeTorch(torchLight.IsLighted);
            Take();
        }
    }

    public void LightIt(GameObject interacter)
    {
        PlayerHands playerHands = interacter.GetComponent<PlayerHands>();
        if (playerHands.CurrentHandItem != RightHandItem.Empty && playerHands.IsPlayerLightOn)
        {
            torchLight.SetLight(true);
        }
    }
}
