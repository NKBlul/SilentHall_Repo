using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseController : MonoBehaviour
{
    private Animator animator;
    public AnimationClip clip;
    void Start()
    {
        animator = GetComponent<Animator>();
        SetPose(clip.name);
    }

    public void SetPose(string name)
    {
        animator.Play(name);
    }
}
