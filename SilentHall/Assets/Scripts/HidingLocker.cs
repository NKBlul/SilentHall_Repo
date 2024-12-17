using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingLocker : BaseLocker, IInteractable
{
    [SerializeField] Transform hidingSpot;

    public string GetInteractionPrompt(GameObject trigger)
    {
        return $"Press [E] to hide";
    }

    public void OnInteract(GameObject trigger)
    {
        if (!isOpen)
        {
            animator.Play(LockerAnimationState.Locker_Open.ToString());
            isOpen = true;
        }
    }
}
