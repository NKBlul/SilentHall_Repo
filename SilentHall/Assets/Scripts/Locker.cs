using UnityEngine;

public enum LockerAnimationState
{
    Locker_Default,
    Locker_Open,
    Locker_Close
}

public class Locker : MonoBehaviour, IInteractable
{
    [SerializeField] Animator animator;
    bool isOpen = false;

    void Start()
    {
        animator.Play(LockerAnimationState.Locker_Default.ToString());
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
