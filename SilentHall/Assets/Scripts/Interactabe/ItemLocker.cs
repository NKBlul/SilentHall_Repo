using UnityEngine;

public class ItemLocker : BaseLocker, IInteractable
{
    public string GetInteractionPrompt(GameObject trigger)
    {
        if (!isOpen)
        {
            return $"Press [E] to open";
        }
        return "";
    }


    public void OnInteract(GameObject trigger)
    {
        if (!isOpen)
        {
            animator.Play(LockerAnimationState.Locker_Open.ToString());
            isOpen = true;
            gameObject.layer = default;
        }
    }
}
