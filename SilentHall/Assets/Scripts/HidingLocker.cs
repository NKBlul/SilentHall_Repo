using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingLocker : BaseLocker, IInteractable
{
    public string GetInteractionPrompt()
    {
        return $"Press [E] to hide";
    }


    public void OnInteract()
    {
        if (!isOpen)
        {
            animator.Play(LockerAnimationState.Locker_Open.ToString());
            isOpen = true;
        }
    }
}
