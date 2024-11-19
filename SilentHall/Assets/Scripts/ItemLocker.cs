using UnityEngine;

public class ItemLocker : BaseLocker, IInteractable
{
    public string GetInteractionPrompt()
    {
        return $"Press [E] to open";
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
