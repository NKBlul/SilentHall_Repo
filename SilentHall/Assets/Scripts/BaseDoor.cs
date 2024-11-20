using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorAnimationState
{
    Door_Default,
    Door_Open,
    Door_Close
}

public class BaseDoor : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected bool isOpen = false;

    void Start()
    {
        animator.Play(DoorAnimationState.Door_Open.ToString());
    }
}
