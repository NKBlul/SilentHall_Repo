using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDoor : BaseDoor, IInteractable
{
    public string GetInteractionPrompt()
    {
        if (!isOpen)
        {
            return $"Press [E] to open door";
        }

        return $"Press [E] to close door";
    }

    public void OnInteract()
    {
        Debug.Log("HELO");
        if (!isOpen)
        {
            animator.Play(DoorAnimationState.Door_Open.ToString());
            isOpen = true;
        }
        else
        {
            animator.Play(DoorAnimationState.Door_Close.ToString());
            isOpen = false;
        }
    }
}
