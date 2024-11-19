using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LockerAnimationState
{
    Locker_Default,
    Locker_Open,
    Locker_Close
}

public class BaseLocker : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected bool isOpen = false;

    void Start()
    {
        animator.Play(LockerAnimationState.Locker_Default.ToString());
    }
}
